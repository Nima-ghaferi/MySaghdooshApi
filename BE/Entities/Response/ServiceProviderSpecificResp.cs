using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Response
{
    public class ServiceProviderSpecificResp : ServiceProviderGeneralInfoResp
    {
        public string Tel { get; set; }
        public string Address { get; set; }
        public List<string> PhotoList { get; set; }
    }
}
