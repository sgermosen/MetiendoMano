namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmbulanceToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ambulances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AmbulanceId = c.String(nullable: false),
                        AmbulanceStatus = c.String(nullable: false),
                        AmbulanceDriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AmbulanceDrivers", t => t.AmbulanceDriverId, cascadeDelete: true)
                .Index(t => t.AmbulanceDriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ambulances", "AmbulanceDriverId", "dbo.AmbulanceDrivers");
            DropIndex("dbo.Ambulances", new[] { "AmbulanceDriverId" });
            DropTable("dbo.Ambulances");
        }
    }
}
