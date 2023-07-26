namespace LapShop.Models
{
    public class ShoppingCartItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ImageName { get; set; }
       public int qty { get; set; }
        public decimal price { get; set; }
        public decimal Total { get; set; }

    }
}
