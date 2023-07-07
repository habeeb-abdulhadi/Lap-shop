using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using LapShop.Bl;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        IItems OclsItems;
        ISliders oClsSliders;
        Icategories oClsCategories;
        public HomeController(IItems item,ISliders slider, Icategories categories)
        {
            OclsItems = item;
            this.oClsSliders = slider;
            this.oClsCategories = categories;
        }
        public IActionResult Index()
        {
            //LapShopContext ctx=new LapShopContext();
            ////var categories= ctx.TbCategories.OrderBy(a=>a.CategoryId).ToList();//
            //var categories = ctx.TbCategories.Where(s=>s.CategoryName.Contains("Ap")) //startswith,,,endwith
            //    .OrderBy(a => a.CategoryId).ToList();
            //categories.Select(s => new TbCategory()
            //{ 
            //    CategoryId = s.CategoryId,
            //    CategoryName = s.CategoryName,
            
            //});

            VmHomePage vm= new VmHomePage();
            vm.lstAllItems =OclsItems.GetAllItemsData(null).Skip(2).Take(18).ToList();
            vm.lstRecommrndedProducts = OclsItems.GetAllItemsData(null).Skip(20).Take(8).ToList();
            vm.lstNewItems = OclsItems.GetAllItemsData(null).Skip(30).Take(10).ToList();
            vm.lstFreeDelivry = OclsItems.GetAllItemsData(null).Skip(40).Take(4).ToList();
            vm.lstSliders = oClsSliders.GetAll();
            vm.lstCategorys = oClsCategories.GetAll().Take(4).ToList();
            return View(vm);
        }
    }
}
