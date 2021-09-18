namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullNameInDoctorDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "FullName");
        }
    }
}
