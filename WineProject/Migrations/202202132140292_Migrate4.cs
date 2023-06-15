namespace WineProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        WineId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        Brand = c.String(),
                        Year = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WineId);
            
            AddColumn("dbo.Wines", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Sum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Sum", c => c.Int(nullable: false));
            DropColumn("dbo.Wines", "Price");
            DropTable("dbo.CartItems");
        }
    }
}
