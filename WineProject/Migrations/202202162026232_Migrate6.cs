namespace WineProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate6 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        WineId = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Name = c.String(),
                        Brand = c.String(),
                        Year = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.WineId);
            
        }
    }
}
