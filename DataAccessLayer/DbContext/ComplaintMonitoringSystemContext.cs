using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccessLayer.Models;

namespace DataAccessLayer.DbContext
{
    public partial class ComplaintMonitoringSystemContext : IdentityDbContext<IdentityUser>
    {
        public ComplaintMonitoringSystemContext()
        {
        }

        public ComplaintMonitoringSystemContext(DbContextOptions<ComplaintMonitoringSystemContext> options)
            : base(options)
        {
        }


        public virtual DbSet<ComplaintsOfEmployee> ComplaintsOfEmployees { get; set; } = null!;
        public virtual DbSet<ComplientBox> ComplientBoxes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ComplaintMonitoringSystem;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ComplaintsOfEmployee>(entity =>
            {
                entity.ToTable("ComplaintsOfEmployee");

                entity.Property(e => e.ComplaintId).HasMaxLength(350);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Issue).HasMaxLength(50);

                entity.Property(e => e.LoginId).HasMaxLength(450);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Resolutation).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.ComplaintsOfEmployees)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplaintsOfEmployee_ComplaintsOfEmployee");
            });

            modelBuilder.Entity<ComplientBox>(entity =>
            {
                entity.HasKey(e => e.ComplientId);

                entity.ToTable("ComplientBox");

                entity.Property(e => e.ComplientId).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasMaxLength(50);

                entity.Property(e => e.Issue).HasMaxLength(200);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Resolution).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
