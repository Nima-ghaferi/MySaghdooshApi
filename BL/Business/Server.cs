using BE;
using BE.Entities.Response;
using ErrorCenter;
using ErrorCenter.Messages;
using System;

namespace BL.Business
{
    public class Server
    {
        public static Result<DMLResultResp> AddNewServer(ServerInfoResp serverInfo)
        {
            Result<DMLResultResp> result;
            try
            {
                int res = DA.Queries.Servers.InsertServerInfo(serverInfo);
                DMLResultResp resp = new DMLResultResp(res);
                result = new Result<DMLResultResp>(resp, null, true);
            }
            catch (Exception)
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.AddNewServerInfoError), false);
            }
            return result;
        }
    }
}