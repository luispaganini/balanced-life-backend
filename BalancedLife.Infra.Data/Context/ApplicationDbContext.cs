﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using BalancedLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Body> Bodies { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodGroup> FoodGroups { get; set; }

    public virtual DbSet<FoodNutritionInfo> FoodNutritionInfos { get; set; }

    public virtual DbSet<ItemsPlan> ItemsPlans { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<NutritionalComposition> NutritionalCompositions { get; set; }

    public virtual DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

    public virtual DbSet<PlanDiet> PlanDiets { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Snack> Snacks { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StatusUser> StatusUsers { get; set; }

    public virtual DbSet<TypeSnack> TypeSnacks { get; set; }

    public virtual DbSet<UnitMeasurement> UnitMeasurements { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserPatientLink> UserPatientLinks { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Body>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__body__3213E83F0B1685D1");

            entity.ToTable("body");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Bodies)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("body_iduser_foreign");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__city__3213E83F96C813B2");

            entity.ToTable("city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdState).HasColumnName("idState");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdStateNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_idstate_foreign");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__food__3213E83FFA8ADDD7");

            entity.ToTable("food");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.IdFoodGroup).HasColumnName("idFoodGroup");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ReferenceTable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenceTable");

            entity.HasOne(d => d.IdFoodGroupNavigation).WithMany(p => p.Foods)
                .HasForeignKey(d => d.IdFoodGroup)
                .HasConstraintName("food_idfoodgroup_foreign");
        });

        modelBuilder.Entity<FoodGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__foodGrou__3213E83F24B0C950");

            entity.ToTable("foodGroup");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FoodNutritionInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__foodNutr__3213E83FE3FDB4C5");

            entity.ToTable("foodNutritionInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.IdNutritionalComposition).HasColumnName("idNutritionalComposition");
            entity.Property(e => e.IdUnitMeasurement).HasColumnName("idUnitMeasurement");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.FoodNutritionInfos)
                .HasForeignKey(d => d.IdFood)
                .HasConstraintName("foodNutritionInfo_idfood_foreign");

            entity.HasOne(d => d.IdNutritionalCompositionNavigation).WithMany(p => p.FoodNutritionInfos)
                .HasForeignKey(d => d.IdNutritionalComposition)
                .HasConstraintName("foodNutritionInfo_nutritionalComposition_foreign");

            entity.HasOne(d => d.IdUnitMeasurementNavigation).WithMany(p => p.FoodNutritionInfos)
                .HasForeignKey(d => d.IdUnitMeasurement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foodnutritioninfo_idunitmeasurement_foreign");
        });

        modelBuilder.Entity<ItemsPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__itemsPla__3213E83F066954A3");

            entity.ToTable("itemsPlan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.IdPlan).HasColumnName("idPlan");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.ItemsPlans)
                .HasForeignKey(d => d.IdFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("itemsplan_idfood_foreign");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.ItemsPlans)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("itemsplan_idplan_foreign");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__meals__3213E83F430D8140");

            entity.ToTable("meal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Appointment)
                .HasColumnType("datetime")
                .HasColumnName("appointment");
            entity.Property(e => e.IdTypeSnack).HasColumnName("idTypeSnack");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Observation).HasColumnName("observation");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdTypeSnackNavigation).WithMany(p => p.Meals)
                .HasForeignKey(d => d.IdTypeSnack)
                .HasConstraintName("FK_TypeSnack");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Meals)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInfo");
        });

        modelBuilder.Entity<NutritionalComposition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nutritio__3213E83FB0FE6D4E");

            entity.ToTable("nutritionalComposition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Item)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item");
        });

        modelBuilder.Entity<PasswordResetCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__password__3213E83F78961A5F");

            entity.ToTable("passwordResetCode");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expiresAt");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IsUsed).HasColumnName("isUsed");
            entity.Property(e => e.VerificationCode)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("verificationCode");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PasswordResetCodes)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        modelBuilder.Entity<PlanDiet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__planDiet__3213E83F72D6552F");

            entity.ToTable("planDiet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNutricionist).HasColumnName("idNutricionist");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdNutricionistNavigation).WithMany(p => p.PlanDiets)
                .HasForeignKey(d => d.IdNutricionist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_idnutricionist_foreign");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3213E83F6C475DCA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiryDate");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Invalidated).HasColumnName("invalidated");
            entity.Property(e => e.JwtId)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("jwtId");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("token");
            entity.Property(e => e.Used).HasColumnName("used");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshTokens_Users");
        });

        modelBuilder.Entity<Snack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__snack__3213E83FC6D4312A");

            entity.ToTable("snack");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Appointment)
                .HasColumnType("datetime")
                .HasColumnName("appointment");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.IdMeal).HasColumnName("idMeal");
            entity.Property(e => e.IdTypeSnack).HasColumnName("idTypeSnack");
            entity.Property(e => e.IdUnitMeasurement).HasColumnName("idUnitMeasurement");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SnackCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_snack_created_by");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.Snacks)
                .HasForeignKey(d => d.IdFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("snack_idfood_foreign");

            entity.HasOne(d => d.IdMealNavigation).WithMany(p => p.Snacks)
                .HasForeignKey(d => d.IdMeal)
                .HasConstraintName("FK_meals");

            entity.HasOne(d => d.IdTypeSnackNavigation).WithMany(p => p.Snacks)
                .HasForeignKey(d => d.IdTypeSnack)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("snack_idtypesnack_foreign");

            entity.HasOne(d => d.IdUnitMeasurementNavigation).WithMany(p => p.Snacks)
                .HasForeignKey(d => d.IdUnitMeasurement)
                .HasConstraintName("FK_UnitMeasurement");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SnackUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_snack_updated_by");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__state__3213E83FCD3E66C3");

            entity.ToTable("state");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Uf)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("uf");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__status__3213E83F8753957B");

            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<StatusUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statusUs__3213E83FF176B859");

            entity.ToTable("statusUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNutricionist).HasColumnName("idNutricionist");
            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdNutricionistNavigation).WithMany(p => p.StatusUserIdNutricionistNavigations)
                .HasForeignKey(d => d.IdNutricionist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("statususer_idnutricionist_foreign");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.StatusUsers)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("statususer_idstatus_foreign");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.StatusUserIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("statususer_iduser_foreign");
        });

        modelBuilder.Entity<TypeSnack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typeSnac__3213E83F213FB233");

            entity.ToTable("typeSnack");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<UnitMeasurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__unitMeas__3213E83F0B6F18AF");

            entity.ToTable("unitMeasurement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userInfo__3213E83F2F737ACD");

            entity.ToTable("userInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birth)
                .HasColumnType("datetime")
                .HasColumnName("birth");
            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cpf");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ExpirationLicence)
                .HasColumnType("datetime")
                .HasColumnName("expirationLicence");
            entity.Property(e => e.Facebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("facebook");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.IdCity).HasColumnName("idCity");
            entity.Property(e => e.IdUserRole).HasColumnName("idUserRole");
            entity.Property(e => e.Instagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instagram");
            entity.Property(e => e.IsCompleteProfile).HasColumnName("isCompleteProfile");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("urlImage");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("whatsapp");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("zipCode");

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("user_idcity_foreign");

            entity.HasOne(d => d.IdUserRoleNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.IdUserRole)
                .HasConstraintName("user_iduserlevel_foreign");
        });

        modelBuilder.Entity<UserPatientLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userPati__3213E83F183C571F");

            entity.ToTable("userPatientLink");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNutritionist).HasColumnName("idNutritionist");
            entity.Property(e => e.IdPatient).HasColumnName("idPatient");
            entity.Property(e => e.IsCurrentNutritionist).HasColumnName("isCurrentNutritionist");
            entity.Property(e => e.LinkStatus).HasColumnName("linkStatus");

            entity.HasOne(d => d.IdNutritionistNavigation).WithMany(p => p.UserPatientLinkIdNutritionistNavigations)
                .HasForeignKey(d => d.IdNutritionist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nutritionist");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.UserPatientLinkIdPatientNavigations)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userLeve__3213E83FBFA99A8F");

            entity.ToTable("userRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}