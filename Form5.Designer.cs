namespace electronics
{
    partial class Form5
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblFiltersTitle;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblGroupBy;
        private System.Windows.Forms.ComboBox cmbGroupBy;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ComboBox cmbOutput;
        private System.Windows.Forms.Label lblFiltersHelper;
        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.Label lblOverviewTitle;
        private System.Windows.Forms.Panel pnlSelectedReport;
        private System.Windows.Forms.Label lblSelectedReportTitle;
        private System.Windows.Forms.Label lblSelectedReportValue;
        private System.Windows.Forms.Panel pnlDateRange;
        private System.Windows.Forms.Label lblDateRangeTitle;
        private System.Windows.Forms.Label lblDateRangeValue;
        private System.Windows.Forms.Panel pnlOutputFormat;
        private System.Windows.Forms.Label lblOutputFormatTitle;
        private System.Windows.Forms.Label lblOutputFormatValue;
        private System.Windows.Forms.Panel pnlPreparedBy;
        private System.Windows.Forms.Label lblPreparedByTitle;
        private System.Windows.Forms.Label lblPreparedByValue;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Label lblPreviewSubtitle;
        private System.Windows.Forms.ListView lvPreview;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colReference;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblRowsTitle;
        private System.Windows.Forms.Label lblRowsValue;
        private System.Windows.Forms.Label lblTotalValueTitle;
        private System.Windows.Forms.Label lblTotalValueValue;
        private System.Windows.Forms.Label lblLastRunTitle;
        private System.Windows.Forms.Label lblLastRunValue;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.Label lblActionsSubtitle;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Panel pnlRecommended;
        private System.Windows.Forms.Label lblRecommendedTitle;
        private System.Windows.Forms.Label lblRecommendedText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "04/01/2026", "ORD-001", "Sales", "Laptop order", "25000" }, -1);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "04/03/2026", "ORD-002", "Sales", "Router order", "3200" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "04/05/2026", "INV-008", "Inventory", "Monitor restock", "18000" }, -1);
            ListViewItem listViewItem4 = new ListViewItem(new string[] { "04/07/2026", "SUP-004", "Supplier", "Tablet delivery", "52000" }, -1);
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlFilters = new Panel();
            lblFiltersTitle = new Label();
            lblReportType = new Label();
            cmbReportType = new ComboBox();
            lblFromDate = new Label();
            dtpFromDate = new DateTimePicker();
            lblToDate = new Label();
            dtpToDate = new DateTimePicker();
            lblGroupBy = new Label();
            cmbGroupBy = new ComboBox();
            lblOutput = new Label();
            cmbOutput = new ComboBox();
            lblFiltersHelper = new Label();
            pnlOverview = new Panel();
            lblOverviewTitle = new Label();
            pnlSelectedReport = new Panel();
            lblSelectedReportTitle = new Label();
            lblSelectedReportValue = new Label();
            pnlDateRange = new Panel();
            lblDateRangeTitle = new Label();
            lblDateRangeValue = new Label();
            pnlOutputFormat = new Panel();
            lblOutputFormatTitle = new Label();
            lblOutputFormatValue = new Label();
            pnlPreparedBy = new Panel();
            lblPreparedByTitle = new Label();
            lblPreparedByValue = new Label();
            pnlPreview = new Panel();
            lblPreviewTitle = new Label();
            lblPreviewSubtitle = new Label();
            lvPreview = new ListView();
            colDate = new ColumnHeader();
            colReference = new ColumnHeader();
            colCategory = new ColumnHeader();
            colDescription = new ColumnHeader();
            colAmount = new ColumnHeader();
            pnlTotals = new Panel();
            lblRowsTitle = new Label();
            lblRowsValue = new Label();
            lblTotalValueTitle = new Label();
            lblTotalValueValue = new Label();
            lblLastRunTitle = new Label();
            lblLastRunValue = new Label();
            pnlActions = new Panel();
            lblActionsTitle = new Label();
            lblActionsSubtitle = new Label();
            btnGenerateReport = new Button();
            btnExportExcel = new Button();
            pnlRecommended = new Panel();
            lblRecommendedTitle = new Label();
            lblRecommendedText = new Label();
            pnlFilters.SuspendLayout();
            pnlOverview.SuspendLayout();
            pnlSelectedReport.SuspendLayout();
            pnlDateRange.SuspendLayout();
            pnlOutputFormat.SuspendLayout();
            pnlPreparedBy.SuspendLayout();
            pnlPreview.SuspendLayout();
            pnlTotals.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlRecommended.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(32, 43, 76);
            lblTitle.Location = new Point(36, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(320, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Report Generator";
            // 
            // lblSubtitle
            // 
            lblSubtitle.ForeColor = Color.DimGray;
            lblSubtitle.Location = new Point(40, 68);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(680, 24);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Build inventory, sales, and supplier reports with filters, previews, and export-ready output.";
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.White;
            pnlFilters.BorderStyle = BorderStyle.FixedSingle;
            pnlFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlFilters.Controls.Add(lblFiltersTitle);
            pnlFilters.Controls.Add(lblReportType);
            pnlFilters.Controls.Add(cmbReportType);
            pnlFilters.Controls.Add(lblFromDate);
            pnlFilters.Controls.Add(dtpFromDate);
            pnlFilters.Controls.Add(lblToDate);
            pnlFilters.Controls.Add(dtpToDate);
            pnlFilters.Controls.Add(lblGroupBy);
            pnlFilters.Controls.Add(cmbGroupBy);
            pnlFilters.Controls.Add(lblOutput);
            pnlFilters.Controls.Add(cmbOutput);
            pnlFilters.Controls.Add(lblFiltersHelper);
            pnlFilters.Location = new Point(36, 110);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Size = new Size(1108, 176);
            pnlFilters.TabIndex = 2;
            // 
            // lblFiltersTitle
            // 
            lblFiltersTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFiltersTitle.Location = new Point(22, 16);
            lblFiltersTitle.Name = "lblFiltersTitle";
            lblFiltersTitle.Size = new Size(180, 28);
            lblFiltersTitle.TabIndex = 0;
            lblFiltersTitle.Text = "Report Filters";
            // 
            // lblReportType
            // 
            lblReportType.Location = new Point(24, 58);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(120, 22);
            lblReportType.TabIndex = 1;
            lblReportType.Text = "Report Type";
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Items.AddRange(new object[] { "Inventory Summary", "Daily Sales", "Supplier Activity", "Customer Orders" });
            cmbReportType.Location = new Point(24, 86);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(260, 25);
            cmbReportType.TabIndex = 2;
            // 
            // lblFromDate
            // 
            lblFromDate.Location = new Point(314, 58);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(100, 22);
            lblFromDate.TabIndex = 3;
            lblFromDate.Text = "From Date";
            // 
            // dtpFromDate
            // 
            dtpFromDate.Format = DateTimePickerFormat.Short;
            dtpFromDate.Location = new Point(314, 86);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(180, 25);
            dtpFromDate.TabIndex = 4;
            // 
            // lblToDate
            // 
            lblToDate.Location = new Point(522, 58);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(100, 22);
            lblToDate.TabIndex = 5;
            lblToDate.Text = "To Date";
            // 
            // dtpToDate
            // 
            dtpToDate.Format = DateTimePickerFormat.Short;
            dtpToDate.Location = new Point(522, 86);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(180, 25);
            dtpToDate.TabIndex = 6;
            // 
            // lblGroupBy
            // 
            lblGroupBy.Location = new Point(730, 58);
            lblGroupBy.Name = "lblGroupBy";
            lblGroupBy.Size = new Size(90, 22);
            lblGroupBy.TabIndex = 7;
            lblGroupBy.Text = "Group By";
            // 
            // cmbGroupBy
            // 
            cmbGroupBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroupBy.FormattingEnabled = true;
            cmbGroupBy.Items.AddRange(new object[] { "Day", "Week", "Month", "Category", "Supplier" });
            cmbGroupBy.Location = new Point(730, 86);
            cmbGroupBy.Name = "cmbGroupBy";
            cmbGroupBy.Size = new Size(160, 25);
            cmbGroupBy.TabIndex = 8;
            // 
            // lblOutput
            // 
            lblOutput.Location = new Point(918, 58);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(90, 22);
            lblOutput.TabIndex = 9;
            lblOutput.Text = "Output";
            // 
            // cmbOutput
            // 
            cmbOutput.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOutput.FormattingEnabled = true;
            cmbOutput.Items.AddRange(new object[] { "On-screen Preview", "Excel-ready" });
            cmbOutput.Location = new Point(918, 86);
            cmbOutput.Name = "cmbOutput";
            cmbOutput.Size = new Size(160, 25);
            cmbOutput.TabIndex = 10;
            // 
            // lblFiltersHelper
            // 
            lblFiltersHelper.ForeColor = Color.DimGray;
            lblFiltersHelper.Location = new Point(24, 132);
            lblFiltersHelper.Name = "lblFiltersHelper";
            lblFiltersHelper.Size = new Size(520, 24);
            lblFiltersHelper.TabIndex = 11;
            lblFiltersHelper.Text = "Use the filters above to narrow the dataset before generating the final report output.";
            // 
            // pnlOverview
            // 
            pnlOverview.BackColor = Color.White;
            pnlOverview.BorderStyle = BorderStyle.FixedSingle;
            pnlOverview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlOverview.Controls.Add(lblOverviewTitle);
            pnlOverview.Controls.Add(pnlSelectedReport);
            pnlOverview.Controls.Add(pnlDateRange);
            pnlOverview.Controls.Add(pnlOutputFormat);
            pnlOverview.Controls.Add(pnlPreparedBy);
            pnlOverview.Location = new Point(36, 308);
            pnlOverview.Name = "pnlOverview";
            pnlOverview.Size = new Size(250, 446);
            pnlOverview.TabIndex = 3;
            // 
            // lblOverviewTitle
            // 
            lblOverviewTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverviewTitle.Location = new Point(18, 18);
            lblOverviewTitle.Name = "lblOverviewTitle";
            lblOverviewTitle.Size = new Size(180, 28);
            lblOverviewTitle.TabIndex = 0;
            lblOverviewTitle.Text = "Report Overview";
            // 
            // pnlSelectedReport
            // 
            pnlSelectedReport.BackColor = Color.FromArgb(249, 250, 252);
            pnlSelectedReport.BorderStyle = BorderStyle.FixedSingle;
            pnlSelectedReport.Controls.Add(lblSelectedReportTitle);
            pnlSelectedReport.Controls.Add(lblSelectedReportValue);
            pnlSelectedReport.Location = new Point(18, 58);
            pnlSelectedReport.Name = "pnlSelectedReport";
            pnlSelectedReport.Size = new Size(214, 76);
            pnlSelectedReport.TabIndex = 1;
            // 
            // lblSelectedReportTitle
            // 
            lblSelectedReportTitle.ForeColor = Color.DimGray;
            lblSelectedReportTitle.Location = new Point(14, 12);
            lblSelectedReportTitle.Name = "lblSelectedReportTitle";
            lblSelectedReportTitle.Size = new Size(140, 20);
            lblSelectedReportTitle.TabIndex = 0;
            lblSelectedReportTitle.Text = "Selected Report";
            // 
            // lblSelectedReportValue
            // 
            lblSelectedReportValue.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedReportValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblSelectedReportValue.Location = new Point(14, 36);
            lblSelectedReportValue.Name = "lblSelectedReportValue";
            lblSelectedReportValue.Size = new Size(180, 24);
            lblSelectedReportValue.TabIndex = 1;
            lblSelectedReportValue.Text = "Inventory Summary";
            // 
            // pnlDateRange
            // 
            pnlDateRange.BackColor = Color.FromArgb(249, 250, 252);
            pnlDateRange.BorderStyle = BorderStyle.FixedSingle;
            pnlDateRange.Controls.Add(lblDateRangeTitle);
            pnlDateRange.Controls.Add(lblDateRangeValue);
            pnlDateRange.Location = new Point(18, 152);
            pnlDateRange.Name = "pnlDateRange";
            pnlDateRange.Size = new Size(214, 76);
            pnlDateRange.TabIndex = 2;
            // 
            // lblDateRangeTitle
            // 
            lblDateRangeTitle.ForeColor = Color.DimGray;
            lblDateRangeTitle.Location = new Point(14, 12);
            lblDateRangeTitle.Name = "lblDateRangeTitle";
            lblDateRangeTitle.Size = new Size(140, 20);
            lblDateRangeTitle.TabIndex = 0;
            lblDateRangeTitle.Text = "Date Range";
            // 
            // lblDateRangeValue
            // 
            lblDateRangeValue.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDateRangeValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblDateRangeValue.Location = new Point(14, 36);
            lblDateRangeValue.Name = "lblDateRangeValue";
            lblDateRangeValue.Size = new Size(180, 24);
            lblDateRangeValue.TabIndex = 1;
            lblDateRangeValue.Text = "Jan 01 - Jan 31";
            // 
            // pnlOutputFormat
            // 
            pnlOutputFormat.BackColor = Color.FromArgb(249, 250, 252);
            pnlOutputFormat.BorderStyle = BorderStyle.FixedSingle;
            pnlOutputFormat.Controls.Add(lblOutputFormatTitle);
            pnlOutputFormat.Controls.Add(lblOutputFormatValue);
            pnlOutputFormat.Location = new Point(18, 246);
            pnlOutputFormat.Name = "pnlOutputFormat";
            pnlOutputFormat.Size = new Size(214, 76);
            pnlOutputFormat.TabIndex = 3;
            // 
            // lblOutputFormatTitle
            // 
            lblOutputFormatTitle.ForeColor = Color.DimGray;
            lblOutputFormatTitle.Location = new Point(14, 12);
            lblOutputFormatTitle.Name = "lblOutputFormatTitle";
            lblOutputFormatTitle.Size = new Size(140, 20);
            lblOutputFormatTitle.TabIndex = 0;
            lblOutputFormatTitle.Text = "Output Format";
            // 
            // lblOutputFormatValue
            // 
            lblOutputFormatValue.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOutputFormatValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblOutputFormatValue.Location = new Point(14, 36);
            lblOutputFormatValue.Name = "lblOutputFormatValue";
            lblOutputFormatValue.Size = new Size(180, 24);
            lblOutputFormatValue.TabIndex = 1;
            lblOutputFormatValue.Text = "On-screen Preview";
            // 
            // pnlPreparedBy
            // 
            pnlPreparedBy.BackColor = Color.FromArgb(249, 250, 252);
            pnlPreparedBy.BorderStyle = BorderStyle.FixedSingle;
            pnlPreparedBy.Controls.Add(lblPreparedByTitle);
            pnlPreparedBy.Controls.Add(lblPreparedByValue);
            pnlPreparedBy.Location = new Point(18, 340);
            pnlPreparedBy.Name = "pnlPreparedBy";
            pnlPreparedBy.Size = new Size(214, 76);
            pnlPreparedBy.TabIndex = 4;
            // 
            // lblPreparedByTitle
            // 
            lblPreparedByTitle.ForeColor = Color.DimGray;
            lblPreparedByTitle.Location = new Point(14, 12);
            lblPreparedByTitle.Name = "lblPreparedByTitle";
            lblPreparedByTitle.Size = new Size(140, 20);
            lblPreparedByTitle.TabIndex = 0;
            lblPreparedByTitle.Text = "Prepared By";
            // 
            // lblPreparedByValue
            // 
            lblPreparedByValue.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPreparedByValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblPreparedByValue.Location = new Point(14, 36);
            lblPreparedByValue.Name = "lblPreparedByValue";
            lblPreparedByValue.Size = new Size(180, 24);
            lblPreparedByValue.TabIndex = 1;
            lblPreparedByValue.Text = "System User";
            // 
            // pnlPreview
            // 
            pnlPreview.BackColor = Color.White;
            pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPreview.Controls.Add(lblPreviewTitle);
            pnlPreview.Controls.Add(lblPreviewSubtitle);
            pnlPreview.Controls.Add(lvPreview);
            pnlPreview.Controls.Add(pnlTotals);
            pnlPreview.Location = new Point(310, 308);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(572, 446);
            pnlPreview.TabIndex = 4;
            // 
            // lblPreviewTitle
            // 
            lblPreviewTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPreviewTitle.Location = new Point(18, 18);
            lblPreviewTitle.Name = "lblPreviewTitle";
            lblPreviewTitle.Size = new Size(180, 28);
            lblPreviewTitle.TabIndex = 0;
            lblPreviewTitle.Text = "Preview Table";
            // 
            // lblPreviewSubtitle
            // 
            lblPreviewSubtitle.ForeColor = Color.DimGray;
            lblPreviewSubtitle.Location = new Point(18, 48);
            lblPreviewSubtitle.Name = "lblPreviewSubtitle";
            lblPreviewSubtitle.Size = new Size(420, 22);
            lblPreviewSubtitle.TabIndex = 1;
            lblPreviewSubtitle.Text = "The grid below represents how the generated report can look before exporting.";
            // 
            // lvPreview
            // 
            lvPreview.Columns.AddRange(new ColumnHeader[] { colDate, colReference, colCategory, colDescription, colAmount });
            lvPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvPreview.FullRowSelect = true;
            lvPreview.GridLines = true;
            lvPreview.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4 });
            lvPreview.Location = new Point(18, 86);
            lvPreview.MultiSelect = false;
            lvPreview.Name = "lvPreview";
            lvPreview.Size = new Size(536, 250);
            lvPreview.TabIndex = 2;
            lvPreview.UseCompatibleStateImageBehavior = false;
            lvPreview.View = View.Details;
            lvPreview.SelectedIndexChanged += lvPreview_SelectedIndexChanged;
            // 
            // colDate
            // 
            colDate.Text = "Date";
            colDate.Width = 90;
            // 
            // colReference
            // 
            colReference.Text = "Reference";
            colReference.Width = 90;
            // 
            // colCategory
            // 
            colCategory.Text = "Category";
            colCategory.Width = 120;
            // 
            // colDescription
            // 
            colDescription.Text = "Description";
            colDescription.Width = 150;
            // 
            // colAmount
            // 
            colAmount.Text = "Amount";
            colAmount.Width = 80;
            // 
            // pnlTotals
            // 
            pnlTotals.BackColor = Color.FromArgb(249, 250, 252);
            pnlTotals.BorderStyle = BorderStyle.FixedSingle;
            pnlTotals.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTotals.Controls.Add(lblRowsTitle);
            pnlTotals.Controls.Add(lblRowsValue);
            pnlTotals.Controls.Add(lblTotalValueTitle);
            pnlTotals.Controls.Add(lblTotalValueValue);
            pnlTotals.Controls.Add(lblLastRunTitle);
            pnlTotals.Controls.Add(lblLastRunValue);
            pnlTotals.Location = new Point(18, 354);
            pnlTotals.Name = "pnlTotals";
            pnlTotals.Size = new Size(536, 72);
            pnlTotals.TabIndex = 3;
            // 
            // lblRowsTitle
            // 
            lblRowsTitle.ForeColor = Color.DimGray;
            lblRowsTitle.Location = new Point(18, 12);
            lblRowsTitle.Name = "lblRowsTitle";
            lblRowsTitle.Size = new Size(120, 20);
            lblRowsTitle.TabIndex = 0;
            lblRowsTitle.Text = "Rows";
            // 
            // lblRowsValue
            // 
            lblRowsValue.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRowsValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblRowsValue.Location = new Point(18, 34);
            lblRowsValue.Name = "lblRowsValue";
            lblRowsValue.Size = new Size(140, 24);
            lblRowsValue.TabIndex = 1;
            lblRowsValue.Text = "124";
            // 
            // lblTotalValueTitle
            // 
            lblTotalValueTitle.ForeColor = Color.DimGray;
            lblTotalValueTitle.Location = new Point(188, 12);
            lblTotalValueTitle.Name = "lblTotalValueTitle";
            lblTotalValueTitle.Size = new Size(120, 20);
            lblTotalValueTitle.TabIndex = 2;
            lblTotalValueTitle.Text = "Total Value";
            // 
            // lblTotalValueValue
            // 
            lblTotalValueValue.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalValueValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblTotalValueValue.Location = new Point(188, 34);
            lblTotalValueValue.Name = "lblTotalValueValue";
            lblTotalValueValue.Size = new Size(140, 24);
            lblTotalValueValue.TabIndex = 3;
            lblTotalValueValue.Text = "PHP 115,089";
            // 
            // lblLastRunTitle
            // 
            lblLastRunTitle.ForeColor = Color.DimGray;
            lblLastRunTitle.Location = new Point(388, 12);
            lblLastRunTitle.Name = "lblLastRunTitle";
            lblLastRunTitle.Size = new Size(120, 20);
            lblLastRunTitle.TabIndex = 4;
            lblLastRunTitle.Text = "Last Run";
            // 
            // lblLastRunValue
            // 
            lblLastRunValue.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLastRunValue.ForeColor = Color.FromArgb(32, 43, 76);
            lblLastRunValue.Location = new Point(388, 34);
            lblLastRunValue.Name = "lblLastRunValue";
            lblLastRunValue.Size = new Size(120, 24);
            lblLastRunValue.TabIndex = 5;
            lblLastRunValue.Text = "Today";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.BorderStyle = BorderStyle.FixedSingle;
            pnlActions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlActions.Controls.Add(lblActionsTitle);
            pnlActions.Controls.Add(lblActionsSubtitle);
            pnlActions.Controls.Add(btnGenerateReport);
            pnlActions.Controls.Add(btnExportExcel);
            pnlActions.Controls.Add(pnlRecommended);
            pnlActions.Location = new Point(906, 308);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(238, 446);
            pnlActions.TabIndex = 5;
            // 
            // lblActionsTitle
            // 
            lblActionsTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActionsTitle.Location = new Point(18, 18);
            lblActionsTitle.Name = "lblActionsTitle";
            lblActionsTitle.Size = new Size(140, 28);
            lblActionsTitle.TabIndex = 0;
            lblActionsTitle.Text = "Actions";
            // 
            // lblActionsSubtitle
            // 
            lblActionsSubtitle.ForeColor = Color.DimGray;
            lblActionsSubtitle.Location = new Point(18, 50);
            lblActionsSubtitle.Name = "lblActionsSubtitle";
            lblActionsSubtitle.Size = new Size(185, 40);
            lblActionsSubtitle.TabIndex = 1;
            lblActionsSubtitle.Text = "Choose what to do with the report after reviewing the preview.";
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.BackColor = Color.FromArgb(26, 115, 232);
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.Location = new Point(18, 108);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Padding = new Padding(10, 0, 10, 2);
            btnGenerateReport.Size = new Size(198, 48);
            btnGenerateReport.TabIndex = 2;
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.UseVisualStyleBackColor = false;
            // 
            // btnExportExcel
            // 
            btnExportExcel.BackColor = Color.White;
            btnExportExcel.FlatStyle = FlatStyle.Flat;
            btnExportExcel.ForeColor = Color.FromArgb(32, 43, 76);
            btnExportExcel.Location = new Point(18, 168);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Padding = new Padding(10, 0, 10, 2);
            btnExportExcel.Size = new Size(198, 48);
            btnExportExcel.TabIndex = 3;
            btnExportExcel.Text = "Export as Excel";
            btnExportExcel.UseVisualStyleBackColor = false;
            // 
            // pnlRecommended
            // 
            pnlRecommended.BackColor = Color.FromArgb(249, 250, 252);
            pnlRecommended.BorderStyle = BorderStyle.FixedSingle;
            pnlRecommended.Controls.Add(lblRecommendedTitle);
            pnlRecommended.Controls.Add(lblRecommendedText);
            pnlRecommended.Location = new Point(18, 250);
            pnlRecommended.Name = "pnlRecommended";
            pnlRecommended.Size = new Size(198, 86);
            pnlRecommended.TabIndex = 6;
            // 
            // lblRecommendedTitle
            // 
            lblRecommendedTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRecommendedTitle.Location = new Point(12, 10);
            lblRecommendedTitle.Name = "lblRecommendedTitle";
            lblRecommendedTitle.Size = new Size(160, 20);
            lblRecommendedTitle.TabIndex = 0;
            lblRecommendedTitle.Text = "Recommended";
            // 
            // lblRecommendedText
            // 
            lblRecommendedText.ForeColor = Color.DimGray;
            lblRecommendedText.Location = new Point(12, 30);
            lblRecommendedText.Name = "lblRecommendedText";
            lblRecommendedText.Size = new Size(174, 44);
            lblRecommendedText.TabIndex = 1;
            lblRecommendedText.Text = "Generate first, review the preview, then export the final file.";
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1180, 820);
            Controls.Add(pnlActions);
            Controls.Add(pnlPreview);
            Controls.Add(pnlOverview);
            Controls.Add(pnlFilters);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimumSize = new Size(1180, 820);
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report Generator";
            WindowState = FormWindowState.Maximized;
            pnlFilters.ResumeLayout(false);
            pnlOverview.ResumeLayout(false);
            pnlSelectedReport.ResumeLayout(false);
            pnlDateRange.ResumeLayout(false);
            pnlOutputFormat.ResumeLayout(false);
            pnlPreparedBy.ResumeLayout(false);
            pnlPreview.ResumeLayout(false);
            pnlTotals.ResumeLayout(false);
            pnlActions.ResumeLayout(false);
            pnlRecommended.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
