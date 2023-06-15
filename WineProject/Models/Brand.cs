using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public Brand()
        {
            Wines = new List<Wine>();
        }
    }
}