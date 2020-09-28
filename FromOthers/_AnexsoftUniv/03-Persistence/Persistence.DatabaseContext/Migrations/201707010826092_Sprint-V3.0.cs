namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintV30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReviewsPerCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersPerCourses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "UserId" });
            DropIndex("dbo.ReviewsPerCourses", new[] { "UserId" });
            DropIndex("dbo.UsersPerCourses", new[] { "UserId" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Icon", c => c.String(nullable: false));
            AlterColumn("dbo.CourseLessonLearnedsPerStudents", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LessonsPerCourses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.LessonsPerCourses", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ReviewsPerCourses", "Comment", c => c.String(nullable: false));
            AlterColumn("dbo.ReviewsPerCourses", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UsersPerCourses", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CourseLessonLearnedsPerStudents", "UserId");
            CreateIndex("dbo.ReviewsPerCourses", "UserId");
            CreateIndex("dbo.UsersPerCourses", "UserId");
            AddForeignKey("dbo.CourseLessonLearnedsPerStudents", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReviewsPerCourses", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersPerCourses", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersPerCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReviewsPerCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsersPerCourses", new[] { "UserId" });
            DropIndex("dbo.ReviewsPerCourses", new[] { "UserId" });
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "UserId" });
            AlterColumn("dbo.UsersPerCourses", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReviewsPerCourses", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReviewsPerCourses", "Comment", c => c.String());
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
            AlterColumn("dbo.LessonsPerCourses", "Content", c => c.String());
            AlterColumn("dbo.LessonsPerCourses", "Name", c => c.String());
            AlterColumn("dbo.CourseLessonLearnedsPerStudents", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "Icon", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            CreateIndex("dbo.UsersPerCourses", "UserId");
            CreateIndex("dbo.ReviewsPerCourses", "UserId");
            CreateIndex("dbo.CourseLessonLearnedsPerStudents", "UserId");
            AddForeignKey("dbo.UsersPerCourses", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ReviewsPerCourses", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CourseLessonLearnedsPerStudents", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
