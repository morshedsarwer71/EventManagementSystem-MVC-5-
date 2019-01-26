namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banksdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        BankAdress = c.String(),
                        ContactNumber = c.String(),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            AddColumn("dbo.EventPayments", "BankId", c => c.Int(nullable: false));
            AddColumn("dbo.SalaryPayments", "TransactionType", c => c.Int(nullable: false));
            DropColumn("dbo.EventPayments", "BankName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventPayments", "BankName", c => c.String());
            DropColumn("dbo.SalaryPayments", "TransactionType");
            DropColumn("dbo.EventPayments", "BankId");
            DropTable("dbo.Banks");
        }
    }
}
