namespace EventManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class habijabik : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", "dbo.EmployeesMiscellaneousAssignments");
            DropIndex("dbo.WorkOrderChildManpowers", new[] { "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID" });
            DropColumn("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", c => c.Int());
            CreateIndex("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID");
            AddForeignKey("dbo.WorkOrderChildManpowers", "EmployeesMiscellaneousAssignment_MiscellaneousAssignmentID", "dbo.EmployeesMiscellaneousAssignments", "MiscellaneousAssignmentID");
        }
    }
}
