using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Request
{
    public class DMLResultResp : ResultEntity
    {
        public DMLResultResp(int result)
        {
            this.Result = result;
        }
        public int Result { get; set; }
    }
}
