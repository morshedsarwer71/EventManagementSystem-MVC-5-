namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loadInformation7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "InTimeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Attendances", "OutTimeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Lenders", "LenderMobile", c => c.String());
            AddColumn("dbo.LoanInstallments", "LenderId", c => c.Int(nullable: false));
            AddColumn("dbo.LoanInstallments", "InstallmentAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LoanInstallments", "InstallmentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoanInstallments", "ConcernId", c => c.Int(nullable: false));
            AddColumn("dbo.Loans", "ConcernId", c => c.Int(nullable: false));
            DropColumn("dbo.Lenders", "LenderMoile");
            DropColumn("dbo.LoanInstallments", "LoanAmount");
            DropColumn("dbo.LoanInstallments", "LoanDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanInstallments", "LoanDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoanInstallments", "LoanAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Lenders", "LenderMoile", c => c.String());
            DropColumn("dbo.Loans", "ConcernId");
            DropColumn("dbo.LoanInstallments", "ConcernId");
            DropColumn("dbo.LoanInstallments", "InstallmentDate");
            DropColumn("dbo.LoanInstallments", "InstallmentAmount");
            DropColumn("dbo.LoanInstallments", "LenderId");
            DropColumn("dbo.Lenders", "LenderMobile");
            DropColumn("dbo.Attendances", "OutTimeDate");
            DropColumn("dbo.Attendances", "InTimeDate");
        }
    }
}
