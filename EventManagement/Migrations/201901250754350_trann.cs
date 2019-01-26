namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trann : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenditureHeads",
                c => new
                    {
                        ExpenditureHeadId = c.Int(nullable: false, identity: true),
                        ExpenditureHeadEN = c.String(),
                        ExpenditureHeadAR = c.String(),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ExpenditureHeadId);
            
            AddColumn("dbo.Banks", "BankNameEN", c => c.String());
            AddColumn("dbo.Banks", "BankNameAR", c => c.String());
            AddColumn("dbo.Expenditures", "ExpenditureHeadId", c => c.Int(nullable: false));
            DropColumn("dbo.Banks", "BankName");
            DropColumn("dbo.Expenditures", "ExpenditureType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenditures", "ExpenditureType", c => c.Int(nullable: false));
            AddColumn("dbo.Banks", "BankName", c => c.String());
            DropColumn("dbo.Expenditures", "ExpenditureHeadId");
            DropColumn("dbo.Banks", "BankNameAR");
            DropColumn("dbo.Banks", "BankNameEN");
            DropTable("dbo.ExpenditureHeads");
        }
    }
}
