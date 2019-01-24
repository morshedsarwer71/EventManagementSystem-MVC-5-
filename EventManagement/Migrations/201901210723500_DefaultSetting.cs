namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultSettings",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        VatName = c.String(),
                        VatValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VatNumber = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId);
            
            DropTable("dbo.VATs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VATs",
                c => new
                    {
                        VatId = c.Int(nullable: false, identity: true),
                        VatName = c.String(),
                        VatValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VatId);
            
            DropTable("dbo.DefaultSettings");
        }
    }
}
