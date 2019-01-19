namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employyeupp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "ContactNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "EmergencyNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmergencyNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "ContactNumber", c => c.Int(nullable: false));
        }
    }
}
