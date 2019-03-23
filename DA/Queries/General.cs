using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Entities;
using ErrorCenter;
using ErrorCenter.Messages;

namespace DA.Queries
{
    public class General
    {
        public static List<Category> SelectAllCategories()
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    var categories = (from t in dbCtx.Categories
                                      select t).ToList();
                    return categories;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.SelectFromCategoriesException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
