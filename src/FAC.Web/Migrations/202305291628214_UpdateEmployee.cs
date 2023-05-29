namespace FAC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ProfilePhotoUrl", c => c.String());
            AddColumn("dbo.Employees", "IncludeInTeamList", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "TeamListViewOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "TeamListViewOrder");
            DropColumn("dbo.Employees", "IncludeInTeamList");
            DropColumn("dbo.Employees", "ProfilePhotoUrl");
        }
    }
}
