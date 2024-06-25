using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public partial class finsContext : DbContext
    {
        public finsContext()
        {
        }

        public finsContext(DbContextOptions<finsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Edge> Edges { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<Mapmanage> Mapmanages { get; set; }
        public virtual DbSet<Mappoint> Mappoints { get; set; }
        public virtual DbSet<Mappointex> Mappointices { get; set; }
        public virtual DbSet<Mappointroute> Mappointroutes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();

                optionsBuilder.UseMySql(configuration.GetConnectionString("Project"), ServerVersion.AutoDetect(configuration.GetConnectionString("Project")),
                    mysqlOptions => mysqlOptions.UseNetTopologySuite()); // Enable NetTopologySuite for MySQL

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.HasIndex(e => e.FacilityId, "FK_Building_Facility_idx");

                entity.Property(e => e.BuildingName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Building_Facility");
            });

            modelBuilder.Entity<Edge>(entity =>
            {
                entity.ToTable("edge");

                entity.HasIndex(e => e.MapPointB, "FK_Edge_MPB_idx");

                entity.HasIndex(e => e.MapPointA, "FK_Edge_MP_idx");

                entity.Property(e => e.Direction).HasDefaultValueSql("'2'");

                entity.HasOne(d => d.MapPointANavigation)
                    .WithMany(p => p.EdgeMapPointANavigations)
                    .HasForeignKey(d => d.MapPointA)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Edge_MPA");

                entity.HasOne(d => d.MapPointBNavigation)
                    .WithMany(p => p.EdgeMapPointBNavigations)
                    .HasForeignKey(d => d.MapPointB)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Edge_MPB");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("floor");

                entity.HasIndex(e => e.BuildingId, "FK_Building_Floor_idx");

                entity.Property(e => e.FloorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Greeting)
                    .IsRequired()
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Floors)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Building_Floor");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("map");

                entity.HasIndex(e => e.FloorId, "FK_Map_Floor_idx");

                entity.Property(e => e.MapImage2D).IsRequired();

                entity.Property(e => e.MapName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Map_Floor");
            });

            modelBuilder.Entity<Mapmanage>(entity =>
            {
                entity.HasKey(e => new { e.MapId, e.MemberId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("mapmanage");

                entity.HasIndex(e => e.MemberId, "FK_Member_Map_idx");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Mapmanages)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Map_Member");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Mapmanages)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Map");
            });

            modelBuilder.Entity<Mappoint>(entity =>
            {
                entity.ToTable("mappoint");

                entity.HasIndex(e => e.BuildingId, "FK_MP_Building_idx");

                entity.HasIndex(e => e.FloorId, "FK_MP_Floor_idx");

                entity.HasIndex(e => e.MapId, "FK_MP_Map_idx");

                entity.Property(e => e.Destination).HasDefaultValueSql("'0'");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.LocationGps).HasColumnName("LocationGPS");

                entity.Property(e => e.MapPointName).HasMaxLength(50);
                entity.Property(e => e.LocationApp).IsRequired();

                entity.Property(e => e.LocationGps).HasColumnName("LocationGPS");

                entity.Property(e => e.LocationWeb).IsRequired();

                entity.Property(e => e.MapPointName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Mappoints)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MP_Building");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Mappoints)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MP_Floor");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Mappoints)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MP_Map");
            });

            modelBuilder.Entity<Mappointex>(entity =>
            {
                entity.ToTable("mappointex");

                entity.HasIndex(e => e.MapPointId, "FK_MPEX_MP_idx");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("URL");

                entity.HasOne(d => d.MapPoint)
                    .WithMany(p => p.Mappointices)
                    .HasForeignKey(d => d.MapPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MPEX_MP");
            });

            modelBuilder.Entity<Mappointroute>(entity =>
            {
                entity.HasKey(e => new { e.MapPointId, e.RouteId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("mappointroute");

                entity.HasIndex(e => e.RouteId, "FK_MPR_Route");

                entity.Property(e => e.MapPointId).HasColumnName("MapPointID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.Property(e => e.MpOrder).HasColumnName("MP_Order");

                entity.HasOne(d => d.MapPoint)
                    .WithMany(p => p.Mappointroutes)
                    .HasForeignKey(d => d.MapPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MPR_MP");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Mappointroutes)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MPR_Route");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "FK_Role_Member_idx");

                entity.HasIndex(e => e.Username, "Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar).HasMaxLength(45);

                entity.Property(e => e.Country).HasMaxLength(45);

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')")
                    .HasDefaultValueSql("'deactive'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Member");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Description).HasMaxLength(45);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");
                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("section");

                entity.HasIndex(e => e.FloorId, "FK_Section_Floor_idx");

                entity.Property(e => e.SectionName).HasMaxLength(45);

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Section_Floor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
