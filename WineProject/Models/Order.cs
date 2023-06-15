using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Sum { get; set; }
        public int Discount { get; set; }
        public double Total { get; set; }
        public DateTime BuyingDay { get; set; }
        public string Address { get; set; }
        public string ListGoods { get; set; }

        public string Id_Customer { get; set; }
        [ForeignKey("Id_Customer")]
        public ApplicationUser Customer { get; set; }
    }
}