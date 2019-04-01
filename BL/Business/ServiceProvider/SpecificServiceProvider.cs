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
    public class SpecificServiceProvider
    {
        public static Result<ServiceProviderSpecificResp> GetServiceProvider(int providerId)
        {
            Result<ServiceProviderSpecificResp> result = null;
            try
            {
                ServiceProviderSpecificResp temp = DA.Queries.Servers.SelectServiceProviderById(providerId);
                if (temp != null)
                {
                    result = new Result<ServiceProviderSpecificResp>(temp, null, true);
                }
                else
                {
                    result = new Result<ServiceProviderSpecificResp>(null, new Error(ErrorMessage.ServiceProviderDoesNotExistError), false);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message, e);
                result = new Result<ServiceProviderSpecificResp>(null, new Error(ErrorMessage.LoadServiceProviderSpecsError), false);
            }
            return result;
        }
    }
}
