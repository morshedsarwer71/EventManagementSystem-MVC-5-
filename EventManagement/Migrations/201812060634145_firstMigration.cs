namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountsHeads",
                c => new
                    {
                        AccountsHeadId = c.Int(nullable: false, identity: true),
                        AccountsHeadNameEN = c.String(nullable: false),
                        AccountsHeadNameAR = c.String(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        ReportHeadId = c.Int(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountsHeadId);
            
            CreateTable(
                "dbo.Concerns",
                c => new
                    {
                        ConcernId = c.Int(nullable: false, identity: true),
                        ConcernNameEN = c.String(),
                        ConcernNameAR = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConcernId);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        JournalId = c.Int(nullable: false, identity: true),
                        JournalEntryDate = c.DateTime(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        DebitJournalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitAccountsHeadId = c.Int(nullable: false),
                        CreditJournalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAccountsHeadId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        JournalCreationDate = c.DateTime(nullable: false),
                        JournalCreator = c.Int(nullable: false),
                        JournalModificationDate = c.DateTime(nullable: false),
                        JournalModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JournalId);
            
            CreateTable(
                "dbo.ReportHeads",
                c => new
                    {
                        ReportHeadId = c.Int(nullable: false, identity: true),
                        ReportHeadNameEN = c.String(nullable: false),
                        ReportHeadNameAR = c.String(nullable: false),
                        ReportType = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportHeadId);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        Disabled = c.Boolean(nullable: false),
                        LoginDate = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        LoginFailedCount = c.Int(nullable: false),
                        Language = c.String(),
                        ActivationDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        HashSalt = c.String(),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemUsers");
            DropTable("dbo.ReportHeads");
            DropTable("dbo.Journals");
            DropTable("dbo.Concerns");
            DropTable("dbo.AccountsHeads");
        }
    }
}
