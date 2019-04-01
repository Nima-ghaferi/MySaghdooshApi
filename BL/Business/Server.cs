using BE;
using BE.Entities.Request;
using BE.Entities.Response;
using BL.Business.Utility;
using ErrorCenter;
using ErrorCenter.Messages;
using System;

namespace BL.Business
{
    public class Server
    {
        public static Result<DMLResultResp> AddNewServer(ServerInfoReq serverInfo)
        {
            Result<DMLResultResp> result;
            try
            {
                int res = DA.Queries.Servers.InsertServerInfo(serverInfo);
                DMLResultResp resp = new DMLResultResp(res);
                result = new Result<DMLResultResp>(resp, null, true);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.AddNewServerInfoError), false);
            }
            return result;
        }
    }
}