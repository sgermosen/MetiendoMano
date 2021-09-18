namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmbulanceInDb : DbMigration
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
                        AssignDriver = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ambulances");
        }
    }
}
