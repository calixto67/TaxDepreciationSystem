using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxDepreciationSystem.Backend.Repository.Models
{
    public partial class BMTContext : DbContext
    {
        public BMTContext(DbContextOptions<BMTContext> options) : base(options)
        {

        }

        public virtual DbSet<Contact> Contact { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });   
        }
    }
}
