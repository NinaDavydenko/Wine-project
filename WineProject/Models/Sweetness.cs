using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Sweetness
    {
        public int Id { get; set; }
        // сухе, брют, напівсухе, напівсолодке, солодке
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public Sweetness()
        {
            Wines = new List<Wine>();
        }
    }
}