namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgainAddScheduleInDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        AvailableStartDay = c.String(nullable: false),
                        AvailableEndDay = c.String(nullable: false),
                        AvailableStartTime = c.DateTime(nullable: false),
                        AvailableEndTime = c.DateTime(nullable: false),
                        TimePerPatient = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Schedules", new[] { "DoctorId" });
            DropTable("dbo.Schedules");
        }
    }
}
