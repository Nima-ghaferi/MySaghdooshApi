using System;
using System.Collections.Generic;
using BE;
using BE.Entities.Response;
using ErrorCenter;
using ErrorCenter.Messages;

namespace BL.Business
{
    public class General
    {
        public static ResultList<Category> GetAllCategories()
        {
            ResultList<Category> result;
            try
            {                
                var categories = DA.Queries.General.SelectAllCategories();
                List<Category> resp = new List<Category>();
                resp = categories.ToBusinessEntity();
                result = new ResultList<Category>(resp, null, true);                
            }
            catch (Exception)
            {
                result = new ResultList<Category>(null, new Error(ErrorMessage.LoadCategoriesError), false);
            }
            return result;
        }
    }
}
