namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusInDoctorDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Status");
        }
    }
}
