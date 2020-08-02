using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(SparePart sparePart, int quantity)
        {
            CartLine line = Lines.Where(p => p.SparePart.SparePartId == sparePart.SparePartId).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    SparePart = sparePart,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(SparePart sparePart) => 
            Lines.RemoveAll(l => l.SparePart.SparePartId == sparePart.SparePartId);

        public virtual void Clear() => Lines.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public SparePart SparePart { get; set; }
        public int Quantity { get; set; }
    }
}

