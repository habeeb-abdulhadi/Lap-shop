using LapShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LapShop.Models
{
    public class ShippingInfo
    {
        public int shippingInfoId { get; set; }

        [Required]
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        [Required]
        public int phoneNo { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string Address { get; set; }
        public int DeptNo { get; set; }
    }
}