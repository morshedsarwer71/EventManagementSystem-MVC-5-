namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemUsers", "ConcernID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemUsers", "ConcernID");
        }
    }
}
