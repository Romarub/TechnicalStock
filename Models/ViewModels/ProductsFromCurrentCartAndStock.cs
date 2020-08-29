using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models.ViewModels
{
    public class ProductsFromCurrentCartAndStock
    {
        public IQueryable<SparePart> StockSpares { get; set; }
        public List<SparePart> CartSpares { get; set; }
    }
}
