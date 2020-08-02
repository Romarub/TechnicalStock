using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models
{
    public interface IStockRepository
    {
        IQueryable<SparePart> SpareParts { get; }
        public void AddNewItem(SparePart newPart);
    }
}
