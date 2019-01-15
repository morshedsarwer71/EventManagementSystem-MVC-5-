namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class habijabi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", c => c.Int());
            CreateIndex("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID");
            AddForeignKey("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", "dbo.EmployeesMiscellaneousAssignments", "MiscellaneousAssignmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", "dbo.EmployeesMiscellaneousAssignments");
            DropIndex("dbo.WorkOrderChildManpowers", new[] { "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID" });
            DropColumn("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID");
        }
    }
}
