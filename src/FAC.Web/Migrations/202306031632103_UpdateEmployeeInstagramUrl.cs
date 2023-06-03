namespace FAC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeInstagramUrl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "InstagramUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "InstagramUrl", c => c.Int(nullable: false));
        }
    }
}
