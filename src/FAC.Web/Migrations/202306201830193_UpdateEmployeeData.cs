namespace FAC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ListeningExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "DesignExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "LearningExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "PersonalExperience", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PersonalExperience");
            DropColumn("dbo.Employees", "LearningExperience");
            DropColumn("dbo.Employees", "DesignExperience");
            DropColumn("dbo.Employees", "ListeningExperience");
        }
    }
}
