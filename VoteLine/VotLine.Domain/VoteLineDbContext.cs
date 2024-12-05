using Microsoft.EntityFrameworkCore;
using System;
using VoteLine.Domain.Entities;


namespace VoteLine.Domain
{
    public class VoteLineDbContext : DbContext
    {

        public VoteLineDbContext(DbContextOptions<VoteLineDbContext> options): base(options) 
        { 
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }

        //relaciones de las tablas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //// Configuración de la relación entre User y Vote
            //modelBuilder.Entity<Vote>()
            //    .HasOne(v => v.User)
            //    .WithMany(u => u.Votes)
            //    .HasForeignKey(v => v.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Configuración de la relación entre Candidate y Vote
            //modelBuilder.Entity<Vote>()
            //    .HasOne(v => v.Candidate)
            //    .WithMany(c => c.Votes)
            //    .HasForeignKey(v => v.CandidateId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
