namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class habGH : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrderParents", "PaymentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrderParents", "PaymentStatus");
        }
    }
}
