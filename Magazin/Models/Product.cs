using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazin.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
    }
}
