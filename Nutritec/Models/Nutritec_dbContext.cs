using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Nutritec.Models
{
    public partial class Nutritec_dbContext : DbContext
    {
        public Nutritec_dbContext()
        {
        }

        public Nutritec_dbContext(DbContextOptions<Nutritec_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ConsumesProduct> ConsumesProducts { get; set; }
        public virtual DbSet<ConsumesRecipe> ConsumesRecipes { get; set; }
        public virtual DbSet<DailyPlan> DailyPlans { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<HasVitamin> HasVitamins { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Nutritionist> Nutritionists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientProduct> PatientProducts { get; set; }
        public virtual DbSet<PatientRecipe> PatientRecipes { get; set; }
        public virtual DbSet<PlanHa> PlanHas { get; set; }
        public virtual DbSet<PlanProductView> PlanProductViews { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeHa> RecipeHas { get; set; }
        public virtual DbSet<Vitamin> Vitamins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:nutritecapidbserver.database.windows.net,1433;Initial Catalog=Nutritec_db;Persist Security Info=False;User ID=juanignava;Password=bases2021#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ADMIN");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConsumesProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductBarcode, e.PatientEmail, e.Day, e.Meal })
                    .HasName("PK__CONSUMES__A708186632B63A41");

                entity.ToTable("CONSUMES_PRODUCT");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Day)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
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
                entity.HasKey(e => new { e.RecipeNumber, e.PatientEmail, e.Day, e.Meal })
                    .HasName("PK__CONSUMES__BC5A9E1DC9643A64");

                entity.ToTable("CONSUMES_RECIPE");

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Day)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
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
                    .HasName("PK__DAILY_PL__78A1A19C02DDD39C");

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
                    .HasName("PK__FOLLOWS__678069EEB9976E4C");

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
                    .HasName("PK__HAS_VITA__F878CB256FE6E0C0");

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
                    .HasName("PK__MEASUREM__A78C2372FDAA2639");

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
                    .HasName("PK__NUTRITIO__A9D1053506D4FF73");

                entity.ToTable("NUTRITIONIST");

                entity.HasIndex(e => new { e.Username, e.NutritionistCode, e.Id, e.CreditCardNumber }, "UQ__NUTRITIO__62CFCAA9852359A9")
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

                entity.Property(e => e.PrictureUrl)
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
                    .HasName("PK__PATIENT__A9D10535C0C054A7");

                entity.ToTable("PATIENT");

                entity.HasIndex(e => e.Username, "UQ__PATIENT__536C85E4BB6C4A36")
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
                    .HasConstraintName("PATIENT_NUTRITIONIST_FK");
            });

            modelBuilder.Entity<PatientProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PATIENT_PRODUCTS");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PatientRecipe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PATIENT_RECIPES");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlanHa>(entity =>
            {
                entity.HasKey(e => new { e.PlanNumber, e.ProductBarcode, e.Mealtime })
                    .HasName("PK__PLAN_HAS__9457BC1AE9B607F0");

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

            modelBuilder.Entity<PlanProductView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PLAN_PRODUCT_VIEW");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Mealtime)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Barcode)
                    .HasName("PK__PRODUCT__177800D24A320264");

                entity.ToTable("PRODUCT");

                entity.Property(e => e.Barcode).ValueGeneratedNever();

                entity.Property(e => e.Approved)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

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
                    .HasName("PK__RECIPE__78A1A19C334D88B8");

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
                    .HasName("PK__RECIPE_H__0298784668E3E281");

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
                    .HasName("PK__VITAMIN__A25C5AA67CCC4CD6");

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
