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

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Bounce> Bounces { get; set; }

    public virtual DbSet<CardOption> CardOptions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ChildrenUnderAge> ChildrenUnderAges { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractEventInfo> ContractEventInfos { get; set; }

    public virtual DbSet<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; }

    public virtual DbSet<CustomerInfo> CustomerInfos { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<HeardResource> HeardResources { get; set; }

    public virtual DbSet<PartyPackage> PartyPackages { get; set; }

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
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.AddonId).HasName("Addons_pkey");

            entity.Property(e => e.AddonId).UseIdentityAlwaysColumn();
            entity.Property(e => e.AddonName).HasMaxLength(100);
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.AddressTypeId).HasName("AddressType_pkey");

            entity.ToTable("AddressType");

            entity.Property(e => e.AddressTypeId).UseIdentityAlwaysColumn();
            entity.Property(e => e.AddressTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Bounce>(entity =>
        {
            entity.HasKey(e => e.BounceId).HasName("Bounces_pkey");

            entity.Property(e => e.BounceId).UseIdentityAlwaysColumn();
            entity.Property(e => e.BounceName).HasMaxLength(100);
        });

        modelBuilder.Entity<CardOption>(entity =>
        {
            entity.HasKey(e => e.CardOptionId).HasName("CardOptions_pkey");

            entity.Property(e => e.CardOptionId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CardOptionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("Characters_pkey");

            entity.Property(e => e.CharacterId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CharacterName).HasMaxLength(100);
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

        modelBuilder.Entity<ContractEventInfo>(entity =>
        {
            entity.HasKey(e => e.ContractEventInfoId).HasName("Contract_EventInfo_pkey");

            entity.ToTable("Contract_EventInfo");

            entity.Property(e => e.ContractEventInfoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Contract_EventInfoId");
            entity.Property(e => e.EventInfoEndClownHour).HasColumnName("EventInfo_EndClownHour");
            entity.Property(e => e.EventInfoEventAddress)
                .HasMaxLength(1000)
                .HasColumnName("EventInfo_EventAddress");
            entity.Property(e => e.EventInfoEventCity)
                .HasMaxLength(100)
                .HasColumnName("EventInfo_EventCity");
            entity.Property(e => e.EventInfoEventDate).HasColumnName("EventInfo_EventDate");
            entity.Property(e => e.EventInfoEventState).HasColumnName("EventInfo_EventState");
            entity.Property(e => e.EventInfoEventType).HasColumnName("EventInfo_EventType");
            entity.Property(e => e.EventInfoEventZip).HasColumnName("EventInfo_EventZip");
            entity.Property(e => e.EventInfoNumberOfChildren).HasColumnName("EventInfo_NumberOfChildren");
            entity.Property(e => e.EventInfoPartyEndTime).HasColumnName("EventInfo_PartyEndTime");
            entity.Property(e => e.EventInfoPartyStartTime).HasColumnName("EventInfo_PartyStartTime");
            entity.Property(e => e.EventInfoStartClownHour).HasColumnName("EventInfo_StartClownHour");
            entity.Property(e => e.EventInfoTeamAssigned).HasColumnName("EventInfo_TeamAssigned");
            entity.Property(e => e.EventInfoVenue).HasColumnName("EventInfo_Venue");
            entity.Property(e => e.EventInfoVenueDescription)
                .HasMaxLength(1000)
                .HasColumnName("EventInfo_VenueDescription");
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

        modelBuilder.Entity<CustomerInfo>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("CustomerInfo_pkey");

            entity.ToTable("CustomerInfo");

            entity.Property(e => e.CustomerId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AlternatePhone).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Comments).HasMaxLength(1000);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(300);
            entity.Property(e => e.HonoreeName).HasMaxLength(300);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.PhoneNo).HasMaxLength(100);
            entity.Property(e => e.SpecifyOther)
                .HasMaxLength(100)
                .HasColumnName("specifyOther");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("EventType_pkey");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).UseIdentityAlwaysColumn();
            entity.Property(e => e.EventTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<HeardResource>(entity =>
        {
            entity.HasKey(e => e.HeardResourceId).HasName("HeardResource_pkey");

            entity.ToTable("HeardResource");

            entity.Property(e => e.HeardResourceId).UseIdentityAlwaysColumn();
            entity.Property(e => e.HeardResourceName).HasMaxLength(100);
        });

        modelBuilder.Entity<PartyPackage>(entity =>
        {
            entity.HasKey(e => e.PartyPackageId).HasName("PartyPackage_pkey");

            entity.ToTable("PartyPackage");

            entity.Property(e => e.PartyPackageId).UseIdentityAlwaysColumn();
            entity.Property(e => e.PartyPackageName).HasMaxLength(100);
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
