using BE;
using BE.Entities.Response;
using ErrorCenter;
using ErrorCenter.Messages;
using System;

namespace BL.Business
{
    public class Server
    {
        public static Result<DMLResult> AddNewServer(ServerInfo serverInfo)
        {
            Result<DMLResult> result;
            try
            {
                int res = DA.Queries.Servers.InsertServerInfo(serverInfo);
                DMLResult resp = new DMLResult(res);
                result = new Result<DMLResult>(resp, null, true);
            }
            catch (Exception)
            {
                result = new Result<DMLResult>(null, new Error(ErrorMessage.AddNewServerInfoError), false);
            }
            return result;
        }
    }
}