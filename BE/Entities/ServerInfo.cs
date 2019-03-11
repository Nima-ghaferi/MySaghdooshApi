using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class ServerInfo : ResultEntity
    {
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public int CategoryId { get; set; }
    }
}
