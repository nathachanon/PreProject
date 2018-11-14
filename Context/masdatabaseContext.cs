using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MASdemo.Context
{
    public partial class masdatabaseContext : DbContext
    {
        public masdatabaseContext()
        {
        }

        public masdatabaseContext(DbContextOptions<masdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CalInfoRoom> CalInfoRoom { get; set; }
        public virtual DbSet<Dorm> Dorm { get; set; }
        public virtual DbSet<Renter> Renter { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Roomtype> Roomtype { get; set; }
        public virtual DbSet<SetFloorRoom> SetFloorRoom { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=masdatabase;user=root;pwd=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalInfoRoom>(entity =>
            {
                entity.HasKey(e => e.CalId)
                    .HasName("PRIMARY");

                entity.ToTable("cal_info_room");

                entity.HasIndex(e => e.Rid)
                    .HasName("RID");

                entity.Property(e => e.CalId)
                    .HasColumnName("Cal_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateTime)
                    .HasColumnName("Date")
                    .HasColumnType("date");

                entity.Property(e => e.ElecMeter)
                    .HasColumnName("Elec_meter")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rid)
                    .HasColumnName("RID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TatalAmount)
                    .HasColumnName("Tatal_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalElecAmount)
                    .HasColumnName("Total_elec_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalElecUnit)
                    .HasColumnName("Total_elec_unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalWaterAmount)
                    .HasColumnName("Total_water_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalWaterUnit)
                    .HasColumnName("Total_water_unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WaterMeter)
                    .HasColumnName("Water_meter")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.R)
                    .WithMany(p => p.CalInfoRoom)
                    .HasForeignKey(d => d.Rid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cal_info_room_ibfk_1");
            });

            modelBuilder.Entity<Dorm>(entity =>
            {
                entity.HasKey(e => e.Did)
                    .HasName("PRIMARY");

                entity.ToTable("dorm");

                entity.HasIndex(e => e.Oid)
                    .HasName("OID");

                entity.Property(e => e.Did)
                    .HasColumnName("DID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddNo).HasColumnType("varchar(20)");

                entity.Property(e => e.District).HasColumnType("varchar(20)");

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasColumnName("DName")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Picture)
                    .HasColumnName("DPicture")
                    .HasColumnType("text");

                entity.Property(e => e.Province).HasColumnType("varchar(20)");

                entity.Property(e => e.SetElecUnit)
                    .HasColumnName("Set_elec_unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SetWaterUnit)
                    .HasColumnName("Set_water_unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Street).HasColumnType("varchar(20)");

                entity.Property(e => e.SubDistrict).HasColumnType("varchar(20)");

                entity.Property(e => e.Oid)
                    .HasColumnName("OID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ZipCode).HasColumnType("varchar(20)");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Dorm)
                    .HasForeignKey(d => d.Oid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dorm_ibfk_1");
            });

            modelBuilder.Entity<Renter>(entity =>
            {
                entity.HasKey(e => e.RenId)
                    .HasName("PRIMARY");

                entity.ToTable("renter");

                entity.HasIndex(e => e.Rid)
                    .HasName("RID");

                entity.Property(e => e.RenId)
                    .HasColumnName("Ren_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RenAge)
                    .HasColumnName("Ren_Age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RenAgreement)
                    .HasColumnName("Ren_Agreement")
                    .HasColumnType("text");

                entity.Property(e => e.RenName)
                    .HasColumnName("Ren_Name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RenSsnPicture)
                    .HasColumnName("Ren_SSN_Picture")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.RenSurename)
                    .HasColumnName("Ren_Surename")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RenTel)
                    .HasColumnName("Ren_Tel")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.Rid)
                    .HasColumnName("RID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartElecMeter)
                    .HasColumnName("Start_Elec_Meter")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartWaterMeter)
                    .HasColumnName("Start_Water_Meter")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateTime)
                    .HasColumnName("Ren_StartDate")
                    .HasColumnType("date");


                entity.HasOne(d => d.R)
                    .WithMany(p => p.Renter)
                    .HasForeignKey(d => d.Rid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("renter_ibfk_1");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PRIMARY");

                entity.ToTable("room");

                entity.HasIndex(e => e.Did)
                    .HasName("DID");

                entity.HasIndex(e => e.Tid)
                    .HasName("TID");

                entity.Property(e => e.Rid)
                    .HasColumnName("RID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Did)
                    .HasColumnName("DID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoomNumber).HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Tid)
                    .HasColumnName("TID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.D)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.Did)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_ibfk_1");

                entity.HasOne(d => d.T)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_ibfk_2");
            });

            modelBuilder.Entity<Roomtype>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PRIMARY");

                entity.ToTable("roomtype");

                entity.HasIndex(e => e.Did)
                    .HasName("DID");

                entity.Property(e => e.Tid)
                    .HasColumnName("TID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Did)
                    .HasColumnName("DID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.D)
                    .WithMany(p => p.Roomtype)
                    .HasForeignKey(d => d.Did)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("roomtype_ibfk_1");
            });

            modelBuilder.Entity<SetFloorRoom>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("set_floor_room");

                entity.HasIndex(e => e.Did)
                    .HasName("DID");

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Did)
                    .HasColumnName("DID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Floor).HasColumnType("int(11)");

                entity.Property(e => e.Room).HasColumnType("int(11)");

                entity.HasOne(d => d.D)
                    .WithMany(p => p.SetFloorRoom)
                    .HasForeignKey(d => d.Did)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("set_floor_room_ibfk_1");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Oid)
                    .HasName("PRIMARY");

                entity.ToTable("Owner");

                entity.Property(e => e.Oid)
                    .HasColumnName("Oid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Picture).HasColumnType("text");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnType("varchar(12)");
            });
        }
    }
}
