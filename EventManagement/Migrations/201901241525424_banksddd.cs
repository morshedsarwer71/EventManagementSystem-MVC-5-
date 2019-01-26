namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banksddd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenditures",
                c => new
                    {
                        ExpenditureId = c.Int(nullable: false, identity: true),
                        ExpenditureType = c.Int(nullable: false),
                        MyProperty = c.Int(nullable: false),
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
                .PrimaryKey(t => t.ExpenditureId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expenditures");
        }
    }
}
