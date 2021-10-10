using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace karma
{
    public partial class dbkarmaContext : DbContext
    {
        public dbkarmaContext()
        {
        }

        public dbkarmaContext(DbContextOptions<dbkarmaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<Charity> Charities { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.DBConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("announcements");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Added)
                    .HasColumnType("datetime")
                    .HasColumnName("added");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Charity>(entity =>
            {
                entity.ToTable("charity");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Added)
                    .HasColumnType("datetime")
                    .HasColumnName("added");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Website).HasColumnName("website");
            });

            modelBuilder.Entity<Listing>(entity =>
            {
                entity.ToTable("listings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Added)
                    .HasColumnType("datetime")
                    .HasColumnName("added");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
