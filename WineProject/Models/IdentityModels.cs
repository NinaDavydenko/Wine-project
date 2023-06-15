using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WineProject.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public double Total { get; set; }
        public int Discount { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ApplicationUser()
        {
            Orders = new List<Order>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //userIdentity.AddClaim(new Claim("NameSurname", this.Name + this.Surname));
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public string GetSurname()
        {
            return this.Name + " " + this.Surname;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            //Database.SetInitializer(new MyInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GrapeVariety> GrapeVarieties { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sweetness> Sweetnesses { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Wine> Wines { get; set; }

        //public System.Data.Entity.DbSet<WineProject.Models.CartItem> CartItems { get; set; }
    }

    //public class MyInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    public class MyInitializer :DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleUser = new IdentityRole { Name = "user" };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);
            //context.SaveChanges();
            //base.Seed(context);

            var admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", Birth = DateTime.Now, Name = "Admin", Surname = "Admin", Total = 0, Discount = 0 };
            string passadmin = "Admin_1";
            var resuladmin = userManager.Create(admin, passadmin);

            if (resuladmin.Succeeded)
            {
                userManager.AddToRole(admin.Id, roleAdmin.Name);
            }

            var user = new ApplicationUser { UserName = "user1@gmail.com", Email = "user1@gmail.com", Birth = DateTime.Now, Name = "User1", Surname = "User1", Total = 0, Discount = 0 };
            string passuser = "User_1";
            var resultuser = userManager.Create(user, passuser);

            if (resultuser.Succeeded)
            {
                userManager.AddToRole(user.Id, roleUser.Name);
            }

            Brand brand1 = new Brand { Name = "Brand1" };
            Brand brand2 = new Brand { Name = "Brand2" };
            Brand brand3 = new Brand { Name = "Brand3" };
            Brand brand4 = new Brand { Name = "Brand4" };

            context.Brands.AddRange(new List<Brand> { brand1, brand2, brand3, brand4 });

            Color color1 = new Color { Name = "Біле" };
            Color color2 = new Color { Name = "Червоне" };
            Color color3 = new Color { Name = "Рожеве" };

            context.Colors.AddRange(new List<Color> { color1, color2, color3 });

            Country country1 = new Country { Name = "Country1" };
            Country country2 = new Country { Name = "Country2" };
            Country country3 = new Country { Name = "Country3" };
            Country country4 = new Country { Name = "Country4" };

            context.Countries.AddRange(new List<Country> { country1, country2, country3, country4 });

            GrapeVariety grapeVariety1 = new GrapeVariety { Name = "GrapeVariety1" };
            GrapeVariety grapeVariety2 = new GrapeVariety { Name = "GrapeVariety2" };
            GrapeVariety grapeVariety3 = new GrapeVariety { Name = "GrapeVariety3" };
            GrapeVariety grapeVariety4 = new GrapeVariety { Name = "GrapeVariety4" };

            context.GrapeVarieties.AddRange(new List<GrapeVariety> { grapeVariety1, grapeVariety2, grapeVariety3, grapeVariety4 });

            Sweetness sweetness1 = new Sweetness { Name = "Сухе" };
            Sweetness sweetness2 = new Sweetness { Name = "Брют" };
            Sweetness sweetness3 = new Sweetness { Name = "Напівсухе" };
            Sweetness sweetness4 = new Sweetness { Name = "Напівсолодке" };
            Sweetness sweetness5 = new Sweetness { Name = "Солодке" };

            context.Sweetnesses.AddRange(new List<Sweetness> { sweetness1, sweetness2, sweetness3, sweetness4, sweetness5 });

            Type type1 = new Type { Name = "Безалкогольне" };
            Type type2 = new Type { Name = "Вермут" };
            Type type3 = new Type { Name = "Кагор" };
            Type type4 = new Type { Name = "Мадера" };
            Type type5 = new Type { Name = "Плодове" };
            Type type6 = new Type { Name = "Портвейн" };
            Type type7 = new Type { Name = "Тихе" };
            Type type8 = new Type { Name = "Херес" };

            context.Types.AddRange(new List<Type> { type1, type2, type3, type4, type5, type6, type7, type8 });

            Wine wine1 = new Wine { Name = "Wine1", ProductionYear = 2019, Potential = 10, Volume = 0.5, Price=1.1, Description = "Description to Wine1", ImageWine = "Images/wine1.jpg", Color = color1, Type = type1, Sweetness = sweetness1, Country = country1, Brand = brand1, GrapeVariety = grapeVariety1 };
            Wine wine2 = new Wine { Name = "Wine2", ProductionYear = 2018, Potential = 7, Volume = 0.5, Price = 2.2, Description = "Description to Wine2", ImageWine = "Images/wine2.jpg", Color = color2, Type = type2, Sweetness = sweetness2, Country = country2, Brand = brand2, GrapeVariety = grapeVariety2 };
            Wine wine3 = new Wine { Name = "Wine3", ProductionYear = 2017, Potential = 8, Volume = 0.75, Price = 3.3, Description = "Description to Wine3", ImageWine = "Images/wine3.jpg", Color = color3, Type = type3, Sweetness = sweetness3, Country = country3, Brand = brand3, GrapeVariety = grapeVariety3 };
            Wine wine4 = new Wine { Name = "Wine4", ProductionYear = 2017, Potential = 5, Volume = 0.75, Price = 4.4, Description = "Description to Wine4", ImageWine = "Images/wine4.jpg", Color = color3, Type = type4, Sweetness = sweetness4, Country = country4, Brand = brand4, GrapeVariety = grapeVariety4 };
            Wine wine5 = new Wine { Name = "Wine5", ProductionYear = 2015, Potential = 2, Volume = 0.5, Price = 5.5, Description = "Description to Wine5", ImageWine = "Images/wine5.jpg", Color = color1, Type = type5, Sweetness = sweetness5, Country = country1, Brand = brand1, GrapeVariety = grapeVariety1 };
            Wine wine6 = new Wine { Name = "Wine6", ProductionYear = 2019, Potential = 15, Volume = 1.0, Price = 6.6, Description = "Description to Wine6", ImageWine = "Images/wine6.jpg", Color = color2, Type = type6, Sweetness = sweetness1, Country = country2, Brand = brand2, GrapeVariety = grapeVariety2 };
            Wine wine7 = new Wine { Name = "Wine7", ProductionYear = 2012, Potential = 15, Volume = 1.0, Price = 7.7, Description = "Description to Wine7", ImageWine = "Images/wine7.jpg", Color = color3, Type = type7, Sweetness = sweetness2, Country = country3, Brand = brand3, GrapeVariety = grapeVariety3 };
            Wine wine8 = new Wine { Name = "Wine8", ProductionYear = 2016, Potential = 10, Volume = 0.25, Price = 8.8, Description = "Description to Wine8", ImageWine = "Images/wine8.jpg", Color = color1, Type = type8, Sweetness = sweetness3, Country = country4, Brand = brand4, GrapeVariety = grapeVariety4 };
            Wine wine9 = new Wine { Name = "Wine9", ProductionYear = 2020, Potential = 5, Volume = 0.5, Price = 9.9, Description = "Description to Wine9", ImageWine = "Images/wine9.jpg", Color = color2, Type = type1, Sweetness = sweetness4, Country = country1, Brand = brand1, GrapeVariety = grapeVariety1 };
            Wine wine10 = new Wine { Name = "Wine10", ProductionYear = 2018, Potential = 7, Volume = 0.75, Price = 10.1, Description = "Description to Wine10", ImageWine = "Images/wine10.jpg", Color = color3, Type = type2, Sweetness = sweetness5, Country = country2, Brand = brand2, GrapeVariety = grapeVariety2 };

            context.Wines.AddRange(new List<Wine> { wine1, wine2, wine3, wine4, wine5, wine6, wine7, wine8, wine9, wine10 });

            //context.SaveChanges();
            base.Seed(context);
        }
    }
}