namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenditures", "ExpenditureDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Expenditures", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenditures", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Expenditures", "ExpenditureDate");
        }
    }
}
