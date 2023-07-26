using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LapShop.Migrations;

namespace LapShop.Models;

public partial class LapShopContext : IdentityDbContext<ApplicationUser>
{
    public LapShopContext()
    {
    }

    public LapShopContext(DbContextOptions<LapShopContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<TbBusinessInfo> TbBusinessInfos { get; set; }

    public virtual DbSet<TbCashTransacion> TbCashTransacions { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbItem> TbItems { get; set; }

    public virtual DbSet<TbItemDiscount> TbItemDiscounts { get; set; }

    public virtual DbSet<TbItemImage> TbItemImages { get; set; }

    public virtual DbSet<TbItemType> TbItemTypes { get; set; }

    public virtual DbSet<TbO> TbOs { get; set; }

    public virtual DbSet<TbPurchaseInvoice> TbPurchaseInvoices { get; set; }

    public virtual DbSet<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; }

    public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; }

    public virtual DbSet<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }

    public virtual DbSet<TbSlider> TbSliders { get; set; }

    public virtual DbSet<TbSupplier> TbSuppliers { get; set; }
    public virtual DbSet<TbSettings> TbSettings { get; set; }
    public virtual DbSet<TbPages> TbPages { get; set; }

    public virtual DbSet<VwItem> VwItems { get; set; }

    public virtual DbSet<VwItemCategory> VwItemCategories { get; set; }

    public virtual DbSet<VwItemsOutOfInvoice> VwItemsOutOfInvoices { get; set; }

    public virtual DbSet<VwSalesInvoice> VwSalesInvoices { get; set; }
    public virtual DbSet<VwItemCategories> VwItemCategory { get; set; }
    public virtual DbSet<VwSalseInvoicesItems> VwSalseInvoicesItem { get; set; }
    public virtual DbSet<VwSalseInvoiceSuums> VwSalseInvoiceSuum { get; set; }
    public virtual DbSet<VwSalseInvoiceSuumss> VwSalseInvoiceSumm { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TbBusinessInfo>(entity =>
        {
            entity.HasKey(e => e.BusinessInfoId);

            entity.ToTable("TbBusinessInfo");

            entity.HasIndex(e => e.CutomerId, "IX_TbBusinessInfo_CutomerId").IsUnique();

            entity.Property(e => e.Budget).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.BusinessCardNumber).HasMaxLength(20);

            entity.HasOne(d => d.Cutomer).WithOne(p => p.TbBusinessInfo).HasForeignKey<TbBusinessInfo>(d => d.CutomerId);
        });

        modelBuilder.Entity<TbCashTransacion>(entity =>
        {
            entity.HasKey(e => e.CashTransactionId);

            entity.ToTable("TbCashTransacion");

            entity.Property(e => e.CashDate).HasColumnType("datetime");
            entity.Property(e => e.CashValue).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ImageName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.ShowInHomePage)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerName).HasMaxLength(100);
        });
        modelBuilder.Entity<TbPages>(entity =>
        {
            entity.HasKey(e => e.PageId);

            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<TbItem>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.HasIndex(e => e.ItemTypeId, "IX_TbItems_ItemTypeId");

            entity.HasIndex(e => e.OsId, "IX_TbItems_OsId");

            entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("('2020-09-20T00:00:00.0000000')");
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.ItemTypeId).HasDefaultValueSql("((0))");
            entity.Property(e => e.OsId).HasDefaultValueSql("((0))");
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItems_TbCategories");

            entity.HasOne(d => d.ItemType).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ItemTypeId)
                .HasConstraintName("FK_TbItems_TbItemTypes");

            entity.HasOne(d => d.Os).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.OsId)
                .HasConstraintName("FK_TbItems_TbOs");

            entity.HasMany(d => d.Customers).WithMany(p => p.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "TbCustomerItem",
                    r => r.HasOne<TbCustomer>().WithMany().HasForeignKey("CustomerId"),
                    l => l.HasOne<TbItem>().WithMany().HasForeignKey("ItemId"),
                    j =>
                    {
                        j.HasKey("ItemId", "CustomerId");
                        j.HasIndex(new[] { "CustomerId" }, "IX_TbCustomerItems_CustomerId");
                    });
        });

        modelBuilder.Entity<TbItemDiscount>(entity =>
        {
            entity.HasKey(e => e.ItemDiscountId);

            entity.ToTable("TbItemDiscount");

            entity.HasIndex(e => e.ItemId, "IX_TbItemDiscount_ItemId");

            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.TbItemDiscounts)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItemDiscounts_TbItems");
        });

        modelBuilder.Entity<TbItemImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.Property(e => e.ImageName).HasMaxLength(200);

            entity.HasOne(d => d.Item).WithMany(p => p.TbItemImages)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItemImages_TbItems");
        });

        modelBuilder.Entity<TbItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId);

            entity.Property(e => e.ItemTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbO>(entity =>
        {
            entity.HasKey(e => e.OsId);

            entity.Property(e => e.OsName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbPurchaseInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TbPurchaseInvoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoices_TbSuppliers");
        });

        modelBuilder.Entity<TbPurchaseInvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId);

            entity.Property(e => e.InvoiceItemId).ValueGeneratedNever();
            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbPurchaseInvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceItems_TbPurchaseInvoices");

            entity.HasOne(d => d.Item).WithMany(p => p.TbPurchaseInvoiceItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceItems_TbItems");
        });

        modelBuilder.Entity<TbSalesInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.DelivryDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TbSalesInvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId);

            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbSalesInvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceItems_TbSalesInvoices");

            entity.HasOne(d => d.Item).WithMany(p => p.TbSalesInvoiceItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceItems_TbItems");
        });

        modelBuilder.Entity<TbSlider>(entity =>
        {
            entity.HasKey(e => e.SliderId);

            entity.ToTable("TbSlider");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK_TbSupplier");

            entity.Property(e => e.SupplierName).HasMaxLength(100);
        });
        modelBuilder.Entity<TbSettings>(entity =>
        {
            entity.HasKey(e => e.Id);

        });

        modelBuilder.Entity<VwItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItems");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.ItemTypeName).HasMaxLength(100);
            entity.Property(e => e.OsName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
        });
        modelBuilder.Entity<VwItemCategories>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwItemCategory");
        });
        modelBuilder.Entity<VwSalseInvoicesItems>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwSalseInvoicesItem");
        });
        modelBuilder.Entity<VwSalseInvoiceSuumss>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwSalseInvoiceSumm");
        });
       
        modelBuilder.Entity<VwSalseInvoiceSuums>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(" VwSalseInvoiceSuum");
        });
       
        modelBuilder.Entity<VwItemCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItemCategories");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<VwItemsOutOfInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItemsOutOfInvoices");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<VwSalesInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSalesInvoices");

            entity.Property(e => e.DelivryDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
