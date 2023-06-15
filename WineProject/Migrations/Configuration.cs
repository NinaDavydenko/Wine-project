namespace WineProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WineProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WineProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WineProject.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
        }
    }
}
