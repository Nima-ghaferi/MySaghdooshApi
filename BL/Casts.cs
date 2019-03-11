using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entities.Response;

namespace BL
{
    public static class Casts
    {
        public static List<Category> ToBusinessEntity (this List<DA.Entities.Category> categories)
        {
            var beCategories = new List<Category>();
            foreach (DA.Entities.Category category in categories)
            {
                beCategories.Add((Category)category);
            }
            return beCategories;
        }
    }
}
