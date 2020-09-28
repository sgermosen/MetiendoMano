namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintV40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LessonsPerCourses", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LessonsPerCourses", "Order");
        }
    }
}
