using LapShop.Models;
using Microsoft.AspNetCore.Http;

namespace LapShop.Bl
{
    public interface ISliders
    {
        public List<TbSlider> GetAll();
        public TbSlider GetById(int id);
        public bool Save(TbSlider category);
        public bool Delete(int id);


    }
    public class ClsSliders : ISliders
    {
        LapShopContext context;

        public ClsSliders(LapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbSlider> GetAll() 
        {
            try
            {
            var lstCategories = context.TbSliders.Where(a=>a.CurrentState==1).ToList(); 
                return lstCategories;
            }
            catch
            {
                return new List<TbSlider>();

            }
           
        }
        public TbSlider GetById(int id)
        {
            try
            {
               var Os = context.TbSliders.FirstOrDefault(a => a.SliderId == id && a.CurrentState==1);
                return Os;
            }
            catch
            {
                return new TbSlider();
            }
        }
        public bool Save(TbSlider Os)
        {
            try
            {
                if (Os.SliderId == 0)
                {
                    Os.CreatedBy = "1";
                    Os.CreatedDate = DateTime.Now;
                    context.TbSliders.Add(Os);
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
