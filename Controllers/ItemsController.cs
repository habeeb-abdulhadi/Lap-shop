using Microsoft.AspNetCore.Mvc;
using LapShop.Bl;
using LapShop.Models;
namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(IItems item, IItemImages oItemImages,Icategories Categorie)
        {
            oItem = item;
            this.oItemImages = oItemImages;
            OCategorie = Categorie;
        }
        IItems oItem;
        IItemImages oItemImages;
        Icategories OCategorie;
        public IActionResult ItemDetails(int id)
        {
            var item=oItem.GetItemById(id);
            VmItemDetails vm=new VmItemDetails();
            vm.Item=item;
            vm.lstRecommenedItems = oItem.GetRecommendedItems(id).Take(20).ToList();
            vm.lstItemImages=oItemImages.GetByItemId(id);
            return View(vm);
        }
        public IActionResult AllItems()
        {

            return View();
        }
        public IActionResult ItemList(int itemId)
        {
            if (itemId == 0)
            {
                TbCategory nu = new TbCategory()
                {
                    CategoryId=0
                };
             
                return View(nu);
                //return   RedirectToAction("AllItems");
            }
            else
            {
                var id = OCategorie.GetById(itemId);
                return View(id);
            }
        }
    }
}
