using MercatikaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercatikaApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public float Price { get; set; }
        public Category CategoryCode { get; set; } = new();

        public List<ProductDetail> ProductDetails { get; set; } = new();
    }
}