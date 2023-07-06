﻿using LapShop.Models;
using Microsoft.AspNetCore.Http;

namespace LapShop.Bl
{
    public interface IItemType
    {
        public List<TbItemType> GetAll();
        public TbItemType GetById(int id);
        public bool Save(TbItemType category);
        public bool Delete(int id);


    }
    public class ClsItemTypes : IItemType
    {
        LapShopContext context;

        public ClsItemTypes(LapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbItemType> GetAll() 
        {
            try
            {
            var lstItemTypes = context.TbItemTypes.Where(a=>a.CurrentState==1).ToList(); 
                return lstItemTypes;
            }
            catch
            {
                return new List<TbItemType>();

            }
           
        }
        public TbItemType GetById(int id)
        {
            try
            {
               var itemType = context.TbItemTypes.FirstOrDefault(a => a.ItemTypeId == id && a.CurrentState == 1);
                return itemType;
            }
            catch
            {
                return new TbItemType();
            }
        }
        public bool Save(TbItemType itemType)
        {
            try
            {
                if (itemType.ItemTypeId == 0)
                {
                    itemType.CreatedBy = "1";
                    itemType.CreatedDate = DateTime.Now;
                    context.TbItemTypes.Add(itemType);
                }
                else
                {
                    itemType.UpdatedBy = "1";
                    itemType.UpdatedDate = DateTime.Now;
                    context.Entry(itemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

                var itemType = GetById(id);
                itemType.CurrentState = 0;
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
