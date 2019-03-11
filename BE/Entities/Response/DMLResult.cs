using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Response
{
    public class DMLResult : ResultEntity
    {
        public DMLResult(int result)
        {
            this.Result = result;
        }
        public int Result { get; set; }
    }
}
