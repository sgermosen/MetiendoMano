namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropScheduleForSomeChanges : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Schedules");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Doctor = c.String(nullable: false),
                        AvailableStartDay = c.String(nullable: false),
                        AvailableEndDay = c.String(nullable: false),
                        AvailableStartTime = c.DateTime(nullable: false),
                        AvailableEndTime = c.DateTime(nullable: false),
                        TimePerPatient = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
