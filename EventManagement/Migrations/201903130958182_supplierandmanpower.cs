namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierandmanpower : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManpowerSuppliers",
                c => new
                    {
                        ManpowerSupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        ContactNumber = c.String(),
                        Description = c.String(),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManpowerSupplierId);
            
            CreateTable(
                "dbo.WorkOrderDailyManpowers",
                c => new
                    {
                        WOCManpowerId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WOCManpowerId);
            
            AddColumn("dbo.Employees", "ProviderCompany", c => c.String());
            AddColumn("dbo.Employees", "ManpowerSupplierId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ManpowerSupplierId");
            DropColumn("dbo.Employees", "ProviderCompany");
            DropTable("dbo.WorkOrderDailyManpowers");
            DropTable("dbo.ManpowerSuppliers");
        }
    }
}
