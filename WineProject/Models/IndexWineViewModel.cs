using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class IndexWineViewModel
    {
        public List<Wine> Wines { get; set; }
        public IPagedList<Wine> PagedWines { get; set; }
        public List<Color> Colors { get; set; }
        public List<WineProject.Models.Type> Types { get; set; }
        public List<Sweetness> Sweetnesses { get; set; }
        public List<Country> Countries { get; set; }
        public List<Brand> Brands { get; set; }
        public List<GrapeVariety> GrapeVarieties { get; set; }

        //public PageInfo PageInfo { get; set; }
    }
}