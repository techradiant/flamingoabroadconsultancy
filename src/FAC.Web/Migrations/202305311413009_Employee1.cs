namespace FAC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "TwitterUrl", c => c.String());
            AddColumn("dbo.Employees", "FacebookUrl", c => c.String());
            AddColumn("dbo.Employees", "InstagramUrl", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "InstagramUrl");
            DropColumn("dbo.Employees", "FacebookUrl");
            DropColumn("dbo.Employees", "TwitterUrl");
        }
    }
}
