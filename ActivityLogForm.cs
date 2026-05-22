using System.ComponentModel;

namespace electronics;

public sealed class ActivityLogForm : Form
{
    private readonly AuditLogRepository auditLogRepository = new();
    private readonly BindingList<AuditLogEntry> entries = new();
    private readonly DataGridView grid = new();
    private readonly TextBox txtSearch = new();
    private readonly DateTimePicker dtpFrom = new();
    private readonly DateTimePicker dtpTo = new();
    private readonly Button btnRefresh = new();

    public ActivityLogForm()
    {
        Text = "Activity Log";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(1040, 680);
        MinimumSize = new Size(920, 580);
        BackColor = Color.FromArgb(245, 247, 250);

        BuildLayout();
        Load += async (_, _) => await LoadEntriesAsync();
    }

    private void BuildLayout()
    {
        Label title = new()
        {
            Text = "Activity Log / Audit Trail",
            Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
            Location = new Point(28, 22),
            Size = new Size(360, 40)
        };

        Label searchLabel = new()
        {
            Text = "Search",
            Location = new Point(32, 84),
            Size = new Size(80, 22)
        };

        txtSearch.Location = new Point(32, 108);
        txtSearch.Size = new Size(330, 25);
        txtSearch.PlaceholderText = "User, action, or details";
        txtSearch.TextChanged += async (_, _) => await LoadEntriesAsync();

        Label fromLabel = new() { Text = "From", Location = new Point(386, 84), Size = new Size(80, 22) };
        dtpFrom.Location = new Point(386, 108);
        dtpFrom.Size = new Size(150, 25);
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        dtpFrom.ValueChanged += async (_, _) => await LoadEntriesAsync();

        Label toLabel = new() { Text = "To", Location = new Point(558, 84), Size = new Size(80, 22) };
        dtpTo.Location = new Point(558, 108);
        dtpTo.Size = new Size(150, 25);
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Value = DateTime.Today;
        dtpTo.ValueChanged += async (_, _) => await LoadEntriesAsync();

        btnRefresh.Text = "Refresh";
        btnRefresh.Location = new Point(730, 106);
        btnRefresh.Size = new Size(110, 30);
        btnRefresh.Click += async (_, _) => await LoadEntriesAsync();

        grid.Location = new Point(32, 156);
        grid.Size = new Size(960, 470);
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grid.AutoGenerateColumns = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.BackgroundColor = Color.White;
        grid.DataSource = entries;

        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AuditLogEntry.OccurredAt),
            HeaderText = "Date",
            Width = 150,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" }
        });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(AuditLogEntry.Username), HeaderText = "User", Width = 120 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(AuditLogEntry.FullName), HeaderText = "Name", Width = 180 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(AuditLogEntry.Action), HeaderText = "Action", Width = 150 });
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AuditLogEntry.Details),
            HeaderText = "Details",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        Controls.AddRange(new Control[] { title, searchLabel, txtSearch, fromLabel, dtpFrom, toLabel, dtpTo, btnRefresh, grid });
    }

    private async Task LoadEntriesAsync()
    {
        if (dtpFrom.Value.Date > dtpTo.Value.Date)
        {
            return;
        }

        btnRefresh.Enabled = false;
        try
        {
            List<AuditLogEntry> results = await auditLogRepository.GetRecentAsync(txtSearch.Text, dtpFrom.Value.Date, dtpTo.Value.Date);
            entries.Clear();
            foreach (AuditLogEntry entry in results)
            {
                entries.Add(entry);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to load activity logs.\n\n{ex.Message}", "Activity Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnRefresh.Enabled = true;
        }
    }
}
