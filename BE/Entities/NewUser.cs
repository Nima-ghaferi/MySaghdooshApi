using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities
{
    public class NewUser : ResultEntity
    {
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
