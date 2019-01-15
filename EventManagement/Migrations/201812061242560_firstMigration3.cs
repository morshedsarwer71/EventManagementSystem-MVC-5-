namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportHeads", "ConcernId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportHeads", "ConcernId");
        }
    }
}
