namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropDownThePatientInDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "ApplicationUserId" });
            DropTable("dbo.Patients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(),
                        PhoneNo = c.Long(nullable: false),
                        Contact = c.Long(nullable: false),
                        BloodGroup = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Patients", "ApplicationUserId");
            AddForeignKey("dbo.Patients", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
