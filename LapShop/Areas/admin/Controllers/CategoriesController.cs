using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using LapShop.Bl;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("admin")]
    public class CategoriesController : Controller
    {
        public CategoriesController(Icategories category)
        {
            oclsCategories=category;
        }
        Icategories oclsCategories ;

        public IActionResult List()
        {

            return View(oclsCategories.GetAll());
        }
    
        public IActionResult Edit(int? categoryId)
        {
            var category = new TbCategory();
           if(categoryId != null)
            {
               category=oclsCategories.GetById(Convert.ToInt32(categoryId));
            }
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Files)
        {
            if(!ModelState.IsValid)
                return View("Edit", category);

            category.ImageName = await Helper.UploadImage(Files,"Categories");
            oclsCategories.Save(category);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int categoryId)
        {
            oclsCategories.Delete(categoryId);
            return RedirectToAction("List");
        }
    }
}
