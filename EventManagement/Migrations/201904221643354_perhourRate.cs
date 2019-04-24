namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perhourRate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrderDailyManpowers", "PerHourRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrderDailyManpowers", "PerHourRate");
        }
    }
}
