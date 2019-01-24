namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingvatTableff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VATs", "VatValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VATs", "VatValue");
        }
    }
}
