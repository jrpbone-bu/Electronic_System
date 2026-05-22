using MySqlConnector;

namespace electronics;

public sealed class AccountRepository
{
    private readonly DatabaseConnection databaseConnection = new();

    public async Task InitializeAsync()
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string createTableSql = """
            CREATE TABLE IF NOT EXISTS accounts (
                id INT AUTO_INCREMENT PRIMARY KEY,
                username VARCHAR(50) NOT NULL UNIQUE,
                password_hash VARCHAR(255) NOT NULL,
                full_name VARCHAR(120) NOT NULL,
                email VARCHAR(120) NOT NULL UNIQUE,
                role VARCHAR(50) NOT NULL DEFAULT 'User',
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
            );
            """;

        await using (MySqlCommand createCommand = new(createTableSql, connection))
        {
            await createCommand.ExecuteNonQueryAsync();
        }

        const string countSql = "SELECT COUNT(*) FROM accounts;";
        await using MySqlCommand countCommand = new(countSql, connection);
        long accountCount = (long)(await countCommand.ExecuteScalarAsync() ?? 0L);

        if (accountCount == 0)
        {
            const string seedSql = """
                INSERT INTO accounts (username, password_hash, full_name, email, role, is_active)
                VALUES (@username, @password_hash, @full_name, @email, @role, 1);
                """;
            await using MySqlCommand seedCommand = new(seedSql, connection);
            seedCommand.Parameters.AddWithValue("@username", "administrator");
            seedCommand.Parameters.AddWithValue("@password_hash", PasswordHasher.Hash("admin123"));
            seedCommand.Parameters.AddWithValue("@full_name", "System Administrator");
            seedCommand.Parameters.AddWithValue("@email", "admin@electronicshop.local");
            seedCommand.Parameters.AddWithValue("@role", "Admin");
            await seedCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task<Account?> AuthenticateAsync(string username, string password)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT id, username, password_hash, full_name, email, role, is_active, created_at, updated_at
            FROM accounts
            WHERE username = @username
            LIMIT 1;
            """;

        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@username", username.Trim());

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        string passwordHash = reader.GetString("password_hash");
        bool isActive = reader.GetBoolean("is_active");
        if (!isActive || !PasswordHasher.Verify(password, passwordHash))
        {
            return null;
        }

        return ReadAccount(reader);
    }

    public async Task<List<Account>> SearchAsync(string searchText)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT id, username, full_name, email, role, is_active, created_at, updated_at
            FROM accounts
            WHERE @search = ''
               OR username LIKE @pattern
               OR full_name LIKE @pattern
               OR email LIKE @pattern
               OR role LIKE @pattern
            ORDER BY full_name, username;
            """;

        await using MySqlCommand command = new(sql, connection);
        string search = searchText.Trim();
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@pattern", $"%{search}%");

        List<Account> accounts = new();
        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            accounts.Add(ReadAccount(reader));
        }

        return accounts;
    }

    public async Task AddAsync(string username, string password, string fullName, string email, string role, bool isActive)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            INSERT INTO accounts (username, password_hash, full_name, email, role, is_active)
            VALUES (@username, @password_hash, @full_name, @email, @role, @is_active);
            """;

        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@username", username.Trim());
        command.Parameters.AddWithValue("@password_hash", PasswordHasher.Hash(password));
        command.Parameters.AddWithValue("@full_name", fullName.Trim());
        command.Parameters.AddWithValue("@email", email.Trim());
        command.Parameters.AddWithValue("@role", role.Trim());
        command.Parameters.AddWithValue("@is_active", isActive);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(int id, string username, string fullName, string email, string role, bool isActive, string? newPassword)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        string sql = string.IsNullOrWhiteSpace(newPassword)
            ? """
              UPDATE accounts
              SET username = @username, full_name = @full_name, email = @email, role = @role, is_active = @is_active
              WHERE id = @id;
              """
            : """
              UPDATE accounts
              SET username = @username, full_name = @full_name, email = @email, role = @role, is_active = @is_active, password_hash = @password_hash
              WHERE id = @id;
              """;

        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@username", username.Trim());
        command.Parameters.AddWithValue("@full_name", fullName.Trim());
        command.Parameters.AddWithValue("@email", email.Trim());
        command.Parameters.AddWithValue("@role", role.Trim());
        command.Parameters.AddWithValue("@is_active", isActive);

        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            command.Parameters.AddWithValue("@password_hash", PasswordHasher.Hash(newPassword));
        }

        await command.ExecuteNonQueryAsync();
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = "UPDATE accounts SET is_active = @is_active WHERE id = @id;";
        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@is_active", isActive);
        await command.ExecuteNonQueryAsync();
    }

    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = "UPDATE accounts SET password_hash = @password_hash WHERE email = @email;";
        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@email", email.Trim());
        command.Parameters.AddWithValue("@password_hash", PasswordHasher.Hash(newPassword));

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = "SELECT COUNT(*) FROM accounts WHERE email = @email;";
        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@email", email.Trim());

        long count = (long)(await command.ExecuteScalarAsync() ?? 0L);
        return count > 0;
    }

    private static Account ReadAccount(MySqlDataReader reader)
    {
        return new Account
        {
            Id = reader.GetInt32("id"),
            Username = reader.GetString("username"),
            FullName = reader.GetString("full_name"),
            Email = reader.GetString("email"),
            Role = reader.GetString("role"),
            IsActive = reader.GetBoolean("is_active"),
            CreatedAt = reader.GetDateTime("created_at"),
            UpdatedAt = reader.GetDateTime("updated_at")
        };
    }
}
