namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class habijabikL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeImagePath", c => c.String());
            AddColumn("dbo.Employees", "PassportImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PassportImagePath");
            DropColumn("dbo.Employees", "EmployeeImagePath");
        }
    }
}
