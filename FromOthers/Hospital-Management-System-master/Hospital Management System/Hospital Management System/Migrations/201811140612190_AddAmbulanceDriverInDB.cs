namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmbulanceDriverInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmbulanceDrivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        Cnic = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ambulances", "AmbulanceStatus", c => c.String(nullable: false));
            AddColumn("dbo.Ambulances", "AmbulancesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ambulances", "AmbulancesId");
            AddForeignKey("dbo.Ambulances", "AmbulancesId", "dbo.Ambulances", "Id");
            DropColumn("dbo.Ambulances", "AssignDriver");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ambulances", "AssignDriver", c => c.String(nullable: false));
            DropForeignKey("dbo.Ambulances", "AmbulancesId", "dbo.Ambulances");
            DropIndex("dbo.Ambulances", new[] { "AmbulancesId" });
            DropColumn("dbo.Ambulances", "AmbulancesId");
            DropColumn("dbo.Ambulances", "AmbulanceStatus");
            DropTable("dbo.AmbulanceDrivers");
        }
    }
}
