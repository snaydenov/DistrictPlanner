using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DistrictPlanner.Models
{
    public partial class DistrictPlannerContext : DbContext
    {
        public DistrictPlannerContext()
        {
        }

        public DistrictPlannerContext(DbContextOptions<DistrictPlannerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Road> Roads { get; set; }
        public virtual DbSet<Settlement> Settlements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Road>(entity =>
            {
                entity.HasKey(e => e.RoadId)
                    .HasName("PK__tmp_ms_x__C5D162EAEB0E5D2D");

                entity.Property(e => e.Distance).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.EndSettlement)
                    .WithMany(p => p.RoadsEndSettlement)
                    .HasForeignKey(d => d.EndSettlementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roads__EndSettle__5441852A");

                entity.HasOne(d => d.StartSettlement)
                    .WithMany(p => p.RoadsStartSettlement)
                    .HasForeignKey(d => d.StartSettlementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roads__StartSett__534D60F1");
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.HasKey(e => e.SettlementId)
                    .HasName("PK__tmp_ms_x__7712545A8C6809F6");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
