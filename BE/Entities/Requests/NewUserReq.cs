using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Requests
{
    public class NewUserReq
    {
        public NewUserReq(string name, string tel)
        {
            Name = name;
            Tel = tel;
        }

        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
