using System;
using System.Collections.Generic;
using BloodBankDB.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace BloodBankDB.EF;

public partial class BloodBankDBContext : DbContext
{
    public BloodBankDBContext()
    {
    }

    public BloodBankDBContext(DbContextOptions<BloodBankDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=shahriyer\\SQLEXPRESS;Database=BloodBankDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>(entity =>
        {
            entity.ToTable("Donation");

            entity.Property(e => e.CampName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Donor)
                .WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.ToTable("Donor");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.City)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.ContactNo)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}