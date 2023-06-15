using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Color
    {
        public int Id { get; set; }
        // біле, червоне, рожеве
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public Color()
        {
            Wines = new List<Wine>();
        }
    }
}