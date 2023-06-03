namespace FAC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        EmployeeDesignation = c.String(),
                        EmployeeAddress = c.String(),
                        EmployeePhone = c.String(),
                        EmployeeGender = c.String(),
                        City = c.String(),
                        CompanyName = c.String(),
                        PinCode = c.Int(nullable: false),
                        EmployeeDiscription = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        ProfilePhotoUrl =c.String(),
                        IncludeInTeamList =c.String(),
                        TeamListViewOrder = c.String(),
                        TwitterUrl = c.String(),
                        FacebookUrl = c.String(),
                        InstagramUrl = c.String()

                })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
