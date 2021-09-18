namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPrescriptionForSomeChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prescriptions", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Prescriptions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Prescriptions", new[] { "DoctorId" });
            DropIndex("dbo.Prescriptions", new[] { "PatientId" });
            DropIndex("dbo.Prescriptions", new[] { "Schedule_Id" });
            DropTable("dbo.Prescriptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        MedicalTest1 = c.String(),
                        MedicalTest2 = c.String(),
                        MedicalTest3 = c.String(),
                        MedicalTest4 = c.String(),
                        Medicine1 = c.String(),
                        Morning1 = c.String(),
                        Afternoon1 = c.String(),
                        Evening1 = c.String(),
                        Medicine2 = c.String(),
                        Morning2 = c.String(),
                        Afternoon2 = c.String(),
                        Evening2 = c.String(),
                        Medicine3 = c.String(),
                        Morning3 = c.String(),
                        Afternoon3 = c.String(),
                        Evening3 = c.String(),
                        Medicine4 = c.String(),
                        Morning4 = c.String(),
                        Afternoon4 = c.String(),
                        Evening4 = c.String(),
                        Medicine5 = c.String(),
                        Morning5 = c.String(),
                        Afternoon5 = c.String(),
                        Evening5 = c.String(),
                        Medicine6 = c.String(),
                        Morning6 = c.String(),
                        Afternoon6 = c.String(),
                        Evening6 = c.String(),
                        Medicine7 = c.String(),
                        Morning7 = c.String(),
                        Afternoon7 = c.String(),
                        Evening7 = c.String(),
                        CheckUpAfterDays = c.Int(nullable: false),
                        ScheduleDoctorId = c.Int(nullable: false),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Prescriptions", "Schedule_Id");
            CreateIndex("dbo.Prescriptions", "PatientId");
            CreateIndex("dbo.Prescriptions", "DoctorId");
            AddForeignKey("dbo.Prescriptions", "Schedule_Id", "dbo.Schedules", "Id");
            AddForeignKey("dbo.Prescriptions", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Prescriptions", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
        }
    }
}
