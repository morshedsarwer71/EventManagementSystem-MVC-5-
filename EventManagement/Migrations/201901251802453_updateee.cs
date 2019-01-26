namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalaryPayments", "SalaryTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.SalaryPayments", "TransactionTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.SalaryPayments", "PaymentType");
            DropColumn("dbo.SalaryPayments", "TransactionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalaryPayments", "TransactionType", c => c.Int(nullable: false));
            AddColumn("dbo.SalaryPayments", "PaymentType", c => c.Int(nullable: false));
            DropColumn("dbo.SalaryPayments", "TransactionTypeId");
            DropColumn("dbo.SalaryPayments", "SalaryTypeId");
        }
    }
}
