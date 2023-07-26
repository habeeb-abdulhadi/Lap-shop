using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using LapShop.Bl;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        IItems itemService;
        UserManager<ApplicationUser> _userManager;
        ISalesInvoice salesInvoiceSarvice;
        ISalesInvoiceItems _SalesInvoiceItems;
        public OrderController(IItems itemService, UserManager<ApplicationUser> userManager,
            ISalesInvoice ssalesInvoiceService,ISalesInvoiceItems salesInvoiceItems)
        {
            this.itemService = itemService;
            _userManager = userManager;
            salesInvoiceSarvice = ssalesInvoiceService;
            _SalesInvoiceItems = salesInvoiceItems;
        }
        public IActionResult Cart()
        {
            string sessionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sessionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
           
                return View(cart);
            
               
        }
        public ShoppingCart GetShoppingCart()
        {

            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            if (cart != null)
                return cart;
            else
                return null;

        }
        [Authorize]
        public IActionResult CheckOut()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            if (cart.qty > 0)
                return View(cart);
            else
                return View( null);
        }

      
        [Authorize] //لازم تكون مسجل دخول عشان يفوت
        public async Task<IActionResult> OrderSuccess(ShoppingCart ob)
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            await SaveOrder(cart, ob);
            HttpContext.Response.Cookies.Delete("Cart");

            return View();
        }



        //public IActionResult AddToCart(int id)
        //{
        //    ShoppingCart cart;
        //    if (HttpContext.Request.Cookies["Cart"] != null)
        //        cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);

        //    else
        //        cart = new ShoppingCart();

        //    var item = itemService.GetById(id);

        //    var itemList = cart.lstItems.Where(a => a.ItemId == id).FirstOrDefault();
        //    if (itemList != null)
        //    {
        //        itemList.qty++;
        //        itemList.Total = itemList.qty * itemList.price;
        //    }
        //    else
        //    {
        //        cart.lstItems.Add(new ShoppingCartItem
        //        {
        //            ItemId = item.ItemId,
        //            ImageName = item.ImageName,
        //            ItemName = item.ItemName,
        //            price = item.SalesPrice,
        //            qty = 1,
        //            Total = item.SalesPrice

        //        });
        //    }
        //    cart.Total = cart.lstItems.Sum(a => a.Total);
        //    cart.qty = cart.lstItems.Sum(a => a.qty);

        //    HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

        //    // HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));   فقط على مستوى الجلسه اذا خرج تفقد التخزين
        //    return RedirectToAction("GetShoppingCart");
        //}



        public IActionResult AddToCart(int id, int? qty)
        {
            ShoppingCart cart;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);

            else
                cart = new ShoppingCart();

            var item = itemService.GetById(id);

            var itemList = cart.lstItems.Where(a => a.ItemId == id).FirstOrDefault();
            if (itemList != null)
            {
                if (qty != null && qty <= 0)
                {
                    cart.lstItems.Remove(itemList);
                }
                else
                {
                    itemList.qty = (qty == null) ? itemList.qty + 1 : (int)qty;
                    itemList.Total = itemList.qty * itemList.price;
                }
            }
            else
            {
                cart.lstItems.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ImageName = item.ImageName,
                    ItemName = item.ItemName,
                    price = item.SalesPrice,
                    qty = 1,
                    Total = item.SalesPrice

                });
            }
            cart.Total = cart.lstItems.Sum(a => a.Total);
            cart.qty = cart.lstItems.Sum(a => a.qty);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            // HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));   فقط على مستوى الجلسه اذا خرج تفقد التخزين
            return RedirectToAction("GetShoppingCart");
        }


        public bool RemoveFromCart(int id)
        {
            ShoppingCart cart;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);

            else
                cart = new ShoppingCart();

            var item = itemService.GetById(id);

            var itemList = cart.lstItems.Where(a => a.ItemId == id).FirstOrDefault();
            if (itemList != null)
            {
                cart.lstItems.Remove(itemList);
            }
            else
            {
                return false;
            }
            cart.Total = cart.lstItems.Sum(a => a.Total);
            cart.qty = cart.lstItems.Sum(a => a.qty);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            // HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));   فقط على مستوى الجلسه اذا خرج تفقد التخزين
            return true;
        }

        async Task SaveOrder(ShoppingCart oShopingCart, ShoppingCart ob)
        {

            var user = await _userManager.GetUserAsync(User);

            ShippingInfo oShippingInfo = new ShippingInfo()
            {
                shippingInfoId = ob.shippingInfo.shippingInfoId,
                userId = user.Id,
                firstName = ob.shippingInfo.firstName,
                lastName = ob.shippingInfo.lastName,
                phoneNo = ob.shippingInfo.phoneNo,
                Country = ob.shippingInfo.Country,
                city = ob.shippingInfo.city,
                Address = ob.shippingInfo.Address,
                DeptNo = ob.shippingInfo.DeptNo
            };
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShopingCart.lstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.qty,
                        InvoicePrice = item.price * item.qty
                    });
                }

                user = await _userManager.GetUserAsync(User);

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                salesInvoiceSarvice.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IActionResult> MyOrders()
        {
            CheckoutPageViewModel salse = new CheckoutPageViewModel();
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var salseInvoiceItems = salesInvoiceSarvice.GetAllInvoisById(userId);
            var salseInvoice = salesInvoiceSarvice.GetByUserId(userId);
            List<VwSalseInvoiceSuumss> sum = new List<VwSalseInvoiceSuumss>();
            foreach(var invid in salseInvoice)
            {
            var salseInvoiceSum = salesInvoiceSarvice.GetAllInvoicesum(invid.InvoiceId);
                sum.AddRange(salseInvoiceSum);
            }


            salse.qty = salseInvoiceItems.Sum(a => Convert.ToInt32(a.Qty));
            salse.total = salseInvoiceItems.Sum(a => a.InvoicePrice);

            var orderDetails = new List<CheckoutPageViewModel>();
            if(salseInvoiceItems.Count > 0)
            { 
            orderDetails.Add(new CheckoutPageViewModel()
            {

             VwList=salseInvoiceItems,
             salseInvois=salseInvoice,
             salseInvoisSum=sum,
                qty = salse.qty,
                total = salse.total,

            });
            }
            if (orderDetails.Count > 0 )
            return View(orderDetails);
            else
            {
                return View(null);
            }
        }

        //public async Task<IActionResult> MyOrders()
        //{
        //    CheckoutPageViewModel salse = new CheckoutPageViewModel();
        //    var user = await _userManager.GetUserAsync(User);
        //    var userId = user.Id;
        //    var salinvUserId = salesInvoiceSarvice.GetByUserID(userId).LastOrDefault();
        //    List<TbSalesInvoiceItem> invoiceItems = new List<TbSalesInvoiceItem>();

        //    invoiceItems = _SalesInvoiceItems.GetSalesInvoiceId(salinvUserId.InvoiceId);


        //    salse.salesInvoiceItems = invoiceItems;
        //    List<ShoppingCartItem> lstItem = new List<ShoppingCartItem>();
        //    foreach (var item in salse.salesInvoiceItems)
        //    {
        //        item.Item = itemService.GetById(item.ItemId);

        //        lstItem.Add(new ShoppingCartItem
        //        {
        //            ItemId = item.Item.ItemId,
        //            ImageName = item.Item.ImageName,
        //            ItemName = item.Item.ItemName,
        //            price = item.Item.SalesPrice,
        //            qty = Convert.ToInt32(item.Qty),
        //            Total = item.InvoicePrice

        //        });

        //    }
        //    salse.qty = salse.salesInvoiceItems.Sum(a => Convert.ToInt32(a.Qty));
        //    salse.total = salse.salesInvoiceItems.Sum(a => a.InvoicePrice);

        //    var orderDetails = new List<CheckoutPageViewModel>();

        //    orderDetails.Add(new CheckoutPageViewModel()
        //    {

        //        items = lstItem,
        //        salesInvoice = salinvUserId,
        //        salesInvoiceItems = invoiceItems,
        //        vwsalesInvoice = salse.vwsalesInvoice,
        //        qty = salse.qty,
        //        total = salse.total,

        //    });

        //    return View(orderDetails);
        //}

       
    }

}
