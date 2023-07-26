
using Domains;

namespace LapShop.Models
{
    public class VmHomePage
    {
        public  VmHomePage()
        {
            lstAllItems=new List<VwItem>();
            lstRecommrndedProducts=new List<VwItem>();
            lstNewItems=new List<VwItem>();
            lstFreeDelivry=new List<VwItem>();
            lstCategorys=new List<TbCategory>();
        }
        public List<VwItem> lstAllItems { get; set; }
        public List<VwItem> lstRecommrndedProducts { get; set; }
        public List<VwItem> lstNewItems { get; set; }
        public List<VwItem> lstFreeDelivry { get; set; }
        public List<TbCategory> lstCategorys { get; set; }
        public List<TbSlider> lstSliders { get; set; }
        public TbSettings Settings { get; set; }
    }
}
