namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintV50 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "Lesson_Id", "dbo.LessonsPerCourses");
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "Lesson_Id" });
            RenameColumn(table: "dbo.CourseLessonLearnedsPerStudents", name: "Lesson_Id", newName: "LessonId");
            AlterColumn("dbo.CourseLessonLearnedsPerStudents", "LessonId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseLessonLearnedsPerStudents", "LessonId");
            AddForeignKey("dbo.CourseLessonLearnedsPerStudents", "LessonId", "dbo.LessonsPerCourses", "Id", cascadeDelete: true);
            DropColumn("dbo.CourseLessonLearnedsPerStudents", "LessonsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseLessonLearnedsPerStudents", "LessonsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourseLessonLearnedsPerStudents", "LessonId", "dbo.LessonsPerCourses");
            DropIndex("dbo.CourseLessonLearnedsPerStudents", new[] { "LessonId" });
            AlterColumn("dbo.CourseLessonLearnedsPerStudents", "LessonId", c => c.Int());
            RenameColumn(table: "dbo.CourseLessonLearnedsPerStudents", name: "LessonId", newName: "Lesson_Id");
            CreateIndex("dbo.CourseLessonLearnedsPerStudents", "Lesson_Id");
            AddForeignKey("dbo.CourseLessonLearnedsPerStudents", "Lesson_Id", "dbo.LessonsPerCourses", "Id");
        }
    }
}
