namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingvatTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VATs",
                c => new
                    {
                        VatId = c.Int(nullable: false, identity: true),
                        VatName = c.String(),
                    })
                .PrimaryKey(t => t.VatId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VATs");
        }
    }
}
