namespace WineProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CartItems", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.CartItems", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CartItems", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.CartItems", "Price", c => c.Int(nullable: false));
        }
    }
}
