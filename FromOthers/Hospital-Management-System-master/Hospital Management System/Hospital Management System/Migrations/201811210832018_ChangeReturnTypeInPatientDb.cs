namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReturnTypeInPatientDb : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Patients", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Patients", "ApplicationUserId");
            RenameColumn(table: "dbo.Patients", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Patients", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Patients", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Patients", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Patients", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Patients", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Patients", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "ApplicationUser_Id");
        }
    }
}
