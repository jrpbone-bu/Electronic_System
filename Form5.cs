using System.ComponentModel;
using System.Globalization;

namespace electronics
{
    public partial class Form5 : Form
    {
        private readonly TransactionRepository transactionRepository = new();
        private readonly AuditLogRepository auditLogRepository = new();
        private readonly BindingList<TransactionReportRow> reportRows = new();
        private readonly DataGridView gridPreview = new();
        private ReportType currentReportType = ReportType.SalesTransaction;

        public Form5()
        {
            InitializeComponent();
            ConfigureReportModule();
        }

        private async void ConfigureReportModule()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.AddRange(new object[] { "Sales Transaction", "Purchase Receiving", "Inventory Count" });
            cmbReportType.SelectedIndex = 0;

            cmbGroupBy.Items.Clear();
            cmbGroupBy.Items.AddRange(new object[] { "Category" });
            cmbGroupBy.SelectedIndex = 0;

            cmbOutput.Items.Clear();
            cmbOutput.Items.AddRange(new object[] { "Data Grid Preview", "Excel Template" });
            cmbOutput.SelectedIndex = 0;

            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Today;

            lblPreparedByValue.Text = AppSession.CurrentAccount?.FullName ?? "System User";
            lblLastRunValue.Text = "Not generated";

            BuildDataGridPreview();

            cmbReportType.SelectedIndexChanged += (_, _) => UpdateOverview();
            dtpFromDate.ValueChanged += (_, _) => UpdateOverview();
            dtpToDate.ValueChanged += (_, _) => UpdateOverview();
            cmbOutput.SelectedIndexChanged += (_, _) => UpdateOverview();
            btnGenerateReport.Click += async (_, _) => await GenerateReportAsync();
            btnExportExcel.Click += (_, _) => ExportExcel();

            UpdateOverview();
            await GenerateReportAsync();
        }

        private void BuildDataGridPreview()
        {
            lvPreview.Visible = false;

            gridPreview.Location = lvPreview.Location;
            gridPreview.Size = lvPreview.Size;
            gridPreview.Anchor = lvPreview.Anchor;
            gridPreview.AutoGenerateColumns = false;
            gridPreview.AllowUserToAddRows = false;
            gridPreview.AllowUserToDeleteRows = false;
            gridPreview.ReadOnly = true;
            gridPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPreview.MultiSelect = false;
            gridPreview.BackgroundColor = Color.White;
            gridPreview.DataSource = reportRows;

            gridPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(TransactionReportRow.TransactionDate),
                HeaderText = "Date",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.ReferenceNo), HeaderText = "Reference", Width = 112 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.TransactionType), HeaderText = "Transaction", Width = 132 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.PartyName), HeaderText = "Party", Width = 150 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.ItemName), HeaderText = "Item", Width = 145 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.Category), HeaderText = "Category", Width = 110 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(TransactionReportRow.Quantity), HeaderText = "Qty", Width = 58 });
            gridPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(TransactionReportRow.Amount),
                HeaderText = "Amount",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            pnlPreview.Controls.Add(gridPreview);
        }

        private async Task GenerateReportAsync()
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("From Date must be earlier than or equal to To Date.", "Report Generator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGenerateReport.Enabled = false;
            try
            {
                await transactionRepository.InitializeAsync();
                currentReportType = GetSelectedReportType();
                List<TransactionReportRow> rows = await transactionRepository.GetReportRowsAsync(currentReportType, dtpFromDate.Value.Date, dtpToDate.Value.Date);

                reportRows.Clear();
                foreach (TransactionReportRow row in rows)
                {
                    reportRows.Add(row);
                }

                UpdateTotals();
                UpdateOverview();
                lblLastRunValue.Text = DateTime.Now.ToString("MMM dd, yyyy HH:mm", CultureInfo.InvariantCulture);
                await LogActivityAsync(
                    "Report Generated",
                    $"{cmbReportType.Text} report generated for {dtpFromDate.Value:yyyy-MM-dd} to {dtpToDate.Value:yyyy-MM-dd}. Rows: {reportRows.Count:N0}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to generate report.\n\n{ex.Message}", "Report Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateReport.Enabled = true;
            }
        }

        private void ExportExcel()
        {
            if (reportRows.Count == 0)
            {
                MessageBox.Show("Generate a report with records before exporting to Excel.", "Report Generator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog saveFileDialog = new()
            {
                Title = "Export Excel Report",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = ExcelReportExporter.GetDefaultFileName(currentReportType)
            };

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                ExcelReportExporter.Export(
                    saveFileDialog.FileName,
                    currentReportType,
                    reportRows.ToList(),
                    AppSession.CurrentAccount?.FullName ?? "System User");
                _ = LogActivityAsync(
                    "Excel Exported",
                    $"{cmbReportType.Text} report exported to {Path.GetFileName(saveFileDialog.FileName)}. Rows: {reportRows.Count:N0}.");
                MessageBox.Show("Excel report template exported successfully.", "Report Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to export Excel report.\n\n{ex.Message}", "Report Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOverview()
        {
            currentReportType = GetSelectedReportType();
            lblSelectedReportValue.Text = cmbReportType.Text;
            lblDateRangeValue.Text = $"{dtpFromDate.Value:MMM dd} - {dtpToDate.Value:MMM dd}";
            lblOutputFormatValue.Text = cmbOutput.Text;
            lblPreparedByValue.Text = AppSession.CurrentAccount?.FullName ?? "System User";
            lblPreviewSubtitle.Text = "The Data Grid below lists the generated transaction records before exporting.";
            lblRecommendedText.Text = "Generate the selected transaction report, then export the Excel template.";
        }

        private void UpdateTotals()
        {
            lblRowsValue.Text = reportRows.Count.ToString("N0", CultureInfo.InvariantCulture);
            lblTotalValueValue.Text = reportRows.Sum(row => row.Amount).ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        }

        private ReportType GetSelectedReportType()
        {
            return cmbReportType.SelectedIndex switch
            {
                1 => ReportType.PurchaseReceiving,
                2 => ReportType.InventoryCount,
                _ => ReportType.SalesTransaction
            };
        }

        private void lvPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async Task LogActivityAsync(string action, string details)
        {
            try
            {
                await auditLogRepository.AddAsync(action, details);
            }
            catch
            {
                // Report generation/export should not fail because of audit logging.
            }
        }
    }
}
