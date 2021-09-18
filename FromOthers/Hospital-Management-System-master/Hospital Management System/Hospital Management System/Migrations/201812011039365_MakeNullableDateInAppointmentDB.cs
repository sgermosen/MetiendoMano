namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNullableDateInAppointmentDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
        }
    }
}
