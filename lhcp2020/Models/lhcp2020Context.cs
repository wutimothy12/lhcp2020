using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lhcp2020.Models
{
    public partial class lhcp2020Context : DbContext
    {
        public lhcp2020Context()
        {
        }

        public lhcp2020Context(DbContextOptions<lhcp2020Context> options)
            : base(options)
        {
        }

        public virtual DbSet<MainText> MainText { get; set; }
        public  DbSet<ChinesePainting> ChinesePaintings { get; set; }

        // Unable to generate entity type for table 'dbo.ChinesePaintings'. Please see the warning messages.

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lhcp2020;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainText>(entity =>
            {
                entity.Property(e => e.MainTextId).HasColumnName("MainTextID");

                entity.Property(e => e.MainTextDescription)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
