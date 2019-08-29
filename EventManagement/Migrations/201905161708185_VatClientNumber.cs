namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatClientNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventManagementClients", "TaxRegNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventManagementClients", "TaxRegNumber");
        }
    }
}
