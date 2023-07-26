namespace LapShop.Models
{
    public class VmItemDetails
    {
        public VmItemDetails() 
        {
            Item=new VwItem();
            lstItemImages=new List<TbItemImage>();
            lstRecommenedItems=new List<VwItem>();
        }
        public VwItem Item { get; set; }
        public List<TbItemImage> lstItemImages { get; set; }
        public List<VwItem> lstRecommenedItems { get; set; }
    }
}
