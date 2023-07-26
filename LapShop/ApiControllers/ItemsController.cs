using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShop.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        IItems oItem;
        public ItemsController(IItems items)
        {
            oItem = items;
        
        }

        // GET: api/<ItemsController>
        /// <summary>
        /// get all data from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("ItemList")]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            oApiResponse.Data = oItem.GetAll();
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";
            return oApiResponse;
        }

        // GET api/<ItemsController>/5
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ItemList/{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            oApiResponse.Data = oItem.GetById(id);
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";
            return oApiResponse;
        }
        [HttpGet("GetByCategoryId/{categoryId}")]
        public ApiResponse GetByCategoryId(int categoryId)
        {
            ApiResponse oApiResponse = new ApiResponse();
            oApiResponse.Data = oItem.GetAllItemsData(categoryId);
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";
            return oApiResponse;
        }

        // POST api/<ItemsController>
        [HttpPost]
        public ApiResponse Post([FromBody] TbItem item)
        {
            try
            {
            oItem.Save(item);
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
                return oApiResponse;
            }
            catch(Exception ex) 
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";
                return oApiResponse;

            }
        }

        [HttpPost]
        [Route("Delete")]
        public void Delete([FromBody] int id)
        {
            oItem.Delete(id);
        }

      
    }
}
