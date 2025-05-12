using DimiTours.Models;
using Microsoft.EntityFrameworkCore;

namespace DimiTours.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<ActivityQuestion> ActivityQuestions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Section>().ToTable("Section");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<MediaItem>().ToTable("MediaItem");
            modelBuilder.Entity<ActivityQuestion>().ToTable("ActivityQuestion");
            modelBuilder.Entity<UserAnswer>().ToTable("UserAnswer");

            modelBuilder.Entity<ActivityQuestion>()
                .HasKey(aq => aq.Id);

            modelBuilder.Entity<ActivityQuestion>()
               .HasOne(aq => aq.Activity)
               .WithMany(a => a.ActivityQuestions)
               .HasForeignKey(aq => aq.ActivityId);

            modelBuilder.Entity<ActivityQuestion>()
                .HasOne(aq => aq.Question)
                .WithMany(q => q.ActivityQuestions) // FIX για το QuestionId1
                .HasForeignKey(aq => aq.QuestionId);

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

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Διαγραφή του UserAnswer όταν διαγράφεται ο χρήστης

            // Διαγραφή του UserAnswer όταν διαγράφεται η απάντηση
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
