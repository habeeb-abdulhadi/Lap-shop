using LapShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LapShop.Bl
{
    public interface IItems
    {
        public List<TbItem> GetAll();
        public List<VwItem> GetAllItemsData(int? categoryId);
        public List<VwItem> GetRecommendedItems(int itemId);

        public TbItem GetById(int id);
        public VwItem GetItemById(int id);
        public TbItem GetItemByIdd(int id);

        public bool Save(TbItem category);
        public bool Delete(int id);
    }
    public class ClsItems : IItems
    {
        LapShopContext context ;

        public ClsItems(LapShopContext ctx) 
        {
            context= ctx;
        }
       
        public List<TbItem> GetAll()
        {
            try
            {
                var lstCategories = context.TbItems.ToList();
                return lstCategories;
            }
            catch
            {
                return new List<TbItem>();

            }

        }
        public List<VwItem> GetAllItemsData(int? categoryId )
        {
            try
            {

                //var query = from d in context.TbItems
                //            join c in context.TbCategories
                //            on d.CategoryId equals c.CategoryId
                //            join it in context.TbItemTypes
                //             on d.ItemTypeId equals it.ItemTypeId
                //            join os in context.TbOs
                //            on d.OsId equals os.OsId
                //            select new VwItem
                //            {
                //                ItemId = d.ItemId
                //            };

                var lstCategories = context.VwItems.Where(a=>(a.CategoryId==categoryId || categoryId==null || categoryId==0)
                && a.CurrentState==1 && !string.IsNullOrEmpty(a.ItemName) ).OrderByDescending(a=>a.CreatedDate).ToList();
                //var lstCategories = context.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0)&&(a.ItemTypeId==ItemType || ItemType==null|| ItemType==0).ToList();

                return lstCategories;
            }
            catch
            {
                return new List<VwItem>();

            }

        }
        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {

                var item=GetById(itemId);
                var lstCategories = context.VwItems.Where(a=> a.SalesPrice>item.SalesPrice-150
                && a.SalesPrice<item.SalesPrice+150
                && a.CurrentState == 1).OrderByDescending(a => a.CreatedDate).ToList();
                //var lstCategories = context.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0)&&(a.ItemTypeId==ItemType || ItemType==null|| ItemType==0).ToList();

                return lstCategories;
            }
            catch
            {
                return new List<VwItem>();

            }

        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState==1);
                return item;
            }
            catch
            {
                return new TbItem();
            }
        }
        public VwItem GetItemById(int id)
        {
            try
            {
                var item = context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new VwItem();
            }
        }
        public TbItem GetItemByIdd(int id)
        {
            try
            {
                var Item = context.TbItems.Where(c => c.ItemId == id).FirstOrDefault();
                return Item;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    item.CurrentState = 1;
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    context.TbItems.Add(item);
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {

                var item = GetById(id);
                item.CurrentState = 0;
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        
    
}
}
