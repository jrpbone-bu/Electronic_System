using MySqlConnector;

namespace electronics;

public class DatabaseConnection
{
    private const string Server = "localhost";
    private const string Database = "electronicshop_db";
    private const string UserId = "root";
    private const string Password = "QRRQHZKRZLOOEH";

    public string ConnectionString { get; } =
        $"Server={Server};Database={Database};User ID={UserId};Password={Password};SslMode=None;Allow User Variables=True;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}
