using Microsoft.EntityFrameworkCore;
using System;
using VoteLine.Domain.Entities;


namespace VoteLine.Persistence
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

        }

    }
}
