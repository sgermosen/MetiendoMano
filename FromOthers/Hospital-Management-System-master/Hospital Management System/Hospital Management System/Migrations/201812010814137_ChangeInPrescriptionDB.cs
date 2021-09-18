namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInPrescriptionDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prescriptions", "Morning1", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon1", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening1", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning3", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon3", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening3", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning4", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon4", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening4", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning5", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon5", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening5", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning6", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon6", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening6", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Morning7", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Afternoon7", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Prescriptions", "Evening7", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prescriptions", "Evening7", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon7", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning7", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening6", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon6", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning6", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening5", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon5", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning5", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening4", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon4", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning4", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening3", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon3", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning3", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening2", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon2", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning2", c => c.String());
            AlterColumn("dbo.Prescriptions", "Evening1", c => c.String());
            AlterColumn("dbo.Prescriptions", "Afternoon1", c => c.String());
            AlterColumn("dbo.Prescriptions", "Morning1", c => c.String());
        }
    }
}
