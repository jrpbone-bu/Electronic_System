using MySqlConnector;

namespace electronics;

public sealed class TransactionRepository
{
    private readonly DatabaseConnection databaseConnection = new();

    public async Task InitializeAsync()
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        await ExecuteAsync(connection, """
            CREATE TABLE IF NOT EXISTS sales_transactions (
                id INT AUTO_INCREMENT PRIMARY KEY,
                transaction_date DATE NOT NULL,
                reference_no VARCHAR(30) NOT NULL UNIQUE,
                customer_name VARCHAR(120) NOT NULL,
                item_name VARCHAR(120) NOT NULL,
                category VARCHAR(80) NOT NULL,
                quantity INT NOT NULL,
                unit_price DECIMAL(12,2) NOT NULL,
                prepared_by VARCHAR(120) NOT NULL
            );
            """);

        await ExecuteAsync(connection, """
            CREATE TABLE IF NOT EXISTS purchase_transactions (
                id INT AUTO_INCREMENT PRIMARY KEY,
                transaction_date DATE NOT NULL,
                reference_no VARCHAR(30) NOT NULL UNIQUE,
                supplier_name VARCHAR(120) NOT NULL,
                item_name VARCHAR(120) NOT NULL,
                category VARCHAR(80) NOT NULL,
                quantity INT NOT NULL,
                unit_cost DECIMAL(12,2) NOT NULL,
                prepared_by VARCHAR(120) NOT NULL
            );
            """);

        await ExecuteAsync(connection, """
            CREATE TABLE IF NOT EXISTS inventory_counts (
                id INT AUTO_INCREMENT PRIMARY KEY,
                transaction_date DATE NOT NULL,
                reference_no VARCHAR(30) NOT NULL UNIQUE,
                location_name VARCHAR(120) NOT NULL,
                item_name VARCHAR(120) NOT NULL,
                category VARCHAR(80) NOT NULL,
                counted_quantity INT NOT NULL,
                unit_value DECIMAL(12,2) NOT NULL,
                prepared_by VARCHAR(120) NOT NULL
            );
            """);

        await SeedSalesAsync(connection);
        await SeedPurchasesAsync(connection);
        await SeedInventoryCountsAsync(connection);
    }

    public async Task<List<TransactionReportRow>> GetReportRowsAsync(ReportType reportType, DateTime fromDate, DateTime toDate)
    {
        await using MySqlConnection connection = databaseConnection.GetConnection();
        await connection.OpenAsync();

        string sql = reportType switch
        {
            ReportType.SalesTransaction => """
                SELECT transaction_date, reference_no, 'Sales Transaction' transaction_type,
                       customer_name party_name, item_name, category, quantity, unit_price, quantity * unit_price amount, prepared_by
                FROM sales_transactions
                WHERE transaction_date BETWEEN @from_date AND @to_date
                ORDER BY transaction_date, reference_no;
                """,
            ReportType.PurchaseReceiving => """
                SELECT transaction_date, reference_no, 'Purchase Receiving' transaction_type,
                       supplier_name party_name, item_name, category, quantity, unit_cost unit_price, quantity * unit_cost amount, prepared_by
                FROM purchase_transactions
                WHERE transaction_date BETWEEN @from_date AND @to_date
                ORDER BY transaction_date, reference_no;
                """,
            _ => """
                SELECT transaction_date, reference_no, 'Inventory Count' transaction_type,
                       location_name party_name, item_name, category, counted_quantity quantity, unit_value unit_price,
                       counted_quantity * unit_value amount, prepared_by
                FROM inventory_counts
                WHERE transaction_date BETWEEN @from_date AND @to_date
                ORDER BY transaction_date, reference_no;
                """
        };

        await using MySqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@from_date", fromDate.Date);
        command.Parameters.AddWithValue("@to_date", toDate.Date);

        List<TransactionReportRow> rows = new();
        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            rows.Add(new TransactionReportRow
            {
                TransactionDate = reader.GetDateTime("transaction_date"),
                ReferenceNo = reader.GetString("reference_no"),
                TransactionType = reader.GetString("transaction_type"),
                PartyName = reader.GetString("party_name"),
                ItemName = reader.GetString("item_name"),
                Category = reader.GetString("category"),
                Quantity = reader.GetInt32("quantity"),
                UnitPrice = reader.GetDecimal("unit_price"),
                Amount = reader.GetDecimal("amount"),
                PreparedBy = reader.GetString("prepared_by")
            });
        }

        return rows;
    }

    private static async Task ExecuteAsync(MySqlConnection connection, string sql)
    {
        await using MySqlCommand command = new(sql, connection);
        await command.ExecuteNonQueryAsync();
    }

    private static async Task SeedSalesAsync(MySqlConnection connection)
    {
        if (await CountAsync(connection, "sales_transactions") > 0)
        {
            return;
        }

        await ExecuteAsync(connection, """
            INSERT INTO sales_transactions (transaction_date, reference_no, customer_name, item_name, category, quantity, unit_price, prepared_by)
            VALUES
            ('2026-05-01', 'SAL-2026-001', 'Juan Dela Cruz', 'Gaming Laptop', 'Computers', 1, 48500.00, 'System Administrator'),
            ('2026-05-02', 'SAL-2026-002', 'Maria Santos', 'Wireless Router', 'Networking', 2, 3200.00, 'System Administrator'),
            ('2026-05-04', 'SAL-2026-003', 'Northwind Office', '27-inch Monitor', 'Displays', 3, 8900.00, 'System Administrator'),
            ('2026-05-07', 'SAL-2026-004', 'Cebu Repair Hub', 'Mechanical Keyboard', 'Accessories', 5, 2150.00, 'System Administrator'),
            ('2026-05-10', 'SAL-2026-005', 'Ana Reyes', 'Tablet', 'Mobile Devices', 1, 17600.00, 'System Administrator');
            """);
    }

    private static async Task SeedPurchasesAsync(MySqlConnection connection)
    {
        if (await CountAsync(connection, "purchase_transactions") > 0)
        {
            return;
        }

        await ExecuteAsync(connection, """
            INSERT INTO purchase_transactions (transaction_date, reference_no, supplier_name, item_name, category, quantity, unit_cost, prepared_by)
            VALUES
            ('2026-05-01', 'PUR-2026-001', 'TechSource Distribution', 'Gaming Laptop', 'Computers', 4, 40500.00, 'System Administrator'),
            ('2026-05-03', 'PUR-2026-002', 'NetLink Wholesale', 'Wireless Router', 'Networking', 12, 2300.00, 'System Administrator'),
            ('2026-05-05', 'PUR-2026-003', 'DisplayPro Imports', '27-inch Monitor', 'Displays', 8, 6900.00, 'System Administrator'),
            ('2026-05-08', 'PUR-2026-004', 'Accessory Depot', 'Mechanical Keyboard', 'Accessories', 15, 1450.00, 'System Administrator'),
            ('2026-05-11', 'PUR-2026-005', 'MobileOne Supply', 'Tablet', 'Mobile Devices', 6, 13900.00, 'System Administrator');
            """);
    }

    private static async Task SeedInventoryCountsAsync(MySqlConnection connection)
    {
        if (await CountAsync(connection, "inventory_counts") > 0)
        {
            return;
        }

        await ExecuteAsync(connection, """
            INSERT INTO inventory_counts (transaction_date, reference_no, location_name, item_name, category, counted_quantity, unit_value, prepared_by)
            VALUES
            ('2026-05-12', 'CNT-2026-001', 'Main Stockroom', 'Gaming Laptop', 'Computers', 7, 40500.00, 'System Administrator'),
            ('2026-05-12', 'CNT-2026-002', 'Main Stockroom', 'Wireless Router', 'Networking', 22, 2300.00, 'System Administrator'),
            ('2026-05-12', 'CNT-2026-003', 'Display Area', '27-inch Monitor', 'Displays', 11, 6900.00, 'System Administrator'),
            ('2026-05-13', 'CNT-2026-004', 'Display Area', 'Mechanical Keyboard', 'Accessories', 18, 1450.00, 'System Administrator'),
            ('2026-05-13', 'CNT-2026-005', 'Main Stockroom', 'Tablet', 'Mobile Devices', 9, 13900.00, 'System Administrator');
            """);
    }

    private static async Task<long> CountAsync(MySqlConnection connection, string tableName)
    {
        await using MySqlCommand command = new($"SELECT COUNT(*) FROM {tableName};", connection);
        return (long)(await command.ExecuteScalarAsync() ?? 0L);
    }
}
