using BE;
using BE.Entities.Response;
using BL.Business.Utility;
using ErrorCenter;
using ErrorCenter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business.ServiceProvider
{
    public class MainPageServiceProviders
    {
        public static ResultList<ServiceProviderGeneralInfo> GetMainPageProviders(int pageIndex)
        {
            ResultList<ServiceProviderGeneralInfo> result = null;
            List<ServiceProviderGeneralInfo> spList = null;
            try
            {
                spList = DA.Queries.Servers.SelectMainPageByPageIndex(pageIndex);
                result = new ResultList<ServiceProviderGeneralInfo>(spList, null, true);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message, e);
                result = new ResultList<ServiceProviderGeneralInfo>(null, new Error(ErrorMessage.LoadMainServiceProvidersError), false);
            }
            return result;
        }
    }
}
