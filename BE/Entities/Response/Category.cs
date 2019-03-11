using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Response
{
    public class Category : ResultEntity
    {
        public Category(int id , string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
