namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salaryd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventPayments", "Description", c => c.String());
            AddColumn("dbo.SalaryPayments", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalaryPayments", "Description");
            DropColumn("dbo.EventPayments", "Description");
        }
    }
}
