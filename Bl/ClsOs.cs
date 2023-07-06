using LapShop.Models;
using Microsoft.AspNetCore.Http;

namespace LapShop.Bl
{
    public interface Ios
    {
        public List<TbO> GetAll();
        public TbO GetById(int id);
        public bool Save(TbO category);
        public bool Delete(int id);


    }
    public class ClsOs : Ios
    {
        LapShopContext context;

        public ClsOs(LapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbO> GetAll() 
        {
            try
            {
            var lstCategories = context.TbOs.Where(a=>a.CurrentState==1).ToList(); 
                return lstCategories;
            }
            catch
            {
                return new List<TbO>();

            }
           
        }
        public TbO GetById(int id)
        {
            try
            {
               var Os = context.TbOs.FirstOrDefault(a => a.OsId == id && a.CurrentState==1);
                return Os;
            }
            catch
            {
                return new TbO();
            }
        }
        public bool Save(TbO Os)
        {
            try
            {
                if (Os.OsId == 0)
                {
                    Os.CreatedBy = "1";
                    Os.CreatedDate = DateTime.Now;
                    context.TbOs.Add(Os);
                }
                else
                {
                    Os.UpdatedBy = "1";
                    Os.UpdatedDate = DateTime.Now;
                    context.Entry(Os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

                var Os = GetById(id);
                Os.CurrentState = 0;
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
