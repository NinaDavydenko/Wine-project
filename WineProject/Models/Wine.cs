using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WineProject.Models
{
    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public int Potential { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageWine { get; set; }

        public int Id_Color { get; set; }
        [ForeignKey("Id_Color")]
        public Color Color { get; set; }

        public int Id_Type { get; set; }
        [ForeignKey("Id_Type")]
        public Type Type { get; set; }

        public int Id_Sweetness { get; set; }
        [ForeignKey("Id_Sweetness")]
        public Sweetness Sweetness { get; set; }

        public int Id_Country { get; set; }
        [ForeignKey("Id_Country")]
        public Country Country { get; set; }

        public int Id_Brand { get; set; }
        [ForeignKey("Id_Brand")]
        public Brand Brand { get; set; }

        public int Id_GrapeVariety { get; set; }
        [ForeignKey("Id_GrapeVariety")]
        public GrapeVariety GrapeVariety { get; set; }
    }
}