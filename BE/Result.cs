using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;

namespace BE
{
    public class Result<T> where T : ResultEntity
    {
        public T Response { get; }
        public Error Error { get; }
        public bool IsOk { get; }

        public Result(T response, Error error, bool isOk)
        {
            this.Response = response;
            this.Error = error;
            this.IsOk = isOk;
        }
    }
    public class ResultList<T> where T : ResultEntity
    {
        public List<T> Response { get; }
        public Error Error { get; }
        public bool IsOk { get; }

        public ResultList(List<T> response, Error error, bool isOk)
        {
            this.Response = response;
            this.Error = error;
            this.IsOk = isOk;
        }
    }


}