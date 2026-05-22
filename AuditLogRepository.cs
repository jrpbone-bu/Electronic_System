using MySqlConnector;

namespace electronics;

public sealed class AuditLogRepository
{
    private readonly DatabaseConnection databaseConnection = new();

    public async Task InitializeAsync()
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            CREATE TABLE IF NOT EXISTS activity_logs (
                id INT AUTO_INCREMENT PRIMARY KEY,
                occurred_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                account_id INT NULL,
                username VARCHAR(50) NOT NULL,
                full_name VARCHAR(120) NOT NULL,
                action VARCHAR(80) NOT NULL,
                details VARCHAR(500) NOT NULL,
                INDEX idx_activity_logs_occurred_at (occurred_at),
                INDEX idx_activity_logs_action (action)
            );
            """;

        await using MySqlCommand command = new(sql, connection);
        await command.ExecuteNonQueryAsync();
    }

    public async Task AddAsync(string action, string details)
    {
        await InitializeAsync();

        Account? account = AppSession.CurrentAccount;
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            INSERT INTO activity_logs (account_id, username, full_name, action, details)
            VALUES (@account_id, @username, @full_name, @action, @details);
            """;

        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@account_id", account?.Id);
        command.Parameters.AddWithValue("@username", account?.Username ?? "System");
        command.Parameters.AddWithValue("@full_name", account?.FullName ?? "System");
        command.Parameters.AddWithValue("@action", action.Trim());
        command.Parameters.AddWithValue("@details", details.Trim());
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<AuditLogEntry>> GetRecentAsync(string searchText, DateTime fromDate, DateTime toDate)
    {
        await InitializeAsync();

        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT id, occurred_at, username, full_name, action, details
            FROM activity_logs
            WHERE occurred_at >= @from_date
              AND occurred_at < DATE_ADD(@to_date, INTERVAL 1 DAY)
              AND (
                  @search = ''
                  OR username LIKE @pattern
                  OR full_name LIKE @pattern
                  OR action LIKE @pattern
                  OR details LIKE @pattern
              )
            ORDER BY occurred_at DESC, id DESC
            LIMIT 500;
            """;

        string search = searchText.Trim();
        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@from_date", fromDate.Date);
        command.Parameters.AddWithValue("@to_date", toDate.Date);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@pattern", $"%{search}%");

        List<AuditLogEntry> entries = new();
        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            entries.Add(new AuditLogEntry
            {
                Id = reader.GetInt32("id"),
                OccurredAt = reader.GetDateTime("occurred_at"),
                Username = reader.GetString("username"),
                FullName = reader.GetString("full_name"),
                Action = reader.GetString("action"),
                Details = reader.GetString("details")
            });
        }

        return entries;
    }
}
