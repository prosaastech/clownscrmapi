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

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ChildrenUnderAge> ChildrenUnderAges { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; }

    public virtual DbSet<HeardResource> HeardResources { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Clowns;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.AddressTypeId).HasName("AddressType_pkey");

            entity.ToTable("AddressType");

            entity.Property(e => e.AddressTypeId).UseIdentityAlwaysColumn();
            entity.Property(e => e.AddressTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildrenId).HasName("Children_pkey");

            entity.Property(e => e.ChildrenId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<ChildrenUnderAge>(entity =>
        {
            entity.HasKey(e => e.ChildrenUnderAgeId).HasName("ChildrenUnderAge_pkey");

            entity.ToTable("ChildrenUnderAge");

            entity.Property(e => e.ChildrenUnderAgeId).UseIdentityAlwaysColumn();
        });

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

            entity.HasOne(d => d.Team).WithMany(p => p.ContractTimeTeamInfos)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TeamsContrant");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.ContractTimeTeamInfos)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TimeSlotContrant");
        });

        modelBuilder.Entity<HeardResource>(entity =>
        {
            entity.HasKey(e => e.HeardResourceId).HasName("HeardResource_pkey");

            entity.ToTable("HeardResource");

            entity.Property(e => e.HeardResourceId).UseIdentityAlwaysColumn();
            entity.Property(e => e.HeardResourceName).HasMaxLength(100);
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => e.RelationshipId).HasName("Relationship_pkey");

            entity.ToTable("Relationship");

            entity.Property(e => e.RelationshipId).UseIdentityAlwaysColumn();
            entity.Property(e => e.RelationshipName).HasMaxLength(100);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("State_pkey");

            entity.ToTable("State");

            entity.Property(e => e.StateId).UseIdentityAlwaysColumn();
            entity.Property(e => e.StateName).HasMaxLength(100);
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

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("Venue_pkey");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId).UseIdentityAlwaysColumn();
            entity.Property(e => e.VenueName).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
