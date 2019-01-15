namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventManagementModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AttendanceId = c.Int(nullable: false, identity: true),
                        AttendanceDate = c.DateTime(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        OutTime = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceId);
            
            CreateTable(
                "dbo.EmployeeEntitlements",
                c => new
                    {
                        EntitlementId = c.Int(nullable: false, identity: true),
                        EmployeeTypeEN = c.String(nullable: false),
                        EmployeeTypeAR = c.String(nullable: false),
                        ConcernId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntitlementId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        ConcernId = c.Int(nullable: false),
                        EmployeeCode = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        NickName = c.String(),
                        EmployeeEmail = c.String(),
                        ContactNumber = c.Int(nullable: false),
                        EmergencyNumber = c.Int(nullable: false),
                        PermanentAddress = c.String(nullable: false),
                        PresentAddress = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PassportNumber = c.String(nullable: false),
                        PassportExpiratationDate = c.DateTime(nullable: false),
                        NIDNumber = c.String(nullable: false),
                        VisaNumber = c.String(nullable: false),
                        VisaExpirationDate = c.DateTime(nullable: false),
                        EmployeeEntitlementId = c.Int(nullable: false),
                        MonthlySalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RatePerHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nationality = c.String(),
                        IsActive = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeesMiscellaneousAssignments",
                c => new
                    {
                        MiscellaneousAssignmentID = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MiscellaneousAssignmentID);
            
            CreateTable(
                "dbo.EmployeesServiceAssignments",
                c => new
                    {
                        ServiceAssignmentID = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceAssignmentID);
            
            CreateTable(
                "dbo.EmployeesSetupAssignments",
                c => new
                    {
                        SetupAssignmentID = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SetupAssignmentID);
            
            CreateTable(
                "dbo.EventManagementClients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ConcernId = c.Int(nullable: false),
                        ClientNameEN = c.String(nullable: false),
                        ClientNameAR = c.String(nullable: false),
                        ClientCode = c.String(nullable: false),
                        AccountHeadId = c.Int(nullable: false),
                        ClientContactInfo = c.String(nullable: false),
                        ClientAddress = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.EventMiscellaneous",
                c => new
                    {
                        EventMiscellaneousId = c.Int(nullable: false, identity: true),
                        EventMiscellaneousNameEN = c.String(nullable: false),
                        EventMiscellaneousNameAR = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventMiscellaneousId);
            
            CreateTable(
                "dbo.EventServices",
                c => new
                    {
                        EventServiceId = c.Int(nullable: false, identity: true),
                        EventServiceEN = c.String(nullable: false),
                        EventServiceAR = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventServiceId);
            
            CreateTable(
                "dbo.EventSetups",
                c => new
                    {
                        EventSetupId = c.Int(nullable: false, identity: true),
                        EventSetupEN = c.String(nullable: false),
                        EventSetupAR = c.String(nullable: false),
                        ConcernId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventSetupId);
            
            CreateTable(
                "dbo.WorkOrderChildManpowers",
                c => new
                    {
                        WOCManpowerId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WOCManpowerId);
            
            CreateTable(
                "dbo.WorkOrderChildMiscellaneous",
                c => new
                    {
                        WOCMiscellaneousId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EventMiscellaneousId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WOCMiscellaneousId);
            
            CreateTable(
                "dbo.WorkOrderChildServices",
                c => new
                    {
                        WOCServiceId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EventServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WOCServiceId);
            
            CreateTable(
                "dbo.WorkOrderChildSetups",
                c => new
                    {
                        WOCSetupId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        EventSetupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WOCSetupId);
            
            CreateTable(
                "dbo.WorkOrderParents",
                c => new
                    {
                        WorkOrderId = c.Int(nullable: false, identity: true),
                        ConcernId = c.Int(nullable: false),
                        OrderCode = c.String(),
                        VATCode = c.String(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NoOfManpower = c.Int(nullable: false),
                        NoOfSetup = c.Int(nullable: false),
                        NoOfService = c.Int(nullable: false),
                        NoOfPax = c.Int(nullable: false),
                        Notes = c.String(nullable: false),
                        PerHourRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartingDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkOrderId);
            
            CreateTable(
                "dbo.WorkOrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        OrderStates = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkOrderStatus");
            DropTable("dbo.WorkOrderParents");
            DropTable("dbo.WorkOrderChildSetups");
            DropTable("dbo.WorkOrderChildServices");
            DropTable("dbo.WorkOrderChildMiscellaneous");
            DropTable("dbo.WorkOrderChildManpowers");
            DropTable("dbo.EventSetups");
            DropTable("dbo.EventServices");
            DropTable("dbo.EventMiscellaneous");
            DropTable("dbo.EventManagementClients");
            DropTable("dbo.EmployeesSetupAssignments");
            DropTable("dbo.EmployeesServiceAssignments");
            DropTable("dbo.EmployeesMiscellaneousAssignments");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeEntitlements");
            DropTable("dbo.Attendances");
        }
    }
}
