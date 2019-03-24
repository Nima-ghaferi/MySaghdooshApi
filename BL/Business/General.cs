using System;
using System.Collections.Generic;
using BE;
using BE.Entities.Request;
using BL.Business.Utility;
using ErrorCenter;
using ErrorCenter.Messages;

namespace BL.Business
{
    public class General
    {
        public static ResultList<CategoryResp> GetAllCategories()
        {
            ResultList<CategoryResp> result;
            try
            {                
                var categories = DA.Queries.General.SelectAllCategories();
                List<CategoryResp> resp = new List<CategoryResp>();
                resp = categories.ToBusinessEntity();
                result = new ResultList<CategoryResp>(resp, null, true);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result = new ResultList<CategoryResp>(null, new Error(ErrorMessage.LoadCategoriesError), false);
            }
            return result;
        }
    }
}
