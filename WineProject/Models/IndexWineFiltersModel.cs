using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class IndexWineFiltersModel
    {
        public int colorId { get; set; }
        public int brandId { get; set; }
        public int countryId { get; set; }
        public int grapeId { get; set; }
        public int sweetnessId { get; set; }
        public int typeId { get; set; }
    }
}