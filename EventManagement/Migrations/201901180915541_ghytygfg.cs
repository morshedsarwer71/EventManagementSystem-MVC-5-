namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ghytygfg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkOrderParents", "VATCode", c => c.String());
            AlterColumn("dbo.WorkOrderParents", "Notes", c => c.String());
            AlterColumn("dbo.WorkOrderParents", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkOrderParents", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.WorkOrderParents", "Notes", c => c.String(nullable: false));
            AlterColumn("dbo.WorkOrderParents", "VATCode", c => c.String(nullable: false));
        }
    }
}
