using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class NewActivation : ResultEntity
    {
        public int UserId { get; set; }
        public string ActicationCode { get; set; }
    }
}
