using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LapShop.Models;

namespace LapShop.Models
{
    public class CheckoutPageViewModel
    {
        public List<VwSalseInvoicesItems> VwList { get; set; }
        public List<TbSalesInvoice> salseInvois { get; set; }
        public List<VwSalseInvoiceSuumss> salseInvoisSum { get; set; }

        public int qty { get; set; }
        public decimal total { get; set; }


    }
}