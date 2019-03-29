using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Response
{
    public class ServiceProviderGeneralInfo : ResultEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PrimaryPhoto { get; set; }
    }

    public class ServiceProviderGeneralInfoList : ResultEntity
    {
        public ServiceProviderGeneralInfo serviceProviderList { get; set; }
    }
}
