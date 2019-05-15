namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PettyCash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PettyCashes",
                c => new
                    {
                        PettyCasId = c.Int(nullable: false, identity: true),
                        CashDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PettyCasId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PettyCashes");
        }
    }
}
