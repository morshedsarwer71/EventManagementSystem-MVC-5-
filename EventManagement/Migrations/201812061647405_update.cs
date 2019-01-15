namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journals", "VoucherCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journals", "VoucherCode");
        }
    }
}
