using DimiTours.Models;
using Microsoft.EntityFrameworkCore;

namespace DimiTours.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Section> sections { get; set; }
        public DbSet<Activity> activities { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Section>().ToTable("Section");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Answer>().ToTable("Answer");


            //Unique username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            //Αν διαγραφεί Section => διαγράφεται και το Activity (Cascade)
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Section)
                .WithMany() // δεν έχεις navigation από Section -> Activity
                .HasForeignKey(a => a.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            //Διαγραφή των Answers εάν σβηστεί η ερώτηση
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
