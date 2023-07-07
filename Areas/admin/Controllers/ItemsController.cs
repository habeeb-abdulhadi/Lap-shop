using Microsoft.AspNetCore.Mvc;
using LapShop.Bl;
using LapShop.Models;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin,data entry")]
    [Area("admin")]
    public class ItemsController : Controller
    {
        public ItemsController(IItems item,Icategories category,IItemType itemTypes,Ios ios) 
        {
            OClsOs= ios;
            OClsItemTypes = itemTypes;
            OClsItems=item;
            OClsCategories=category;
        }
        IItems OClsItems;
        Icategories OClsCategories;
        IItemType OClsItemTypes ;
        Ios OClsOs;
       // [AllowAnonymous] //مسموح الدخول
        public IActionResult List()
        {
            ViewBag.lstCategories = OClsCategories.GetAll();
            var items=OClsItems.GetAllItemsData(null);
            return View(items);
        }
        public IActionResult Search(int id)
        {
            ViewBag.lstCategories = OClsCategories.GetAll();
            var items = OClsItems.GetAllItemsData(id);
            return View("List",items);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            var item = new Models.TbItem();
            List<TbItemType> tbtyp = new List<TbItemType>();
            tbtyp= OClsItemTypes.GetAll();
            ViewBag.lstCategories =OClsCategories.GetAll();
            ViewBag.lstItemTypes = tbtyp;
            ViewBag.lstOs=OClsOs.GetAll();
            if (itemId != null)
            {
                 item = OClsItems.GetById(Convert.ToInt32(itemId));
            }

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);

            item.ImageName = await Helper.UploadImage(Files, "Items");
            OClsItems.Save(item);

            return RedirectToAction("List");
        }
        public IActionResult Deletee(int itemId)
        {
            OClsItems.Delete(itemId);
            return RedirectToAction("List");
        }
    }
}
