namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInAmbulanceDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ambulances", "AmbulanceStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ambulances", "AmbulanceStatus", c => c.String(nullable: false));
        }
    }
}
