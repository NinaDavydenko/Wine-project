using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class CartItem
    {
        [Key]
        public int WineId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}