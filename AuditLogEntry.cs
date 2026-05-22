namespace electronics;

public sealed class AuditLogEntry
{
    public int Id { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
