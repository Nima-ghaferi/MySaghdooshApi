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
        public static ResultList<ServiceProviderGeneralInfoResp> GetMainPageProviders(int pageIndex)
        {
            ResultList<ServiceProviderGeneralInfoResp> result = null;
            List<ServiceProviderGeneralInfoResp> spList = null;
            try
            {
                spList = DA.Queries.Servers.SelectMainPageByPageIndex(pageIndex);
                result = new ResultList<ServiceProviderGeneralInfoResp>(spList, null, true);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message, e);
                result = new ResultList<ServiceProviderGeneralInfoResp>(null, new Error(ErrorMessage.LoadMainServiceProvidersError), false);
            }
            return result;
        }
    }
}
