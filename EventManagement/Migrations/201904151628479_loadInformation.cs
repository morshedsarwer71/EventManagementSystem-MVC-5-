namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loadInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lenders",
                c => new
                    {
                        LenderId = c.Int(nullable: false, identity: true),
                        LenderName = c.String(),
                        LenderAddress = c.String(),
                        LenderMoile = c.String(),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LenderId);
            
            CreateTable(
                "dbo.LoanInstallments",
                c => new
                    {
                        LoanInstallmentId = c.Int(nullable: false, identity: true),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LoanInstallmentId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        LenderId = c.Int(nullable: false),
                        LoanDate = c.DateTime(nullable: false),
                        LoanPaidDate = c.DateTime(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfIntallment = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LoanId);
            
            AddColumn("dbo.WorkOrderParents", "EventName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrderParents", "EventName");
            DropTable("dbo.Loans");
            DropTable("dbo.LoanInstallments");
            DropTable("dbo.Lenders");
        }
    }
}
