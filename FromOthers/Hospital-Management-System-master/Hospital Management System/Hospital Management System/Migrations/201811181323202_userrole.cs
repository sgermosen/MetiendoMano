namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userrole : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into AspNetRoles(Id, Name) Values(1, 'Admin')");
            Sql("Insert Into AspNetRoles(Id, Name) Values(2, 'Doctor')");
            Sql("Insert Into AspNetRoles(Id, Name) Values(3, 'Patient')");
        }
        
        public override void Down()
        {
        }
    }
}
