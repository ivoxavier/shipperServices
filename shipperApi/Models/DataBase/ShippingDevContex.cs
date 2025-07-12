using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shipperApi.Models.DataBase;

public partial class ShippingDevContex : DbContext
{
    public ShippingDevContex(DbContextOptions<ShippingDevContex> options)
        : base(options)
    {
    }

    public virtual DbSet<ShipDetail> ShipDetails { get; set; }

    public virtual DbSet<ShipHeader> ShipHeaders { get; set; }

    public virtual DbSet<ShipReport> ShipReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ShipDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ship_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HeaderId).HasColumnName("headerID");
            entity.Property(e => e.RefNumber)
                .HasMaxLength(45)
                .HasColumnName("refNumber");
        });

        modelBuilder.Entity<ShipHeader>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ship_headers");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Dest).HasMaxLength(45);
            entity.Property(e => e.Torders).HasColumnName("TOrders");
        });

        modelBuilder.Entity<ShipReport>(entity =>
        {
            entity.HasKey(e => e.IdUnico).HasName("PRIMARY");

            entity.ToTable("ship_reports");

            entity.Property(e => e.IdUnico)
                .ValueGeneratedNever()
                .HasColumnName("idUnico");
            entity.Property(e => e.ReportName)
                .HasMaxLength(45)
                .HasColumnName("reportName");
            entity.Property(e => e.ReportPath)
                .HasMaxLength(45)
                .HasColumnName("reportPath");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
