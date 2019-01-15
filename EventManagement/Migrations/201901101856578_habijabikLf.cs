namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class habijabikLf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOrderQuotations",
                c => new
                    {
                        QuotationWorkOrderId = c.Int(nullable: false, identity: true),
                        QuotationCode = c.String(),
                        QuotationStatus = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        VATCode = c.Int(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        PerHourRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VATValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoOfManpower = c.Int(nullable: false),
                        NoOfPax = c.Int(nullable: false),
                        NoOfService = c.Int(nullable: false),
                        NoOfSetup = c.Int(nullable: false),
                        Notes = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QuotationWorkOrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkOrderQuotations");
        }
    }
}
