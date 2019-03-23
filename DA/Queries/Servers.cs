using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;
using ErrorCenter.Messages;

namespace DA.Queries
{
    public class Servers
    {
        public static int InsertServerInfo(BE.Entities.Response.ServerInfoResp serverInfo)
        {
            try
            {
                var server = new Entities.Server()
                {
                    Name = serverInfo.Name,
                    Address = serverInfo.Address,
                    Tel = serverInfo.Tel,
                    IsActive = true,
                    CategoryId = serverInfo.CategoryId
                };
                using (var dbCtx = new MSDbContext())
                {

                    dbCtx.Servers.Add(server);
                    int result = dbCtx.SaveChanges();

                    if (result == 1)
                    {
                        return server.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoServersException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
