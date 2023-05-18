using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace courseMarathon.Models;

public partial class MarathonContext : DbContext
{
    public MarathonContext()
    {
    }

    public MarathonContext(DbContextOptions<MarathonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DistanceType> DistanceTypes { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=> optionsBuilder.UseSqlServer("Data Source=USER-PC\\SQLEXPRESS;Initial Catalog=Marathon;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistanceType>(entity =>
        {
            entity.ToTable("DistanceType");

            entity.Property(e => e.DistanceTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.MemberId).ValueGeneratedNever();
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Fullname).HasMaxLength(50);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.ToTable("Registration");

            entity.Property(e => e.RegistrationId).ValueGeneratedNever();
            entity.Property(e => e.IntermediatePoint)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.DistanceType).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.DistanceTypeId)
                .HasConstraintName("FK_Registration_DistanceType");

            entity.HasOne(d => d.Member).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Registration_Member");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
