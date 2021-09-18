namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ambulances", "AmbulancesId", "dbo.Ambulances");
            DropIndex("dbo.Ambulances", new[] { "AmbulancesId" });
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String());
            DropTable("dbo.Ambulances");
            DropTable("dbo.AmbulanceDrivers");
        }
        
        public override void Down()
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
            
            CreateTable(
                "dbo.Ambulances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AmbulanceId = c.String(nullable: false),
                        AmbulanceStatus = c.String(nullable: false),
                        AmbulancesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AspNetUsers", "UserRole");
            CreateIndex("dbo.Ambulances", "AmbulancesId");
            AddForeignKey("dbo.Ambulances", "AmbulancesId", "dbo.Ambulances", "Id");
        }
    }
}
