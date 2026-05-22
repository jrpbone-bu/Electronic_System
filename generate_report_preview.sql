USE electronicshop_db;

-- Match the Generate Reports preview table.
-- Change these values before running the query:
--   @report_type: 'Sales Transaction', 'Purchase Receiving', 'Inventory Count', or 'All'
--   @from_date / @to_date: inclusive date range
SET @report_type = 'All';
SET @from_date = '2026-05-01';
SET @to_date = '2026-05-31';

SELECT
    report_rows.`Date`,
    report_rows.`Reference`,
    report_rows.`Transaction`,
    report_rows.`Party`,
    report_rows.`Item`,
    report_rows.`Category`,
    report_rows.`Qty`,
    report_rows.`Amount`
FROM (
    SELECT
        transaction_date AS `Date`,
        reference_no AS `Reference`,
        'Sales Transaction' AS `Transaction`,
        customer_name AS `Party`,
        item_name AS `Item`,
        category AS `Category`,
        quantity AS `Qty`,
        quantity * unit_price AS `Amount`
    FROM sales_transactions
    WHERE transaction_date BETWEEN @from_date AND @to_date

    UNION ALL

    SELECT
        transaction_date AS `Date`,
        reference_no AS `Reference`,
        'Purchase Receiving' AS `Transaction`,
        supplier_name AS `Party`,
        item_name AS `Item`,
        category AS `Category`,
        quantity AS `Qty`,
        quantity * unit_cost AS `Amount`
    FROM purchase_transactions
    WHERE transaction_date BETWEEN @from_date AND @to_date

    UNION ALL

    SELECT
        transaction_date AS `Date`,
        reference_no AS `Reference`,
        'Inventory Count' AS `Transaction`,
        location_name AS `Party`,
        item_name AS `Item`,
        category AS `Category`,
        counted_quantity AS `Qty`,
        counted_quantity * unit_value AS `Amount`
    FROM inventory_counts
    WHERE transaction_date BETWEEN @from_date AND @to_date
) AS report_rows
WHERE @report_type = 'All'
   OR report_rows.`Transaction` = @report_type
ORDER BY report_rows.`Date`, report_rows.`Reference`;
