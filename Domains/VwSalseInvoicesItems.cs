using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

      namespace LapShop.Models;

 public partial class VwSalseInvoicesItems
{
    public int InvoiceItemId { get; set; }

    public int ItemId { get; set; }

    public int InvoiceId { get; set; }

    public double Qty { get; set; }

    public decimal InvoicePrice { get; set; }
    public DateTime DelivryDate { get; set; }
    public Guid CustomerId { get; set; }
    public string ItemName { get; set; } = null!;
    public string? ImageName { get; set; }
    public decimal SalesPrice { get; set; }

}
