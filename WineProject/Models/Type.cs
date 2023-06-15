using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Type
    {
        public int Id { get; set; }
        // безалкогольне, вермут, кагор, мадера, плодове, портвейн, тихе, херес
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public Type()
        {
            Wines = new List<Wine>();
        }
    }
}