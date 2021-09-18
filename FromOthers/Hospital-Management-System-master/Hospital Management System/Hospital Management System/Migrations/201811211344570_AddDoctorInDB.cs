namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNo = c.String(),
                        ContactNo = c.String(nullable: false),
                        Specialization = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        BloodGroup = c.String(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Education = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Doctors", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Doctors", new[] { "DepartmentId" });
            DropIndex("dbo.Doctors", new[] { "ApplicationUserId" });
            DropTable("dbo.Doctors");
        }
    }
}
