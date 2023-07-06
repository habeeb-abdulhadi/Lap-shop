using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.Migrations
{
    /// <inheritdoc />
    public partial class vwnej : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwSalseInvoiceSumm
    as
    select TbSalesInvoices.InvoiceId ,sum(InvoicePrice)as sumprice,sum(Qty)as sumqty
    from TbSalesInvoices join TbSalesInvoiceItems
    on TbSalesInvoices.InvoiceId=TbSalesInvoiceItems.InvoiceId
    group by TbSalesInvoices.InvoiceId
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
