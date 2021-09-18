namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrescriptionDateinPrescriptionDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "PrescriptionAddDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "PrescriptionAddDate");
        }
    }
}
