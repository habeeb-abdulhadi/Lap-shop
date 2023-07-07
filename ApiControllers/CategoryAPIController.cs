using LapShop.Bl;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShop.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        Icategories oClsCategories;
        public CategoryAPIController(Icategories categories)
        {

            this.oClsCategories = categories;
        }
        // GET: api/<CategoryAPIController>
        [HttpGet]
        [Route("/api/GetAllCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                var catList = oClsCategories.GetAll().ToList();

                return Ok(catList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please Try again later");
            }
        }
    }
}
