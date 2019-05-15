namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PettyCash2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PettyCashes", "BankId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PettyCashes", "BankId");
        }
    }
}
