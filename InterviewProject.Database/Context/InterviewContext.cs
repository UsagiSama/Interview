using InterviewProject.Database.DataSeeder;
using InterviewProject.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Database.Context
{
    public class InterviewDbContext
        : DbContext
    {
        public DbSet<Interview> Interviews
            => Set<Interview>();
        public DbSet<Interviewee> Interviewees
            => Set<Interviewee>();
        public DbSet<Interviewer> Interviewers
            => Set<Interviewer>();

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("InterviewDatabase");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var interview = builder.Entity<Interview>();
            interview.HasKey(x => x.Id);
            interview.HasIndex(x => x.Name).IsUnique();
            interview.HasOne(x => x.Interviewee)
                .WithMany()
                .HasForeignKey(x => x.IntervieweeId)
                .IsRequired();
            interview.HasOne(x => x.Interviewer)
                .WithMany()
                .HasForeignKey(x => x.InterviewerId)
                .IsRequired();
            interview.HasData(InterviewSeeder.SeedData());

            var interviewee = builder.Entity<Interviewee>();
            interviewee.HasKey(x => x.Id);
            interviewee.Property(x => x.FirstName)
                .IsRequired();
            interviewee.Property(x => x.LastName)
                .IsRequired();
            interviewee.HasData(IntervieweeSeeder.SeedData());

            var interviewer = builder.Entity<Interviewer>();
            interviewer.HasKey(x => x.Id);
            interviewer.Property(x => x.FirstName)
                .IsRequired();
            interviewer.Property(x => x.LastName)
                .IsRequired();
            interviewer.HasData(InterviewerSeeder.SeedData());
        }
    }
}
