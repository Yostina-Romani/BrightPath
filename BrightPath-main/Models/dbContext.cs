using BrightPath.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrightPath.Models

{
    public class dbContext : IdentityDbContext<applicationuser>
    {
        public dbContext(DbContextOptions<dbContext> options)
           : base(options)
        {

        }
        public DbSet<Student> students { get; set; }
        public DbSet<Grade> grade { get; set; }

        public DbSet<Course> courses { get; set; }
        public DbSet<studentCourse> studentCourses { get; set; }
        public DbSet<Lessons> lessons { get; set; }
        public DbSet<LessonProgress> lessonProgresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.student)
                .WithMany()
                .HasForeignKey(lp => lp.studentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.course)
                .WithMany()
                .HasForeignKey(lp => lp.courseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.lesson)
                .WithMany()
                .HasForeignKey(lp => lp.lessonId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
