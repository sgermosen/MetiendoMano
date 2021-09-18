namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLongtoStringInPatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "PhoneNo", c => c.String());
            AlterColumn("dbo.Patients", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Contact", c => c.Long(nullable: false));
            AlterColumn("dbo.Patients", "PhoneNo", c => c.Long(nullable: false));
        }
    }
}
