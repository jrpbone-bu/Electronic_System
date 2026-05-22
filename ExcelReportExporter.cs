using System.Globalization;
using System.IO.Compression;
using System.Security;
using System.Text;

namespace electronics;

public static class ExcelReportExporter
{
    private const string CompanyName = "Electronics Shop Information System";

    public static void Export(string filePath, ReportType reportType, IReadOnlyList<TransactionReportRow> rows, string preparedBy)
    {
        if (rows.Count == 0)
        {
            throw new InvalidOperationException("Generate a report with at least one row before exporting.");
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        string? logoPath = GetLogoPath();
        bool hasLogo = logoPath is not null;

        using ZipArchive archive = ZipFile.Open(filePath, ZipArchiveMode.Create);
        AddText(archive, "[Content_Types].xml", BuildContentTypesXml(hasLogo));
        AddText(archive, "_rels/.rels", RootRelsXml);
        AddText(archive, "xl/workbook.xml", WorkbookXml);
        AddText(archive, "xl/_rels/workbook.xml.rels", WorkbookRelsXml);
        AddText(archive, "xl/styles.xml", StylesXml);
        AddText(archive, "xl/worksheets/sheet1.xml", BuildReportSheetXml(reportType, rows, preparedBy, hasLogo));
        AddText(archive, "xl/worksheets/sheet2.xml", BuildChartSheetXml(reportType, rows));
        if (hasLogo)
        {
            AddText(archive, "xl/worksheets/_rels/sheet1.xml.rels", Sheet1RelsXml);
            AddText(archive, "xl/drawings/drawing2.xml", LogoDrawingXml);
            AddText(archive, "xl/drawings/_rels/drawing2.xml.rels", LogoDrawingRelsXml);
            AddBinary(archive, "xl/media/logo.png", File.ReadAllBytes(logoPath!));
        }

        AddText(archive, "xl/worksheets/_rels/sheet2.xml.rels", Sheet2RelsXml);
        AddText(archive, "xl/drawings/drawing1.xml", DrawingXml);
        AddText(archive, "xl/drawings/_rels/drawing1.xml.rels", DrawingRelsXml);
        AddText(archive, "xl/charts/chart1.xml", BuildChartXml(reportType, rows));
        AddText(archive, "docProps/core.xml", BuildCoreXml(preparedBy));
        AddText(archive, "docProps/app.xml", AppXml);
    }

    public static string GetDefaultFileName(ReportType reportType)
    {
        string slug = GetReportTitle(reportType).Replace(" ", "_", StringComparison.OrdinalIgnoreCase);
        return $"{slug}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
    }

    private static string BuildReportSheetXml(ReportType reportType, IReadOnlyList<TransactionReportRow> rows, string preparedBy, bool hasLogo)
    {
        StringBuilder xml = StartSheet();
        xml.Append("<cols><col min=\"1\" max=\"1\" width=\"14\" customWidth=\"1\"/><col min=\"2\" max=\"2\" width=\"18\" customWidth=\"1\"/><col min=\"3\" max=\"3\" width=\"22\" customWidth=\"1\"/><col min=\"4\" max=\"4\" width=\"24\" customWidth=\"1\"/><col min=\"5\" max=\"5\" width=\"24\" customWidth=\"1\"/><col min=\"6\" max=\"6\" width=\"18\" customWidth=\"1\"/><col min=\"7\" max=\"9\" width=\"14\" customWidth=\"1\"/></cols>");
        xml.Append("<sheetData>");
        AppendRow(xml, 1, new object[] { hasLogo ? "" : "LOGO", CompanyName }, style: 2);
        AppendRow(xml, 2, new object[] { "", GetReportTitle(reportType) }, style: 2);
        AppendRow(xml, 3, new object[] { "", $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}" });
        AppendRow(xml, 4, new object[] { "", $"Prepared by: {preparedBy}" });
        AppendRow(xml, 6, new object[] { "Date", "Reference No.", "Transaction", "Customer/Supplier/Location", "Item", "Category", "Quantity", "Unit Price", "Amount" }, style: 1);

        int rowNumber = 7;
        foreach (TransactionReportRow row in rows)
        {
            AppendRow(xml, rowNumber++, new object[]
            {
                row.TransactionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                row.ReferenceNo,
                row.TransactionType,
                row.PartyName,
                row.ItemName,
                row.Category,
                row.Quantity,
                row.UnitPrice,
                row.Amount
            });
        }

        rowNumber += 2;
        AppendRow(xml, rowNumber++, new object[] { "Prepared By:", preparedBy });
        AppendRow(xml, rowNumber++, new object[] { "Signature:", "____________________________" });
        AppendRow(xml, rowNumber, new object[] { "Date Signed:", "____________________________" });
        xml.Append("</sheetData>");
        xml.Append("<mergeCells count=\"2\"><mergeCell ref=\"B1:I1\"/><mergeCell ref=\"B2:I2\"/></mergeCells>");
        if (hasLogo)
        {
            xml.Append("<drawing r:id=\"rId1\"/>");
        }

        xml.Append("</worksheet>");
        return xml.ToString();
    }

    private static string BuildChartSheetXml(ReportType reportType, IReadOnlyList<TransactionReportRow> rows)
    {
        List<(string Label, decimal Value)> summary = BuildSummary(reportType, rows);

        StringBuilder xml = StartSheet();
        xml.Append("<cols><col min=\"1\" max=\"1\" width=\"24\" customWidth=\"1\"/><col min=\"2\" max=\"2\" width=\"16\" customWidth=\"1\"/></cols>");
        xml.Append("<sheetData>");
        AppendRow(xml, 1, new object[] { CompanyName }, style: 2);
        AppendRow(xml, 2, new object[] { $"{GetReportTitle(reportType)} Graph" }, style: 2);
        AppendRow(xml, 4, new object[] { "Category", GetMetricLabel(reportType) }, style: 1);

        int rowNumber = 5;
        foreach ((string label, decimal value) in summary)
        {
            AppendRow(xml, rowNumber++, new object[] { label, value });
        }

        xml.Append("</sheetData>");
        xml.Append("<drawing r:id=\"rId1\"/>");
        xml.Append("</worksheet>");
        return xml.ToString();
    }

    private static string BuildChartXml(ReportType reportType, IReadOnlyList<TransactionReportRow> rows)
    {
        int lastSummaryRow = BuildSummary(reportType, rows).Count + 4;
        string metricLabel = Xml(GetMetricLabel(reportType));
        string title = Xml($"{GetReportTitle(reportType)} by Category");

        return $$"""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <c:chartSpace xmlns:c="http://schemas.openxmlformats.org/drawingml/2006/chart" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
              <c:chart>
                <c:title><c:tx><c:rich><a:bodyPr/><a:lstStyle/><a:p><a:r><a:t>{{title}}</a:t></a:r></a:p></c:rich></c:tx><c:overlay val="0"/></c:title>
                <c:plotArea>
                  <c:layout/>
                  <c:barChart>
                    <c:barDir val="col"/>
                    <c:grouping val="clustered"/>
                    <c:ser>
                      <c:idx val="0"/><c:order val="0"/>
                      <c:tx><c:strRef><c:f>Chart!$B$4</c:f><c:strCache><c:ptCount val="1"/><c:pt idx="0"><c:v>{{metricLabel}}</c:v></c:pt></c:strCache></c:strRef></c:tx>
                      <c:cat><c:strRef><c:f>Chart!$A$5:$A${{lastSummaryRow}}</c:f></c:strRef></c:cat>
                      <c:val><c:numRef><c:f>Chart!$B$5:$B${{lastSummaryRow}}</c:f></c:numRef></c:val>
                    </c:ser>
                    <c:axId val="48650112"/><c:axId val="48672768"/>
                  </c:barChart>
                  <c:catAx><c:axId val="48650112"/><c:scaling><c:orientation val="minMax"/></c:scaling><c:axPos val="b"/><c:tickLblPos val="nextTo"/><c:crossAx val="48672768"/><c:crosses val="autoZero"/></c:catAx>
                  <c:valAx><c:axId val="48672768"/><c:scaling><c:orientation val="minMax"/></c:scaling><c:axPos val="l"/><c:majorGridlines/><c:tickLblPos val="nextTo"/><c:crossAx val="48650112"/><c:crosses val="autoZero"/></c:valAx>
                </c:plotArea>
                <c:legend><c:legendPos val="r"/><c:layout/></c:legend>
                <c:plotVisOnly val="1"/>
              </c:chart>
            </c:chartSpace>
            """;
    }

    private static List<(string Label, decimal Value)> BuildSummary(ReportType reportType, IReadOnlyList<TransactionReportRow> rows)
    {
        return rows
            .GroupBy(row => row.Category)
            .Select(group => (group.Key, reportType == ReportType.InventoryCount
                ? group.Sum(row => (decimal)row.Quantity)
                : group.Sum(row => row.Amount)))
            .OrderByDescending(item => item.Item2)
            .ToList();
    }

    private static StringBuilder StartSheet()
    {
        return new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">");
    }

    private static void AppendRow(StringBuilder xml, int rowNumber, object[] values, int? style = null)
    {
        xml.Append(CultureInfo.InvariantCulture, $"<row r=\"{rowNumber}\">");
        for (int index = 0; index < values.Length; index++)
        {
            string cellReference = $"{GetColumnName(index + 1)}{rowNumber}";
            AppendCell(xml, cellReference, values[index], style);
        }

        xml.Append("</row>");
    }

    private static void AppendCell(StringBuilder xml, string cellReference, object value, int? style)
    {
        string styleAttribute = style is null ? string.Empty : $" s=\"{style.Value}\"";
        switch (value)
        {
            case int intValue:
                xml.Append(CultureInfo.InvariantCulture, $"<c r=\"{cellReference}\"{styleAttribute}><v>{intValue}</v></c>");
                break;
            case decimal decimalValue:
                xml.Append(CultureInfo.InvariantCulture, $"<c r=\"{cellReference}\"{styleAttribute}><v>{decimalValue}</v></c>");
                break;
            default:
                xml.Append(CultureInfo.InvariantCulture, $"<c r=\"{cellReference}\" t=\"inlineStr\"{styleAttribute}><is><t>{Xml(value?.ToString() ?? string.Empty)}</t></is></c>");
                break;
        }
    }

    private static string GetColumnName(int columnNumber)
    {
        StringBuilder columnName = new();
        while (columnNumber > 0)
        {
            int modulo = (columnNumber - 1) % 26;
            columnName.Insert(0, (char)('A' + modulo));
            columnNumber = (columnNumber - modulo) / 26;
        }

        return columnName.ToString();
    }

    private static string GetReportTitle(ReportType reportType)
    {
        return reportType switch
        {
            ReportType.SalesTransaction => "Sales Transaction Report",
            ReportType.PurchaseReceiving => "Purchase Receiving Report",
            _ => "Inventory Count Report"
        };
    }

    private static string GetMetricLabel(ReportType reportType)
    {
        return reportType == ReportType.InventoryCount ? "Counted Quantity" : "Amount";
    }

    private static void AddText(ZipArchive archive, string path, string content)
    {
        ZipArchiveEntry entry = archive.CreateEntry(path, CompressionLevel.Optimal);
        using StreamWriter writer = new(entry.Open(), new UTF8Encoding(false));
        writer.Write(content);
    }

    private static void AddBinary(ZipArchive archive, string path, byte[] content)
    {
        ZipArchiveEntry entry = archive.CreateEntry(path, CompressionLevel.Optimal);
        using Stream stream = entry.Open();
        stream.Write(content);
    }

    private static string? GetLogoPath()
    {
        string outputLogo = Path.Combine(AppContext.BaseDirectory, "logo.png");
        if (File.Exists(outputLogo))
        {
            return outputLogo;
        }

        string projectLogo = Path.Combine(Directory.GetCurrentDirectory(), "logo.png");
        return File.Exists(projectLogo) ? projectLogo : null;
    }

    private static string Xml(string value)
    {
        return SecurityElement.Escape(value) ?? string.Empty;
    }

    private static string BuildCoreXml(string preparedBy)
    {
        string now = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        return $$"""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <cp:coreProperties xmlns:cp="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
              <dc:creator>{{Xml(preparedBy)}}</dc:creator>
              <cp:lastModifiedBy>{{Xml(preparedBy)}}</cp:lastModifiedBy>
              <dcterms:created xsi:type="dcterms:W3CDTF">{{now}}</dcterms:created>
              <dcterms:modified xsi:type="dcterms:W3CDTF">{{now}}</dcterms:modified>
            </cp:coreProperties>
            """;
    }

    private static string BuildContentTypesXml(bool hasLogo)
    {
        string logoTypes = hasLogo
            ? """
              <Default Extension="png" ContentType="image/png"/>
              <Override PartName="/xl/drawings/drawing2.xml" ContentType="application/vnd.openxmlformats-officedocument.drawing+xml"/>
              """
            : string.Empty;

        return $$"""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
              <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
              <Default Extension="xml" ContentType="application/xml"/>
              {{logoTypes}}
              <Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/>
              <Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>
              <Override PartName="/xl/worksheets/sheet2.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>
              <Override PartName="/xl/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml"/>
              <Override PartName="/xl/drawings/drawing1.xml" ContentType="application/vnd.openxmlformats-officedocument.drawing+xml"/>
              <Override PartName="/xl/charts/chart1.xml" ContentType="application/vnd.openxmlformats-officedocument.drawingml.chart+xml"/>
              <Override PartName="/docProps/core.xml" ContentType="application/vnd.openxmlformats-package.core-properties+xml"/>
              <Override PartName="/docProps/app.xml" ContentType="application/vnd.openxmlformats-officedocument.extended-properties+xml"/>
            </Types>
            """;
    }

    private const string RootRelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/>
          <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties" Target="docProps/core.xml"/>
          <Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties" Target="docProps/app.xml"/>
        </Relationships>
        """;

    private const string WorkbookXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
          <sheets>
            <sheet name="Report" sheetId="1" r:id="rId1"/>
            <sheet name="Chart" sheetId="2" r:id="rId2"/>
          </sheets>
        </workbook>
        """;

    private const string WorkbookRelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/>
          <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet2.xml"/>
          <Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/>
        </Relationships>
        """;

    private const string StylesXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">
          <fonts count="3"><font><sz val="11"/><name val="Calibri"/></font><font><b/><sz val="11"/><name val="Calibri"/></font><font><b/><sz val="16"/><name val="Calibri"/></font></fonts>
          <fills count="3"><fill><patternFill patternType="none"/></fill><fill><patternFill patternType="gray125"/></fill><fill><patternFill patternType="solid"><fgColor rgb="FFD9EAF7"/><bgColor indexed="64"/></patternFill></fill></fills>
          <borders count="1"><border><left/><right/><top/><bottom/><diagonal/></border></borders>
          <cellStyleXfs count="1"><xf numFmtId="0" fontId="0" fillId="0" borderId="0"/></cellStyleXfs>
          <cellXfs count="3"><xf numFmtId="0" fontId="0" fillId="0" borderId="0" xfId="0"/><xf numFmtId="0" fontId="1" fillId="2" borderId="0" xfId="0" applyFont="1" applyFill="1"/><xf numFmtId="0" fontId="2" fillId="0" borderId="0" xfId="0" applyFont="1"/></cellXfs>
          <cellStyles count="1"><cellStyle name="Normal" xfId="0" builtinId="0"/></cellStyles>
        </styleSheet>
        """;

    private const string Sheet1RelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing" Target="../drawings/drawing2.xml"/>
        </Relationships>
        """;

    private const string Sheet2RelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing" Target="../drawings/drawing1.xml"/>
        </Relationships>
        """;

    private const string DrawingXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <xdr:wsDr xmlns:xdr="http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
          <xdr:twoCellAnchor>
            <xdr:from><xdr:col>3</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>3</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:from>
            <xdr:to><xdr:col>10</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>20</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:to>
            <xdr:graphicFrame macro="">
              <xdr:nvGraphicFramePr><xdr:cNvPr id="2" name="Chart 1"/><xdr:cNvGraphicFramePr/></xdr:nvGraphicFramePr>
              <xdr:xfrm><a:off x="0" y="0"/><a:ext cx="0" cy="0"/></xdr:xfrm>
              <a:graphic><a:graphicData uri="http://schemas.openxmlformats.org/drawingml/2006/chart"><c:chart xmlns:c="http://schemas.openxmlformats.org/drawingml/2006/chart" r:id="rId1"/></a:graphicData></a:graphic>
            </xdr:graphicFrame>
            <xdr:clientData/>
          </xdr:twoCellAnchor>
        </xdr:wsDr>
        """;

    private const string DrawingRelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart" Target="../charts/chart1.xml"/>
        </Relationships>
        """;

    private const string LogoDrawingXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <xdr:wsDr xmlns:xdr="http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
          <xdr:twoCellAnchor editAs="oneCell">
            <xdr:from><xdr:col>0</xdr:col><xdr:colOff>95250</xdr:colOff><xdr:row>0</xdr:row><xdr:rowOff>47625</xdr:rowOff></xdr:from>
            <xdr:to><xdr:col>1</xdr:col><xdr:colOff>381000</xdr:colOff><xdr:row>3</xdr:row><xdr:rowOff>95250</xdr:rowOff></xdr:to>
            <xdr:pic>
              <xdr:nvPicPr><xdr:cNvPr id="3" name="Company Logo"/><xdr:cNvPicPr/></xdr:nvPicPr>
              <xdr:blipFill><a:blip r:embed="rId1"/><a:stretch><a:fillRect/></a:stretch></xdr:blipFill>
              <xdr:spPr><a:prstGeom prst="rect"><a:avLst/></a:prstGeom></xdr:spPr>
            </xdr:pic>
            <xdr:clientData/>
          </xdr:twoCellAnchor>
        </xdr:wsDr>
        """;

    private const string LogoDrawingRelsXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/image" Target="../media/logo.png"/>
        </Relationships>
        """;

    private const string AppXml = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Properties xmlns="http://schemas.openxmlformats.org/officeDocument/2006/extended-properties" xmlns:vt="http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes">
          <Application>Electronics Shop Information System</Application>
        </Properties>
        """;
}
