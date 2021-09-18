namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPatientInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(),
                        PhoneNo = c.Long(nullable: false),
                        Contact = c.Long(nullable: false),
                        BloodGroup = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Patients");
        }
    }
}
