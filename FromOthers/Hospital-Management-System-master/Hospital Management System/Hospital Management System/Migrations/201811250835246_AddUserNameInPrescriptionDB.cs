namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameInPrescriptionDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "UserName");
        }
    }
}
