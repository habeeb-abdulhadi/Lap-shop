using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapShop.Models;

public partial class VwSalseInvoiceSuums
{
    public int InvoiceId { get; set; }

    public double Qty { get; set; }

    public decimal Price { get; set; }
}
