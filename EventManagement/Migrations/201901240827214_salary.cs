namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventPayments",
                c => new
                    {
                        EventPaymentId = c.Int(nullable: false, identity: true),
                        EventClientId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        BankName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventPaymentId);
            
            CreateTable(
                "dbo.SalaryPayments",
                c => new
                    {
                        SalaryPaymentId = c.Int(nullable: false, identity: true),
                        PaymentType = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryMonth = c.DateTime(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryPaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalaryPayments");
            DropTable("dbo.EventPayments");
        }
    }
}
