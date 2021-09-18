namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmbulanceDriversInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmbulanceDrivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Cnic = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AmbulanceDrivers");
        }
    }
}
