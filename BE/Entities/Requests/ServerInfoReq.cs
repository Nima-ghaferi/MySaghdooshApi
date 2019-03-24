using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Request
{
    public class ServerInfoReq
    {
        public ServerInfoReq(string name, string tel, string address, int categoryId)
        {
            Name = name;
            Tel = tel;
            Address = address;
            CategoryId = categoryId;
        }

        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public int CategoryId { get; set; }
    }
}
