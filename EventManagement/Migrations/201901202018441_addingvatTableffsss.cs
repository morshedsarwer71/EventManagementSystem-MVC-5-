namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingvatTableffsss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkOrderParents", "OrderCode", c => c.String());
            AlterColumn("dbo.WorkOrderParents", "VATCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkOrderParents", "VATCode", c => c.String());
            AlterColumn("dbo.WorkOrderParents", "OrderCode", c => c.Int(nullable: false));
        }
    }
}
