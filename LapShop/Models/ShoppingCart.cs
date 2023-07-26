namespace LapShop.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            lstItems= new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> lstItems { get; set; }
        public ShoppingCartItem itemmm { get; set; }
        public ShippingInfo shippingInfo { get; set; }

        public decimal Total { get; set; }
        public int qty { get; set; }

        public string promoCode { get; set; }
    }
}
