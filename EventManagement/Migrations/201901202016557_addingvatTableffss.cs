namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingvatTableffss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkOrderParents", "OrderCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkOrderParents", "OrderCode", c => c.String());
        }
    }
}
