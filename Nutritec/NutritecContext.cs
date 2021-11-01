using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Nutritec.Models
{
    public partial class NutritecContext : DbContext
    {
        public NutritecContext()
        {
        }

        public NutritecContext(DbContextOptions<NutritecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConsumesProduct> ConsumesProducts { get; set; }
        public virtual DbSet<ConsumesRecipe> ConsumesRecipes { get; set; }
        public virtual DbSet<DailyPlan> DailyPlans { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<HasVitamin> HasVitamins { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Nutritionist> Nutritionists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PlanHa> PlanHas { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeHa> RecipeHas { get; set; }
        public virtual DbSet<Vitamin> Vitamins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=K5CK2UFV; Database=Nutritec; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<ConsumesProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductBarcode, e.PatientEmail })
                    .HasName("PK__CONSUMES__C8390B20536C8032");

                entity.ToTable("CONSUMES_PRODUCT");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientEmailNavigation)
                    .WithMany(p => p.ConsumesProducts)
                    .HasForeignKey(d => d.PatientEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONSUMES_PRODUCT_PATIENT_FK");

                entity.HasOne(d => d.ProductBarcodeNavigation)
                    .WithMany(p => p.ConsumesProducts)
                    .HasForeignKey(d => d.ProductBarcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONSUMES_PRODUCT_PRODUCT_FK");
            });

            modelBuilder.Entity<ConsumesRecipe>(entity =>
            {
                entity.HasKey(e => new { e.RecipeNumber, e.PatientEmail })
                    .HasName("PK__CONSUMES__D36B8D5BBD9C3BC4");

                entity.ToTable("CONSUMES_RECIPE");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientEmailNavigation)
                    .WithMany(p => p.ConsumesRecipes)
                    .HasForeignKey(d => d.PatientEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONSUMES_RECIPE_PATIENT_FK");

                entity.HasOne(d => d.RecipeNumberNavigation)
                    .WithMany(p => p.ConsumesRecipes)
                    .HasForeignKey(d => d.RecipeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONSUMES_RECIPE_RECIPE_FK");
            });

            modelBuilder.Entity<DailyPlan>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK__DAILY_PL__78A1A19CFC50F062");

                entity.ToTable("DAILY_PLAN");

                entity.Property(e => e.Number).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NutritionistEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NutritionistEmailNavigation)
                    .WithMany(p => p.DailyPlans)
                    .HasForeignKey(d => d.NutritionistEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DAILY_PLAN_NUTRITIONIST_FK");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.PatientEmail, e.PlanNumber })
                    .HasName("PK__FOLLOWS__678069EE751FF2DD");

                entity.ToTable("FOLLOWS");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientEmailNavigation)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.PatientEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FOLLOWS_PATIENT_FK");

                entity.HasOne(d => d.PlanNumberNavigation)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.PlanNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FOLLOWS_DAILY_PLAN");
            });

            modelBuilder.Entity<HasVitamin>(entity =>
            {
                entity.HasKey(e => new { e.ProductBarcode, e.VitaminCode })
                    .HasName("PK__HAS_VITA__F878CB2596245A87");

                entity.ToTable("HAS_VITAMIN");

                entity.Property(e => e.VitaminCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductBarcodeNavigation)
                    .WithMany(p => p.HasVitamins)
                    .HasForeignKey(d => d.ProductBarcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HAS_VITAMIN_PRODUCT_FK");

                entity.HasOne(d => d.VitaminCodeNavigation)
                    .WithMany(p => p.HasVitamins)
                    .HasForeignKey(d => d.VitaminCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HAS_VITAMIN_VITAMIN_FK");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasKey(e => new { e.PatientEmail, e.Number })
                    .HasName("PK__MEASUREM__A78C2372DC42F2A7");

                entity.ToTable("MEASUREMENT");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.PatientEmailNavigation)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.PatientEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MEASUREMENT_PATIENT_FK");
            });

            modelBuilder.Entity<Nutritionist>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__NUTRITIO__A9D10535126D8F33");

                entity.ToTable("NUTRITIONIST");

                entity.HasIndex(e => new { e.Username, e.NutritionistCode, e.Id, e.CreditCardNumber }, "UQ__NUTRITIO__62CFCAA9ADDC41F9")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Canton)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__PATIENT__A9D105355CF41613");

                entity.ToTable("PATIENT");

                entity.HasIndex(e => e.Username, "UQ__PATIENT__536C85E4FA399C6E")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NutritionistEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Passowrd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NutritionistEmailNavigation)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.NutritionistEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PATIENT_NUTRITIONIST_FK");
            });

            modelBuilder.Entity<PlanHa>(entity =>
            {
                entity.HasKey(e => new { e.PlanNumber, e.ProductBarcode, e.Mealtime })
                    .HasName("PK__PLAN_HAS__9457BC1AE865FA00");

                entity.ToTable("PLAN_HAS");

                entity.Property(e => e.Mealtime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PlanNumberNavigation)
                    .WithMany(p => p.PlanHas)
                    .HasForeignKey(d => d.PlanNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PLAN_HAS_DAILY_PLAN_FK");

                entity.HasOne(d => d.ProductBarcodeNavigation)
                    .WithMany(p => p.PlanHas)
                    .HasForeignKey(d => d.ProductBarcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PLAN_HAS_PRODUCT_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Barcode)
                    .HasName("PK__PRODUCT__177800D216952DE2");

                entity.ToTable("PRODUCT");

                entity.Property(e => e.Barcode).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK__RECIPE__78A1A19C5D3B5A77");

                entity.ToTable("RECIPE");

                entity.Property(e => e.Number).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PatientEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientEmailNavigation)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.PatientEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RECIPE_PATIENT_FK");
            });

            modelBuilder.Entity<RecipeHa>(entity =>
            {
                entity.HasKey(e => new { e.RecipeNumber, e.ProductBarcode })
                    .HasName("PK__RECIPE_H__02987846608AFC29");

                entity.ToTable("RECIPE_HAS");

                entity.HasOne(d => d.ProductBarcodeNavigation)
                    .WithMany(p => p.RecipeHas)
                    .HasForeignKey(d => d.ProductBarcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RECIPE_HAS_PRODUCT_FK");

                entity.HasOne(d => d.RecipeNumberNavigation)
                    .WithMany(p => p.RecipeHas)
                    .HasForeignKey(d => d.RecipeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RECIPE_HAS_RECIPE_FK");
            });

            modelBuilder.Entity<Vitamin>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__VITAMIN__A25C5AA6ADBB9B8E");

                entity.ToTable("VITAMIN");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
