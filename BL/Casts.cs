using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entities.Request;

namespace BL
{
    public static class Casts
    {
        public static List<CategoryResp> ToBusinessEntity (this List<DA.Entities.Category> categories)
        {
            var beCategories = new List<CategoryResp>();
            foreach (DA.Entities.Category category in categories)
            {
                beCategories.Add((CategoryResp)category);
            }
            return beCategories;
        }
    }
}
