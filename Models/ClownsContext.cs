using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClownsCRMAPI.Models;

public partial class ClownsContext : DbContext
{
    public ClownsContext()
    {
    }

    public ClownsContext(DbContextOptions<ClownsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Clowns;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("Contracts_pkey");

            entity.Property(e => e.ContractId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ContractTimeTeamInfo>(entity =>
        {
            entity.HasKey(e => e.ContractTimeTeamInfoId).HasName("Contract_TimeTeamInfo_pkey");

            entity.ToTable("Contract_TimeTeamInfo");

            entity.Property(e => e.ContractTimeTeamInfoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Contract_TimeTeamInfoId");

            entity.HasOne(d => d.Contract).WithMany(p => p.ContractTimeTeamInfos)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Contract_TimeTeamInfo_Contracts_fkey");

            entity.HasOne(d => d.Time).WithMany(p => p.ContractTimeTeamInfos)
                .HasForeignKey(d => d.TimeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Contract_TimeTeamInfo_Teams_fkey");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.ContractTimeTeamInfos)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Contract_TimeTeamInfo_TimeSlot_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("Teams_pkey");

            entity.Property(e => e.TeamId).UseIdentityAlwaysColumn();
            entity.Property(e => e.TeamNo).HasColumnType("character varying");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("TimeSlot_pkey");

            entity.ToTable("TimeSlot");

            entity.Property(e => e.TimeSlotId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Time).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.UserId).UseIdentityAlwaysColumn();
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
