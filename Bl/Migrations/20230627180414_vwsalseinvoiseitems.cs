using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.Migrations
{
    /// <inheritdoc />
    public partial class vwsalseinvoiseitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwSalseInvoicesItem 
as
SELECT        dbo.TbSalesInvoiceItems.InvoiceItemId, dbo.TbSalesInvoiceItems.ItemId, dbo.TbSalesInvoiceItems.InvoiceId, dbo.TbSalesInvoiceItems.Qty, dbo.TbSalesInvoiceItems.InvoicePrice, dbo.TbSalesInvoices.DelivryDate, 
                         dbo.TbSalesInvoices.CustomerId, dbo.TbItems.ItemName, dbo.TbItems.SalesPrice, dbo.TbItems.ImageName
FROM            dbo.TbSalesInvoiceItems INNER JOIN
                         dbo.TbSalesInvoices ON dbo.TbSalesInvoiceItems.InvoiceId = dbo.TbSalesInvoices.InvoiceId INNER JOIN
                         dbo.TbItems ON dbo.TbSalesInvoiceItems.ItemId = dbo.TbItems.ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
