namespace electronics;

public sealed class TransactionReportRow
{
    public DateTime TransactionDate { get; set; }
    public string ReferenceNo { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public string PartyName { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
    public string PreparedBy { get; set; } = string.Empty;
}
