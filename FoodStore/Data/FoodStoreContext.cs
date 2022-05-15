using System;
using FoodStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodStore.Data
{
    public partial class FoodStoreContext : IdentityDbContext<Customer>
    {
        public FoodStoreContext(DbContextOptions<FoodStoreContext> options): base(options)
        {
        }

        public virtual DbSet<CustCategory> CustCategories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<PriceOffer> PriceOffers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<CustCategory>(entity => {

                entity.ToTable("CustCategory");

                entity.Property(p => p.CustCategoryId)
                .HasMaxLength(10)
                .IsFixedLength(true);

                entity.Property(p => p.CategoryName)
                .HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity => {

                entity.Property(p => p.CustCategoryId)
                .HasMaxLength(10)
                .IsFixedLength(true);

                entity.HasOne(p => p.Category)
                .WithMany(p => p.Customers)
                .HasForeignKey(p => p.CustCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Customer_CustCategory");
            });

            modelBuilder.Entity<Order>(entity => {

                entity.ToTable("Order");

                entity.Property(e => e.AddressLine).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Country).IsRequired();

                entity.HasOne(p => p.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<Category>(entity => {

                entity.ToTable("Category");

                entity.Property(p => p.CategoryId)
                .HasMaxLength(10)
                .IsFixedLength(true);

                entity.Property(p => p.CategoryName)
                .HasMaxLength(50)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(entity => {

                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<Offer>(entity => {

                entity.ToTable("Offer");

                entity.Property(p => p.OfferId)
                .HasMaxLength(10)
                .IsFixedLength(true);

                entity.Property(p => p.DiscountProcent).HasColumnType("decimal(8, 2)");

                entity.Property(p => p.CustCategoryId)
                .HasMaxLength(10)
                .IsFixedLength(true);

                entity.HasOne(p => p.CustCategory)
                .WithOne(p => p.Offer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Offer_CustCategory");
            });

            modelBuilder.Entity<PriceOffer>(entity => {

                entity.ToTable("PriceOffer");

                entity.Property(p => p.OfferId)
                 .HasMaxLength(10)
                 .IsFixedLength(true);

                entity.Property(p => p.ProductId)
                  .HasMaxLength(10)
                  .IsFixedLength(true);

                entity.Property(p => p.PromotionalText)
                .HasMaxLength(50);

                entity.Property(p => p.NewPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(p => p.Offer)
                 .WithMany(p => p.PriceOffers)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_PriceOffer_Offer");

                entity.HasOne(p => p.Product)
                .WithOne(p => p.PriceOffer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PriceOffer_Product");
            });

            modelBuilder.Entity<ProductOrder>(entity => {

                entity.ToTable("ProductOrder");
                entity.HasKey(p => new { p.ProductId, p.OrderId });

                entity.Property(e => e.ProductId)
                   .HasMaxLength(10)
                   .IsFixedLength(true);

                entity.Property(e => e.OrderId)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrder_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrder_Product");
            });

        }
    }
}
