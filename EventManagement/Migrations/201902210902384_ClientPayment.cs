namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientPayments",
                c => new
                    {
                        ClientPaymentId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.Int(nullable: false),
                        BankId = c.Int(nullable: false),
                        PayeesName = c.String(),
                        Notes = c.String(),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ClientPaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientPayments");
        }
    }
}
