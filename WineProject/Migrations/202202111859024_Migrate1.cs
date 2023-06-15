namespace WineProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductionYear = c.Int(nullable: false),
                        Potential = c.Int(nullable: false),
                        Volume = c.Double(nullable: false),
                        Description = c.String(),
                        ImageWine = c.String(),
                        Id_Color = c.Int(nullable: false),
                        Id_Type = c.Int(nullable: false),
                        Id_Sweetness = c.Int(nullable: false),
                        Id_Country = c.Int(nullable: false),
                        Id_Brand = c.Int(nullable: false),
                        Id_GrapeVariety = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Id_Brand, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Id_Color, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Id_Country, cascadeDelete: true)
                .ForeignKey("dbo.GrapeVarieties", t => t.Id_GrapeVariety, cascadeDelete: true)
                .ForeignKey("dbo.Sweetnesses", t => t.Id_Sweetness, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.Id_Type, cascadeDelete: true)
                .Index(t => t.Id_Color)
                .Index(t => t.Id_Type)
                .Index(t => t.Id_Sweetness)
                .Index(t => t.Id_Country)
                .Index(t => t.Id_Brand)
                .Index(t => t.Id_GrapeVariety);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrapeVarieties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sweetnesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                        BuyingDay = c.DateTime(nullable: false),
                        Address = c.String(),
                        ListGoods = c.String(),
                        Id_Customer = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id_Customer)
                .Index(t => t.Id_Customer);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Birth = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "Id_Customer", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Wines", "Id_Type", "dbo.Types");
            DropForeignKey("dbo.Wines", "Id_Sweetness", "dbo.Sweetnesses");
            DropForeignKey("dbo.Wines", "Id_GrapeVariety", "dbo.GrapeVarieties");
            DropForeignKey("dbo.Wines", "Id_Country", "dbo.Countries");
            DropForeignKey("dbo.Wines", "Id_Color", "dbo.Colors");
            DropForeignKey("dbo.Wines", "Id_Brand", "dbo.Brands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "Id_Customer" });
            DropIndex("dbo.Wines", new[] { "Id_GrapeVariety" });
            DropIndex("dbo.Wines", new[] { "Id_Brand" });
            DropIndex("dbo.Wines", new[] { "Id_Country" });
            DropIndex("dbo.Wines", new[] { "Id_Sweetness" });
            DropIndex("dbo.Wines", new[] { "Id_Type" });
            DropIndex("dbo.Wines", new[] { "Id_Color" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.Types");
            DropTable("dbo.Sweetnesses");
            DropTable("dbo.GrapeVarieties");
            DropTable("dbo.Countries");
            DropTable("dbo.Colors");
            DropTable("dbo.Wines");
            DropTable("dbo.Brands");
        }
    }
}
