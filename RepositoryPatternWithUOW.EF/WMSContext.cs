using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RepositoryPatternWithUOW.EF.Repositories;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WMSContext : IdentityDbContext<ApplicationUser>
    {
        public WMSContext()
        {
        }

        public WMSContext(DbContextOptions<WMSContext> options)
            : base(options)
        {

        }


        public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }
        public virtual DbSet<AuthenticationProvider> AuthenticationProviders { get; set; }
        public virtual DbSet<AuthenticationProviderParameter> AuthenticationProviderParameters { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CapitalType> CapitalTypes { get; set; }
        public virtual DbSet<CodeType> CodeTypes { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<ConfigurationCategory> ConfigurationCategories { get; set; }
        public virtual DbSet<ConstraintType> ConstraintTypes { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<DivisionHierarchy> DivisionHierarchies { get; set; }
        public virtual DbSet<DivisionLevel> DivisionLevels { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<InventoryItemAssignment> InventoryItemAssignments { get; set; }
        public virtual DbSet<InventoryItemCategory> InventoryItemCategories { get; set; }
        public virtual DbSet<InventoryItemCategoryBrand> InventoryItemCategoryBrands { get; set; }
        public virtual DbSet<InventoryItemCategoryStructure> InventoryItemCategoryStructures { get; set; }
        public virtual DbSet<InventoryItemHistory> InventoryItemHistories { get; set; }
        public virtual DbSet<InventoryItemLevel> InventoryItemLevels { get; set; }
        public virtual DbSet<InventoryItemReservation> InventoryItemReservations { get; set; }
        public virtual DbSet<InventoryItemStatus> InventoryItemStatuses { get; set; }
        public virtual DbSet<InventoryItemStatusInventoryItemType> InventoryItemStatusInventoryItemTypes { get; set; }
        public virtual DbSet<InventoryItemStatusLog> InventoryItemStatusLogs { get; set; }
        public virtual DbSet<InventoryItemType> InventoryItemTypes { get; set; }
        public virtual DbSet<InventoryItemTypeClassification> InventoryItemTypeClassifications { get; set; }
        public virtual DbSet<InventoryItemTypeUnit> InventoryItemTypeUnits { get; set; }
        public virtual DbSet<InventoryLocation> InventoryLocations { get; set; }
        public virtual DbSet<InventoryLocationLevel> InventoryLocationLevels { get; set; }
        public virtual DbSet<InventoryLocationStructure> InventoryLocationStructures { get; set; }
        public virtual DbSet<InventoryLog> InventoryLogs { get; set; }
        public virtual DbSet<InventoryLogOperation> InventoryLogOperations { get; set; }
        public virtual DbSet<InventoryLogTable> InventoryLogTables { get; set; }
        public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public virtual DbSet<InventoryTransactionDetail> InventoryTransactionDetails { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LogAction> LogActions { get; set; }
        public virtual DbSet<LogCategory> LogCategories { get; set; }
        public virtual DbSet<LogDetail> LogDetails { get; set; }
        public virtual DbSet<LogStatus> LogStatuses { get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set; }
        public virtual DbSet<MaintenanceAction> MaintenanceActions { get; set; }
        public virtual DbSet<MaintenanceActionControl> MaintenanceActionControls { get; set; }
        public virtual DbSet<MaintenanceActionDetail> MaintenanceActionDetails { get; set; }
        public virtual DbSet<MaintenanceActionTechnicianPosition> MaintenanceActionTechnicianPositions { get; set; }
        public virtual DbSet<MaintenanceFleet> MaintenanceFleets { get; set; }
        public virtual DbSet<MaintenanceItem> MaintenanceItems { get; set; }
        public virtual DbSet<MaintenanceItemInventoryItem> MaintenanceItemInventoryItems { get; set; }
        public virtual DbSet<MaintenanceItemStatus> MaintenanceItemStatuses { get; set; }
        public virtual DbSet<MaintenanceItemType> MaintenanceItemTypes { get; set; }
        public virtual DbSet<MaintenanceQuestion> MaintenanceQuestions { get; set; }
        public virtual DbSet<MaintenanceQuestionValue> MaintenanceQuestionValues { get; set; }
        public virtual DbSet<MaintenanceType> MaintenanceTypes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterConstraint> ParameterConstraints { get; set; }
        public virtual DbSet<ParameterEntity> ParameterEntities { get; set; }
        public virtual DbSet<ParameterListValue> ParameterListValues { get; set; }
        public virtual DbSet<ParameterType> ParameterTypes { get; set; }
        public virtual DbSet<ParameterTypeConstraintType> ParameterTypeConstraintTypes { get; set; }
        public virtual DbSet<ParameterValue> ParameterValues { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionParameter> PermissionParameters { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }
        public virtual DbSet<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserLanguage> SystemUserLanguages { get; set; }
        public virtual DbSet<SystemUserPermission> SystemUserPermissions { get; set; }
        public virtual DbSet<SystemUserSecurityGroup> SystemUserSecurityGroups { get; set; }
        public virtual DbSet<SystemUserTerminal> SystemUserTerminals { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }
        public virtual DbSet<TechnicianAttendance> TechnicianAttendances { get; set; }
        public virtual DbSet<TechnicianCompany> TechnicianCompanies { get; set; }
        public virtual DbSet<TechnicianPosition> TechnicianPositions { get; set; }
        public virtual DbSet<Terminal> Terminals { get; set; }
        public virtual DbSet<TireCondition> TireConditions { get; set; }
        public virtual DbSet<TireSize> TireSizes { get; set; }
        public virtual DbSet<TireStatus> TireStatuses { get; set; }
        public virtual DbSet<TireTest> TireTests { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleArea> VehicleAreas { get; set; }
        public virtual DbSet<VehicleAttachment> VehicleAttachments { get; set; }
        public virtual DbSet<VehicleBrand> VehicleBrands { get; set; }
        public virtual DbSet<VehicleCurrentTire> VehicleCurrentTires { get; set; }
        public virtual DbSet<VehicleDepartment> VehicleDepartments { get; set; }
        public virtual DbSet<VehicleFamily> VehicleFamilies { get; set; }
        public virtual DbSet<VehicleLicense> VehicleLicenses { get; set; }
        public virtual DbSet<VehicleOwner> VehicleOwners { get; set; }
        public virtual DbSet<VehicleStatus> VehicleStatuses { get; set; }
        public virtual DbSet<VehicleTire> VehicleTires { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseInventoryItemTypeClassification> WarehouseInventoryItemTypeClassifications { get; set; }
        public virtual DbSet<WarehousePermission> WarehousePermissions { get; set; }
        public virtual DbSet<WarehouseStructure> WarehouseStructures { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderWorkshop> WorkOrderWorkshops { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }
        public virtual DbSet<WorkshopMaintenanceType> WorkshopMaintenanceTypes { get; set; }
        public virtual DbSet<WorkshopWarehouse> WorkshopWarehouses { get; set; }
        public virtual DbSet<TechnicianAttendanceLog> TechnicianAttendanceLogs { get; set; }
        public virtual DbSet<TechnicianAttendanceStatusLog> TechnicianAttendanceStatusLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {              
                optionsBuilder.UseNpgsql("Host=test.deqqa.net;Database=wms_dev;Username=wms_user;Password=wms_user");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasAnnotation("Relational:Collation", "Arabic_Saudi Arabia.1256");

      

            modelBuilder.Entity<AttendanceStatus>(entity =>
            {
                entity.ToTable("attendance_status", "core");

                entity.HasIndex(e => e.Name, "attendance_status_name_key")
                    .IsUnique();

                entity.Property(e => e.AttendanceStatusId).HasColumnName("attendance_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<AuthenticationProvider>(entity =>
            {
                entity.ToTable("authentication_provider", "slc");

                entity.HasIndex(e => e.Name, "authentication_provider_name_key")
                    .IsUnique();

                entity.Property(e => e.AuthenticationProviderId).HasColumnName("authentication_provider_id");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasColumnName("is_enabled")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Library)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("library");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<AuthenticationProviderParameter>(entity =>
            {
                entity.HasKey(e => new { e.AuthenticationProviderId, e.Parameter })
                    .HasName("authentication_provider_parameter_pkey");

                entity.ToTable("authentication_provider_parameter", "slc");

                entity.Property(e => e.AuthenticationProviderId).HasColumnName("authentication_provider_id");

                entity.Property(e => e.Parameter)
                    .HasMaxLength(256)
                    .HasColumnName("parameter");

                entity.Property(e => e.Value)
                    .HasMaxLength(512)
                    .HasColumnName("value");

                entity.HasOne(d => d.AuthenticationProvider)
                    .WithMany(p => p.AuthenticationProviderParameters)
                    .HasForeignKey(d => d.AuthenticationProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authentication_provider_paramet_authentication_provider_id_fkey");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand", "inv");

                entity.HasIndex(e => e.Name, "brand_name_key")
                    .IsUnique();

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CapitalType>(entity =>
            {
                entity.ToTable("capital_type", "geo");

                entity.HasIndex(e => e.Name, "capital_type_name_key")
                    .IsUnique();

                entity.Property(e => e.CapitalTypeId).HasColumnName("capital_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CodeType>(entity =>
            {
                entity.ToTable("code_type", "inv");

                entity.HasIndex(e => e.Name, "code_type_name_key")
                    .IsUnique();

                entity.Property(e => e.CodeTypeId).HasColumnName("code_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => new { e.ConfigurationCode, e.ConfigurationCategoryId })
                    .HasName("configuration_pkey");

                entity.ToTable("configuration", "slc");

                entity.Property(e => e.ConfigurationCode)
                    .HasMaxLength(5)
                    .HasColumnName("configuration_code")
                    .IsFixedLength(true);

                entity.Property(e => e.ConfigurationCategoryId).HasColumnName("configuration_category_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.SecurityHash)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("security_hash")
                    .IsFixedLength(true);

                entity.Property(e => e.Value)
                    .HasMaxLength(2048)
                    .HasColumnName("value");

                entity.HasOne(d => d.ConfigurationCategory)
                    .WithMany(p => p.Configurations)
                    .HasForeignKey(d => d.ConfigurationCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("configuration_configuration_category_id_fkey");
            });

            modelBuilder.Entity<ConfigurationCategory>(entity =>
            {
                entity.ToTable("configuration_category", "slc");

                entity.HasIndex(e => e.Name, "configuration_category_name_key")
                    .IsUnique();

                entity.Property(e => e.ConfigurationCategoryId).HasColumnName("configuration_category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentConfigurationCategoryId).HasColumnName("parent_configuration_category_id");

                entity.HasOne(d => d.ParentConfigurationCategory)
                    .WithMany(p => p.InverseParentConfigurationCategory)
                    .HasForeignKey(d => d.ParentConfigurationCategoryId)
                    .HasConstraintName("configuration_category_parent_configuration_category_id_fkey");
            });

            modelBuilder.Entity<ConstraintType>(entity =>
            {
                entity.ToTable("constraint_type", "ece");

                entity.HasIndex(e => e.Name, "constraint_type_name_key")
                    .IsUnique();

                entity.Property(e => e.ConstraintTypeId).HasColumnName("constraint_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("cost_center", "core");

                entity.HasIndex(e => e.Name, "cost_center_name_key")
                    .IsUnique();

                entity.Property(e => e.CostCenterId).HasColumnName("cost_center_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country", "geo");

                entity.HasIndex(e => e.Name, "country_name_key")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(1024)
                    .HasColumnName("image_path");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Dictionary>(entity =>
            {
                entity.HasKey(e => new { e.LanguageId, e.PageId, e.String })
                    .HasName("dictionary_pkey");

                entity.ToTable("dictionary", "dic");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.String)
                    .HasMaxLength(256)
                    .HasColumnName("string");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .HasColumnName("value");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Dictionaries)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dictionary_language_id_fkey");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Dictionaries)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dictionary_page_id_fkey");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("division", "geo");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.CapitalTypeId).HasColumnName("capital_type_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Latitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ParentDivisionId).HasColumnName("parent_division_id");

                entity.HasOne(d => d.CapitalType)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.CapitalTypeId)
                    .HasConstraintName("division_capital_type_id_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("division_country_id_fkey");

                entity.HasOne(d => d.ParentDivision)
                    .WithMany(p => p.InverseParentDivision)
                    .HasForeignKey(d => d.ParentDivisionId)
                    .HasConstraintName("division_parent_division_id_fkey");
            });

            modelBuilder.Entity<DivisionHierarchy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("division_hierarchy", "geo");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.DivisionLevelId).HasColumnName("division_level_id");

                entity.Property(e => e.ParentDivisionId).HasColumnName("parent_division_id");
            });

            modelBuilder.Entity<DivisionLevel>(entity =>
            {
                entity.HasKey(e => new { e.DivisionLevelId, e.CountryId })
                    .HasName("division_level_pkey");

                entity.ToTable("division_level", "geo");

                entity.HasIndex(e => new { e.CountryId, e.Name }, "division_level_country_id_name_key")
                    .IsUnique();

                entity.Property(e => e.DivisionLevelId).HasColumnName("division_level_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("entity", "ece");

                entity.HasIndex(e => e.Name, "entity_name_key")
                    .IsUnique();

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable("inventory_item", "inv");

                entity.HasIndex(e => new { e.SerialNumber, e.LocationId }, "inventory_item_serial_number_location_id_key")
                    .IsUnique();

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(256)
                    .HasColumnName("code");

                entity.Property(e => e.CodeTypeId).HasColumnName("code_type_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.InventoryItemStatusId).HasColumnName("inventory_item_status_id");

                entity.Property(e => e.InventoryItemTypeId).HasColumnName("inventory_item_type_id");

                entity.Property(e => e.InventoryLocationId).HasColumnName("inventory_location_id");

                entity.Property(e => e.IssueNumber)
                    .HasMaxLength(256)
                    .HasColumnName("issue_number");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1024)
                    .HasColumnName("notes");

                entity.Property(e => e.Quantity)
                    .HasPrecision(18, 5)
                    .HasColumnName("quantity");

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("serial_number");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.UnitCost)
                    .HasPrecision(18, 5)
                    .HasColumnName("unit_cost");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.CodeType)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.CodeTypeId)
                    .HasConstraintName("inventory_item_code_type_id_fkey");

                entity.HasOne(d => d.InventoryItemStatus)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.InventoryItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_inventory_item_status_id_fkey");

                entity.HasOne(d => d.InventoryItemType)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.InventoryItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_inventory_item_type_id_fkey");

                entity.HasOne(d => d.InventoryLocation)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.InventoryLocationId)
                    .HasConstraintName("inventory_item_inventory_location_id_fkey");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_location_id_fkey");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_system_user_id_fkey");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_vendor_id_fkey");
            });

            modelBuilder.Entity<InventoryItemAssignment>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemId, e.ObjectId, e.AssignmentDts })
                    .HasName("inventory_item_assignment_pkey");

                entity.ToTable("inventory_item_assignment", "inv");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.AssignmentDts)
                    .HasColumnName("assignment_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Quantity)
                    .HasPrecision(18, 5)
                    .HasColumnName("quantity");

                entity.HasOne(d => d.InventoryItem)
                    .WithMany(p => p.InventoryItemAssignments)
                    .HasForeignKey(d => d.InventoryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_assignment_inventory_item_id_fkey");
            });

            modelBuilder.Entity<InventoryItemCategory>(entity =>
            {
                entity.ToTable("inventory_item_category", "inv");

                entity.HasIndex(e => new { e.Name, e.ParentInventoryItemCategoryId }, "inventory_item_category_name_parent_inventory_item_category_key")
                    .IsUnique();

                entity.Property(e => e.InventoryItemCategoryId).HasColumnName("inventory_item_category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentInventoryItemCategoryId).HasColumnName("parent_inventory_item_category_id");

                entity.HasOne(d => d.ParentInventoryItemCategory)
                    .WithMany(p => p.InverseParentInventoryItemCategory)
                    .HasForeignKey(d => d.ParentInventoryItemCategoryId)
                    .HasConstraintName("inventory_item_category_parent_inventory_item_category_id_fkey");
            });

            modelBuilder.Entity<InventoryItemCategoryBrand>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemCategoryId, e.BrandId })
                    .HasName("inventory_item_category_brand_pkey");

                entity.ToTable("inventory_item_category_brand", "inv");

                entity.Property(e => e.InventoryItemCategoryId).HasColumnName("inventory_item_category_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.InventoryItemCategoryBrands)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_category_brand_brand_id_fkey");

                entity.HasOne(d => d.InventoryItemCategory)
                    .WithMany(p => p.InventoryItemCategoryBrands)
                    .HasForeignKey(d => d.InventoryItemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_category_brand_inventory_item_category_id_fkey");
            });

            modelBuilder.Entity<InventoryItemCategoryStructure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("inventory_item_category_structure", "inv");

                entity.Property(e => e.InventoryItemCategoryId).HasColumnName("inventory_item_category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<InventoryItemHistory>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemId, e.StartDts })
                    .HasName("inventory_item_history_pkey");

                entity.ToTable("inventory_item_history", "core");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.StartDts).HasColumnName("start_dts");

                entity.Property(e => e.EndDts).HasColumnName("end_dts");

                entity.Property(e => e.MaintenanceItemId).HasColumnName("maintenance_item_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.HasOne(d => d.MaintenanceItem)
                    .WithMany(p => p.InventoryItemHistories)
                    .HasForeignKey(d => d.MaintenanceItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_history_maintenance_item_id_fkey");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.InventoryItemHistories)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_history_vehicle_id_fkey");
            });

            modelBuilder.Entity<InventoryItemLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("inventory_item_level", "inv");

                entity.Property(e => e.InventoryItemTypeId).HasColumnName("inventory_item_type_id");

                entity.Property(e => e.InventoryLocationId).HasColumnName("inventory_location_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<InventoryItemReservation>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemId, e.ReservingObjectId, e.ReservingDts })
                    .HasName("inventory_item_reservation_pkey");

                entity.ToTable("inventory_item_reservation", "inv");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.ReservingObjectId).HasColumnName("reserving_object_id");

                entity.Property(e => e.ReservingDts)
                    .HasColumnName("reserving_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ReservedQuantity)
                    .HasPrecision(18, 5)
                    .HasColumnName("reserved_quantity");

                entity.HasOne(d => d.InventoryItem)
                    .WithMany(p => p.InventoryItemReservations)
                    .HasForeignKey(d => d.InventoryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_reservation_inventory_item_id_fkey");
            });

            modelBuilder.Entity<InventoryItemStatus>(entity =>
            {
                entity.ToTable("inventory_item_status", "inv");

                entity.HasIndex(e => e.Name, "inventory_item_status_name_key")
                    .IsUnique();

                entity.Property(e => e.InventoryItemStatusId).HasColumnName("inventory_item_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<InventoryItemStatusInventoryItemType>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemStatusId, e.InventoryItemTypeId })
                    .HasName("inventory_item_status_inventory_item_type_pkey");

                entity.ToTable("inventory_item_status_inventory_item_type", "inv");

                entity.Property(e => e.InventoryItemStatusId).HasColumnName("inventory_item_status_id");

                entity.Property(e => e.InventoryItemTypeId).HasColumnName("inventory_item_type_id");

                entity.HasOne(d => d.InventoryItemStatus)
                    .WithMany(p => p.InventoryItemStatusInventoryItemTypes)
                    .HasForeignKey(d => d.InventoryItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_status_inventory_i_inventory_item_status_id_fkey");

                entity.HasOne(d => d.InventoryItemType)
                    .WithMany(p => p.InventoryItemStatusInventoryItemTypes)
                    .HasForeignKey(d => d.InventoryItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_status_inventory_ite_inventory_item_type_id_fkey");
            });

            modelBuilder.Entity<InventoryItemStatusLog>(entity =>
            {
                entity.HasKey(e => new { e.InventoryItemId, e.StatusDts, e.InventoryItemStatusId })
                    .HasName("inventory_item_status_log_pkey");

                entity.ToTable("inventory_item_status_log", "inv");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.StatusDts).HasColumnName("status_dts");

                entity.Property(e => e.InventoryItemStatusId).HasColumnName("inventory_item_status_id");

                entity.HasOne(d => d.InventoryItem)
                    .WithMany(p => p.InventoryItemStatusLogs)
                    .HasForeignKey(d => d.InventoryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_status_log_inventory_item_id_fkey");

                entity.HasOne(d => d.InventoryItemStatus)
                    .WithMany(p => p.InventoryItemStatusLogs)
                    .HasForeignKey(d => d.InventoryItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_status_log_inventory_item_status_id_fkey");
            });

            modelBuilder.Entity<InventoryItemType>(entity =>
            {
                entity.ToTable("inventory_item_type", "inv");

                entity.Property(e => e.InventoryItemTypeId).HasColumnName("inventory_item_type_id");

                entity.Property(e => e.AutoGenerateSerial).HasColumnName("auto_generate_serial");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CostCenterId).HasColumnName("cost_center_id");

                entity.Property(e => e.CreateDts).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.InventoryItemTypeClassificationId).HasColumnName("inventory_item_type_classification_id");

                entity.Property(e => e.InventoryItemTypeUnitId).HasColumnName("inventory_item_type_unit_id");

                entity.Property(e => e.InventoryitemcategoryId).HasColumnName("inventoryitemcategory_id");

                entity.Property(e => e.IsQuantitative).HasColumnName("is_quantitative");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_brand_id");

                entity.HasOne(d => d.InventoryItemTypeClassification)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.InventoryItemTypeClassificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_item_type_inventory_item_type_classification_id_fkey");

                entity.HasOne(d => d.InventoryItemTypeUnit)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.InventoryItemTypeUnitId)
                    .HasConstraintName("inventory_item_type_inventory_item_type_unit_id_fkey");

                entity.HasOne(d => d.Inventoryitemcategory)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.InventoryitemcategoryId)
                    .HasConstraintName("fk_inventoryitemcategory_id");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("fk_model_id");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.InventoryItemTypes)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_warehouse_id");
            });

            modelBuilder.Entity<InventoryItemTypeClassification>(entity =>
            {
                entity.ToTable("inventory_item_type_classification", "inv");

                entity.HasIndex(e => e.Name, "inventory_item_type_classification_name_key")
                    .IsUnique();

                entity.Property(e => e.InventoryItemTypeClassificationId)
                    .HasColumnName("inventory_item_type_classification_id")
                    .HasDefaultValueSql("nextval('inv.inventory_item_type_classific_inventory_item_type_classific_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<InventoryItemTypeUnit>(entity =>
            {
                entity.ToTable("inventory_item_type_unit", "inv");

                entity.HasIndex(e => e.Name, "inventory_item_type_unit_name_key")
                    .IsUnique();

                entity.Property(e => e.InventoryItemTypeUnitId).HasColumnName("inventory_item_type_unit_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<InventoryLocation>(entity =>
            {
                entity.ToTable("inventory_location", "inv");

                entity.Property(e => e.InventoryLocationId).HasColumnName("inventory_location_id");

                entity.Property(e => e.InventoryLocationLevelId).HasColumnName("inventory_location_level_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.ParentInventoryLocationId).HasColumnName("parent_inventory_location_id");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.HasOne(d => d.InventoryLocationLevel)
                    .WithMany(p => p.InventoryLocations)
                    .HasForeignKey(d => d.InventoryLocationLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_location_inventory_location_level_id_fkey");

                entity.HasOne(d => d.ParentInventoryLocation)
                    .WithMany(p => p.InverseParentInventoryLocation)
                    .HasForeignKey(d => d.ParentInventoryLocationId)
                    .HasConstraintName("inventory_location_parent_inventory_location_id_fkey");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.InventoryLocations)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_location_warehouse_id_fkey");
            });

            modelBuilder.Entity<InventoryLocationLevel>(entity =>
            {
                entity.ToTable("inventory_location_level", "inv");

                entity.Property(e => e.InventoryLocationLevelId).HasColumnName("inventory_location_level_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.ParentInventoryLocationLevelId).HasColumnName("parent_inventory_location_level_id");

                entity.Property(e => e.StructureName)
                    .HasMaxLength(128)
                    .HasColumnName("structure_name");

                entity.HasOne(d => d.ParentInventoryLocationLevel)
                    .WithMany(p => p.InverseParentInventoryLocationLevel)
                    .HasForeignKey(d => d.ParentInventoryLocationLevelId)
                    .HasConstraintName("inventory_location_level_parent_inventory_location_level_i_fkey");
            });

            modelBuilder.Entity<InventoryLocationStructure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("inventory_location_structure", "inv");

                entity.Property(e => e.InventoryLocationId).HasColumnName("inventory_location_id");

                entity.Property(e => e.InventoryLocationLevelId).HasColumnName("inventory_location_level_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
            });

            modelBuilder.Entity<InventoryLog>(entity =>
            {
                entity.ToTable("inventory_log", "inv");

                entity.HasIndex(e => e.InventoryLogOperationID, "IX_inventory_log_InventoryLogOperationID");

                entity.HasIndex(e => e.InventoryLogTableID, "IX_inventory_log_InventoryLogTableID");

                entity.Property(e => e.InventoryLogID).HasColumnName("InventoryLogID");

                entity.Property(e => e.CreateDT).HasColumnName("CreateDT");

                entity.Property(e => e.InventoryLogOperationID).HasColumnName("InventoryLogOperationID");

                entity.Property(e => e.InventoryLogTableID).HasColumnName("InventoryLogTableID");

                entity.Property(e => e.ObjectID).HasColumnName("ObjectID");

                entity.Property(e => e.SystemUserID).HasColumnName("SystemUserID");

                entity.HasOne(d => d.InventoryLogOperation)
                    .WithMany(p => p.InventoryLogs)
                    .HasForeignKey(d => d.InventoryLogOperationID)
                    .HasConstraintName("FK_inventory_log_inventory_log_operation_InventoryLogOperation~");

                entity.HasOne(d => d.InventoryLogTable)
                    .WithMany(p => p.InventoryLogs)
                    .HasForeignKey(d => d.InventoryLogTableID);
            });

            modelBuilder.Entity<InventoryLogOperation>(entity =>
            {
                entity.ToTable("inventory_log_operation", "inv");

                entity.HasIndex(e => e.OperationName, "IX_inventory_log_operation_OperationName")
                    .IsUnique();

                entity.Property(e => e.InventoryLogOperationId).HasColumnName("InventoryLogOperationID");

                entity.Property(e => e.OperationName).IsRequired();
            });

            modelBuilder.Entity<InventoryLogTable>(entity =>
            {
                entity.ToTable("inventory_log_table", "inv");

                entity.HasIndex(e => e.TableName, "IX_inventory_log_table_TableName")
                    .IsUnique();

                entity.Property(e => e.InventoryLogTableId).HasColumnName("InventoryLogTableID");

                entity.Property(e => e.TableName).IsRequired();
            });

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.ToTable("inventory_transaction", "inv");

                entity.Property(e => e.InventoryTransactionId).HasColumnName("inventory_transaction_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.FromLocationId).HasColumnName("from_location_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.ToLocationId).HasColumnName("to_location_id");

                entity.HasOne(d => d.FromLocation)
                    .WithMany(p => p.InventoryTransactionFromLocations)
                    .HasForeignKey(d => d.FromLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_from_location_id_fkey");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_system_user_id_fkey");

                entity.HasOne(d => d.ToLocation)
                    .WithMany(p => p.InventoryTransactionToLocations)
                    .HasForeignKey(d => d.ToLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_to_location_id_fkey");
            });

            modelBuilder.Entity<InventoryTransactionDetail>(entity =>
            {
                entity.HasKey(e => new { e.InventoryTransactionId, e.InventoryItemId })
                    .HasName("inventory_transaction_detail_pkey");

                entity.ToTable("inventory_transaction_detail", "inv");

                entity.Property(e => e.InventoryTransactionId).HasColumnName("inventory_transaction_id");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.InventoryItemStatusId).HasColumnName("inventory_item_status_id");

                entity.Property(e => e.Quantity)
                    .HasPrecision(18, 5)
                    .HasColumnName("quantity");

                entity.HasOne(d => d.InventoryItemStatus)
                    .WithMany(p => p.InventoryTransactionDetails)
                    .HasForeignKey(d => d.InventoryItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_detail_inventory_item_status_id_fkey");

                entity.HasOne(d => d.InventoryTransaction)
                    .WithMany(p => p.InventoryTransactionDetails)
                    .HasForeignKey(d => d.InventoryTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_detail_inventory_transaction_id_fkey");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("language", "dic");

                entity.HasIndex(e => e.Name, "language_name_key")
                    .IsUnique();

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(1024)
                    .HasColumnName("image_path");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Rtl).HasColumnName("rtl");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "inv");

                entity.HasIndex(e => new { e.LocationObjectId, e.LocationTypeId }, "location_location_object_id_location_type_id_key")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.LocationObjectId).HasColumnName("location_object_id");

                entity.Property(e => e.LocationTypeId).HasColumnName("location_type_id");

                entity.HasOne(d => d.LocationType)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_location_type_id_fkey");
            });

            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.ToTable("location_type", "inv");

                entity.HasIndex(e => e.Name, "location_type_name_key")
                    .IsUnique();

                entity.Property(e => e.LocationTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("location_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log", "slc");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.LogActionId).HasColumnName("log_action_id");

                entity.Property(e => e.LogCategoryId).HasColumnName("log_category_id");

                entity.Property(e => e.LogDts)
                    .HasColumnName("log_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.LogStatusId).HasColumnName("log_status_id");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.HasOne(d => d.LogAction)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.LogActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_log_action_id_fkey");

                entity.HasOne(d => d.LogCategory)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.LogCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_log_category_id_fkey");

                entity.HasOne(d => d.LogStatus)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.LogStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_log_status_id_fkey");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_system_user_id_fkey");
            });

            modelBuilder.Entity<LogAction>(entity =>
            {
                entity.ToTable("log_action", "slc");

                entity.HasIndex(e => e.Name, "log_action_name_key")
                    .IsUnique();

                entity.Property(e => e.LogActionId).HasColumnName("log_action_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<LogCategory>(entity =>
            {
                entity.ToTable("log_category", "slc");

                entity.HasIndex(e => e.Name, "log_category_name_key")
                    .IsUnique();

                entity.Property(e => e.LogCategoryId).HasColumnName("log_category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentLogCategoryId).HasColumnName("parent_log_category_id");

                entity.HasOne(d => d.ParentLogCategory)
                    .WithMany(p => p.InverseParentLogCategory)
                    .HasForeignKey(d => d.ParentLogCategoryId)
                    .HasConstraintName("log_category_parent_log_category_id_fkey");
            });

            modelBuilder.Entity<LogDetail>(entity =>
            {
                entity.HasKey(e => new { e.LogId, e.Parameter })
                    .HasName("log_detail_pkey");

                entity.ToTable("log_detail", "slc");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Parameter)
                    .HasMaxLength(64)
                    .HasColumnName("parameter");

                entity.Property(e => e.Value)
                    .HasMaxLength(2048)
                    .HasColumnName("value");

                entity.HasOne(d => d.Log)
                    .WithMany(p => p.LogDetails)
                    .HasForeignKey(d => d.LogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_detail_log_id_fkey");
            });

            modelBuilder.Entity<LogStatus>(entity =>
            {
                entity.ToTable("log_status", "slc");

                entity.HasIndex(e => e.Name, "log_status_name_key")
                    .IsUnique();

                entity.Property(e => e.LogStatusId).HasColumnName("log_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.ToTable("maintenance", "core");

                entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenance_type_id");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1024)
                    .HasColumnName("notes");

                entity.Property(e => e.WorkOrderNumber).HasColumnName("work_order_number");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.HasOne(d => d.MaintenanceType)
                    .WithMany(p => p.Maintenances)
                    .HasForeignKey(d => d.MaintenanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_maintenance_type_id_fkey");

                entity.HasOne(d => d.WorkOrderNumberNavigation)
                    .WithMany(p => p.Maintenances)
                    .HasForeignKey(d => d.WorkOrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_work_order_number_fkey");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.Maintenances)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_workshop_id_fkey");
            });

            modelBuilder.Entity<MaintenanceAction>(entity =>
            {
                entity.ToTable("maintenance_action", "core");

                entity.HasIndex(e => e.Name, "maintenance_action_name_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceActionId).HasColumnName("maintenance_action_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.MaintenanceActions)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_workshop_id_fkey");
            });

            modelBuilder.Entity<MaintenanceActionControl>(entity =>
            {
                entity.HasKey(e => new { e.MaintenanceActionId, e.VehicleFamilyId, e.Scheduled })
                    .HasName("maintenance_action_control_pkey");

                entity.ToTable("maintenance_action_control", "core");

                entity.Property(e => e.MaintenanceActionId).HasColumnName("maintenance_action_id");

                entity.Property(e => e.VehicleFamilyId).HasColumnName("vehicle_family_id");

                entity.Property(e => e.Scheduled).HasColumnName("scheduled");

                entity.HasOne(d => d.MaintenanceAction)
                    .WithMany(p => p.MaintenanceActionControls)
                    .HasForeignKey(d => d.MaintenanceActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_control_maintenance_action_id_fkey");

                entity.HasOne(d => d.VehicleFamily)
                    .WithMany(p => p.MaintenanceActionControls)
                    .HasForeignKey(d => d.VehicleFamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_control_vehicle_family_id_fkey");
            });

            modelBuilder.Entity<MaintenanceActionDetail>(entity =>
            {
                entity.ToTable("maintenance_action_detail", "core");

                entity.HasIndex(e => new { e.MaintenanceActionId, e.Name }, "maintenance_action_detail_maintenance_action_id_name_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceActionDetailId).HasColumnName("maintenance_action_detail_id");

                entity.Property(e => e.MaintenanceActionId).HasColumnName("maintenance_action_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.HasOne(d => d.MaintenanceAction)
                    .WithMany(p => p.MaintenanceActionDetails)
                    .HasForeignKey(d => d.MaintenanceActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_detail_maintenance_action_id_fkey");
            });

            modelBuilder.Entity<MaintenanceActionTechnicianPosition>(entity =>
            {
                entity.HasKey(e => new { e.MaintenanceActionId, e.TechnicianPositionId })
                    .HasName("maintenance_action_technician_position_pkey");

                entity.ToTable("maintenance_action_technician_position", "core");

                entity.Property(e => e.MaintenanceActionId).HasColumnName("maintenance_action_id");

                entity.Property(e => e.TechnicianPositionId).HasColumnName("technician_position_id");

                entity.HasOne(d => d.MaintenanceAction)
                    .WithMany(p => p.MaintenanceActionTechnicianPositions)
                    .HasForeignKey(d => d.MaintenanceActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_technician_positi_maintenance_action_id_fkey");

                entity.HasOne(d => d.TechnicianPosition)
                    .WithMany(p => p.MaintenanceActionTechnicianPositions)
                    .HasForeignKey(d => d.TechnicianPositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_action_technician_posit_technician_position_id_fkey");
            });

            modelBuilder.Entity<MaintenanceFleet>(entity =>
            {
                entity.ToTable("maintenance_fleet", "core");

                entity.HasIndex(e => e.Name, "maintenance_fleet_name_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceFleetId).HasColumnName("maintenance_fleet_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<MaintenanceItem>(entity =>
            {
                entity.ToTable("maintenance_item", "core");

                entity.Property(e => e.MaintenanceItemId).HasColumnName("maintenance_item_id");

                entity.Property(e => e.Checked).HasColumnName("checked");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .HasColumnName("comments");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.Failure)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .HasColumnName("failure");

                entity.Property(e => e.MaintenanceActionDetailId).HasColumnName("maintenance_action_detail_id");

                entity.Property(e => e.MaintenanceActionId).HasColumnName("maintenance_action_id");

                entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");

                entity.Property(e => e.MaintenanceItemStatusId).HasColumnName("maintenance_item_status_id");

                entity.Property(e => e.MaintenanceItemTypeId).HasColumnName("maintenance_item_type_id");

                entity.Property(e => e.RelatedMaintenanceItemId).HasColumnName("related_maintenance_item_id");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.Task)
                    .HasColumnType("character varying")
                    .HasColumnName("task");

                entity.Property(e => e.TechnicianId).HasColumnName("technician_id");

                entity.HasOne(d => d.MaintenanceActionDetail)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.MaintenanceActionDetailId)
                    .HasConstraintName("maintenance_item_maintenance_action_detail_id_fkey");

                entity.HasOne(d => d.MaintenanceAction)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.MaintenanceActionId)
                    .HasConstraintName("maintenance_item_maintenance_action_id_fkey");

                entity.HasOne(d => d.Maintenance)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.MaintenanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_maintenance_id_fkey");

                entity.HasOne(d => d.MaintenanceItemStatus)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.MaintenanceItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_maintenance_item_status_id_fkey");

                entity.HasOne(d => d.MaintenanceItemType)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.MaintenanceItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_maintenance_item_type_id_fkey");

                entity.HasOne(d => d.RelatedMaintenanceItem)
                    .WithMany(p => p.InverseRelatedMaintenanceItem)
                    .HasForeignKey(d => d.RelatedMaintenanceItemId)
                    .HasConstraintName("maintenance_item_related_maintenance_item_id_fkey");

                entity.HasOne(d => d.Technician)
                    .WithMany(p => p.MaintenanceItems)
                    .HasForeignKey(d => d.TechnicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_technician_id_fkey");
            });

            modelBuilder.Entity<MaintenanceItemInventoryItem>(entity =>
            {
                entity.HasKey(e => new { e.MaintenanceItemId, e.InventoryItemId })
                    .HasName("maintenance_item_inventory_item_pkey");

                entity.ToTable("maintenance_item_inventory_item", "core");

                entity.Property(e => e.MaintenanceItemId).HasColumnName("maintenance_item_id");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.Quantity)
                    .HasPrecision(18, 5)
                    .HasColumnName("quantity");

                entity.HasOne(d => d.MaintenanceItem)
                    .WithMany(p => p.MaintenanceItemInventoryItems)
                    .HasForeignKey(d => d.MaintenanceItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_inventory_item_maintenance_item_id_fkey");
            });

            modelBuilder.Entity<MaintenanceItemStatus>(entity =>
            {
                entity.ToTable("maintenance_item_status", "core");

                entity.HasIndex(e => new { e.Name, e.WorkshopId }, "maintenance_item_status_name_workshop_id_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceItemStatusId).HasColumnName("maintenance_item_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.MaintenanceItemStatuses)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_item_status_workshop_id_fkey");
            });

            modelBuilder.Entity<MaintenanceItemType>(entity =>
            {
                entity.ToTable("maintenance_item_type", "core");

                entity.HasIndex(e => e.Name, "maintenance_item_type_name_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceItemTypeId).HasColumnName("maintenance_item_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<MaintenanceQuestion>(entity =>
            {
                entity.ToTable("maintenance_question", "core");

                entity.Property(e => e.MaintenanceQuestionId).HasColumnName("maintenance_question_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.MaintenanceQuestions)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_question_workshop_id_fkey");
            });

            modelBuilder.Entity<MaintenanceQuestionValue>(entity =>
            {
                entity.HasKey(e => new { e.MaintenanceQuestionId, e.MaintenanceId })
                    .HasName("maintenance_question_value_pkey");

                entity.ToTable("maintenance_question_value", "core");

                entity.Property(e => e.MaintenanceQuestionId).HasColumnName("maintenance_question_id");

                entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(1024)
                    .HasColumnName("value");

                entity.HasOne(d => d.Maintenance)
                    .WithMany(p => p.MaintenanceQuestionValues)
                    .HasForeignKey(d => d.MaintenanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_question_value_maintenance_id_fkey");

                entity.HasOne(d => d.MaintenanceQuestion)
                    .WithMany(p => p.MaintenanceQuestionValues)
                    .HasForeignKey(d => d.MaintenanceQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_question_value_maintenance_question_id_fkey");
            });

            modelBuilder.Entity<MaintenanceType>(entity =>
            {
                entity.ToTable("maintenance_type", "core");

                entity.HasIndex(e => e.Name, "maintenance_type_name_key")
                    .IsUnique();

                entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenance_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.ToTable("model", "inv");

                entity.HasIndex(e => new { e.Name, e.BrandId }, "model_name_brand_id_key")
                    .IsUnique();

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("model_brand_id_fkey");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("page", "dic");

                entity.HasIndex(e => e.Name, "page_name_key")
                    .IsUnique();

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.ToTable("parameter", "ece");

                entity.HasIndex(e => e.Name, "parameter_name_key")
                    .IsUnique();

                entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("display_name");

                entity.Property(e => e.IsOptional).HasColumnName("is_optional");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParameterTypeId).HasColumnName("parameter_type_id");

                entity.HasOne(d => d.ParameterType)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.ParameterTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_parameter_type_id_fkey");
            });

            modelBuilder.Entity<ParameterConstraint>(entity =>
            {
                entity.HasKey(e => new { e.ParameterId, e.ConstraintTypeId })
                    .HasName("parameter_constraint_pkey");

                entity.ToTable("parameter_constraint", "ece");

                entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

                entity.Property(e => e.ConstraintTypeId).HasColumnName("constraint_type_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(512)
                    .HasColumnName("value");

                entity.HasOne(d => d.ConstraintType)
                    .WithMany(p => p.ParameterConstraints)
                    .HasForeignKey(d => d.ConstraintTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_constraint_constraint_type_id_fkey");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.ParameterConstraints)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_constraint_parameter_id_fkey");
            });

            modelBuilder.Entity<ParameterEntity>(entity =>
            {
                entity.HasKey(e => new { e.ParameterId, e.EntityId })
                    .HasName("parameter_entity_pkey");

                entity.ToTable("parameter_entity", "ece");

                entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.ParameterEntities)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_entity_entity_id_fkey");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.ParameterEntities)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_entity_parameter_id_fkey");
            });

            modelBuilder.Entity<ParameterListValue>(entity =>
            {
                entity.ToTable("parameter_list_value", "ece");

                entity.HasIndex(e => new { e.ParameterId, e.Value }, "parameter_list_value_parameter_id_value_key")
                    .IsUnique();

                entity.Property(e => e.ParameterListValueId).HasColumnName("parameter_list_value_id");

                entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("value");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.ParameterListValues)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_list_value_parameter_id_fkey");
            });

            modelBuilder.Entity<ParameterType>(entity =>
            {
                entity.ToTable("parameter_type", "ece");

                entity.HasIndex(e => e.Name, "parameter_type_name_key")
                    .IsUnique();

                entity.Property(e => e.ParameterTypeId).HasColumnName("parameter_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ParameterTypeConstraintType>(entity =>
            {
                entity.HasKey(e => new { e.ParameterTypeId, e.ConstraintTypeId })
                    .HasName("parameter_type_constraint_type_pkey");

                entity.ToTable("parameter_type_constraint_type", "ece");

                entity.Property(e => e.ParameterTypeId).HasColumnName("parameter_type_id");

                entity.Property(e => e.ConstraintTypeId).HasColumnName("constraint_type_id");

                entity.HasOne(d => d.ConstraintType)
                    .WithMany(p => p.ParameterTypeConstraintTypes)
                    .HasForeignKey(d => d.ConstraintTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_type_constraint_type_constraint_type_id_fkey");

                entity.HasOne(d => d.ParameterType)
                    .WithMany(p => p.ParameterTypeConstraintTypes)
                    .HasForeignKey(d => d.ParameterTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_type_constraint_type_parameter_type_id_fkey");
            });

            modelBuilder.Entity<ParameterValue>(entity =>
            {
                entity.ToTable("parameter_value", "ece");

                entity.Property(e => e.ParameterValueId).HasColumnName("parameter_value_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ParameterId).HasColumnName("parameter_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(4000)
                    .HasColumnName("value");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.ParameterValues)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parameter_value_parameter_id_fkey");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission", "slc");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PermissionParameter>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.Parameter })
                    .HasName("permission_parameter_pkey");

                entity.ToTable("permission_parameter", "slc");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.Parameter)
                    .HasMaxLength(64)
                    .HasColumnName("parameter");

                entity.Property(e => e.Value)
                    .HasMaxLength(256)
                    .HasColumnName("value");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionParameters)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("permission_parameter_permission_id_fkey");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule", "core");

                entity.HasIndex(e => new { e.VehicleId, e.VisitDts }, "schedule_vehicle_id_visit_dts_key")
                    .IsUnique();

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CreateSystemUserId).HasColumnName("create_system_user_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.VisitDts)
                    .HasColumnType("date")
                    .HasColumnName("visit_dts");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_schedule_vehicls");
            });

            modelBuilder.Entity<SecurityGroup>(entity =>
            {
                entity.ToTable("security_group", "slc");

                entity.HasIndex(e => e.Name, "security_group_name_key")
                    .IsUnique();

                entity.Property(e => e.SecurityGroupId).HasColumnName("security_group_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.IsEnabled).HasColumnName("is_enabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SecurityGroupPermission>(entity =>
            {
                entity.HasKey(e => new { e.SecurityGroupId, e.PermissionId })
                    .HasName("security_group_permission_pkey");

                entity.ToTable("security_group_permission", "slc");

                entity.Property(e => e.SecurityGroupId).HasColumnName("security_group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.SecurityGroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("security_group_permission_permission_id_fkey");

                entity.HasOne(d => d.SecurityGroup)
                    .WithMany(p => p.SecurityGroupPermissions)
                    .HasForeignKey(d => d.SecurityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("security_group_permission_security_group_id_fkey");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("shift", "core");

                entity.HasIndex(e => e.Name, "shift_name_key")
                    .IsUnique();

                entity.Property(e => e.ShiftId).HasColumnName("shift_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("system_user", "slc");

                entity.HasIndex(e => new { e.Username, e.AuthenticationProviderId }, "system_user_username_authentication_provider_id_key")
                    .IsUnique();

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.AuthenticationProviderId).HasColumnName("authentication_provider_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("email");

                entity.Property(e => e.FailedAttemptsCount).HasColumnName("failed_attempts_count");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsEnabled).HasColumnName("is_enabled");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("last_name");

                entity.Property(e => e.LockDts).HasColumnName("lock_dts");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordChanged).HasColumnName("password_changed");

                entity.Property(e => e.RestrictTerminals).HasColumnName("restrict_terminals");

                entity.Property(e => e.UserCategoryId).HasColumnName("user_category_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("username");

                entity.HasOne(d => d.AuthenticationProvider)
                    .WithMany(p => p.SystemUsers)
                    .HasForeignKey(d => d.AuthenticationProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_authentication_provider_id_fkey");

                entity.HasOne(d => d.UserCategory)
                    .WithMany(p => p.SystemUsers)
                    .HasForeignKey(d => d.UserCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_user_category_id_fkey");
            });

            modelBuilder.Entity<SystemUserLanguage>(entity =>
            {
                entity.HasKey(e => e.SystemUserId)
                    .HasName("system_user_language_pkey");

                entity.ToTable("system_user_language", "dic");

                entity.Property(e => e.SystemUserId)
                    .ValueGeneratedNever()
                    .HasColumnName("system_user_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.SystemUserLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_language_language_id_fkey");
            });

            modelBuilder.Entity<SystemUserPermission>(entity =>
            {
                entity.HasKey(e => new { e.SystemUserId, e.PermissionId })
                    .HasName("system_user_permission_pkey");

                entity.ToTable("system_user_permission", "slc");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.IsGranted).HasColumnName("is_granted");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.SystemUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_permission_permission_id_fkey");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserPermissions)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_permission_system_user_id_fkey");
            });

            modelBuilder.Entity<SystemUserSecurityGroup>(entity =>
            {
                entity.HasKey(e => new { e.SystemUserId, e.SecurityGroupId })
                    .HasName("system_user_security_group_pkey");

                entity.ToTable("system_user_security_group", "slc");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.SecurityGroupId).HasColumnName("security_group_id");

                entity.HasOne(d => d.SecurityGroup)
                    .WithMany(p => p.SystemUserSecurityGroups)
                    .HasForeignKey(d => d.SecurityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_security_group_security_group_id_fkey");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserSecurityGroups)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_security_group_system_user_id_fkey");
            });

            modelBuilder.Entity<SystemUserTerminal>(entity =>
            {
                entity.HasKey(e => new { e.SystemUserId, e.TerminalId })
                    .HasName("system_user_terminal_pkey");

                entity.ToTable("system_user_terminal", "slc");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.TerminalId).HasColumnName("terminal_id");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserTerminals)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_terminal_system_user_id_fkey");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.SystemUserTerminals)
                    .HasForeignKey(d => d.TerminalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("system_user_terminal_terminal_id_fkey");
            });

            modelBuilder.Entity<Technician>(entity =>
            {
                entity.ToTable("technician", "core");

                entity.Property(e => e.TechnicianId).HasColumnName("technician_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Contact1)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("contact1");

                entity.Property(e => e.Contact2)
                    .HasMaxLength(32)
                    .HasColumnName("contact2");

                entity.Property(e => e.CostCenterId).HasColumnName("cost_center_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("national_id")
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Systemusercrate)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("systemusercrate");

                entity.Property(e => e.TechnicianCompanyEmployeeId)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("technician_company_employee_id");

                entity.Property(e => e.TechnicianCompanyId).HasColumnName("technician_company_id");

                entity.Property(e => e.TechnicianPositionId).HasColumnName("technician_position_id");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Technicians)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_cost_center_id_fkey");

                entity.HasOne(d => d.TechnicianCompany)
                    .WithMany(p => p.Technicians)
                    .HasForeignKey(d => d.TechnicianCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_technician_company_id_fkey");

                entity.HasOne(d => d.TechnicianPosition)
                    .WithMany(p => p.Technicians)
                    .HasForeignKey(d => d.TechnicianPositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_technician_position_id_fkey");
            });

            modelBuilder.Entity<TechnicianAttendance>(entity =>
            {
                entity.HasKey(e => new { e.TechnicianId, e.EventDate })
                    .HasName("technician_attendance_pkey");

                entity.ToTable("technician_attendance", "core");

                entity.Property(e => e.TechnicianId).HasColumnName("technician_id");

                entity.Property(e => e.EventDate)
                    .HasColumnType("date")
                    .HasColumnName("event_date");

                entity.Property(e => e.AttendanceStatusId).HasColumnName("attendance_status_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.InTime)
                    .HasColumnType("time without time zone")
                    .HasColumnName("in_time");

                entity.Property(e => e.OutTime)
                    .HasColumnType("time without time zone")
                    .HasColumnName("out_time");

                entity.Property(e => e.ShiftId).HasColumnName("shift_id");

                entity.HasOne(d => d.AttendanceStatus)
                    .WithMany(p => p.TechnicianAttendances)
                    .HasForeignKey(d => d.AttendanceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_attendance_attendance_status_id_fkey");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TechnicianAttendances)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_attendance_shift_id_fkey");

                entity.HasOne(d => d.Technician)
                    .WithMany(p => p.TechnicianAttendances)
                    .HasForeignKey(d => d.TechnicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("technician_attendance_technician_id_fkey");
            });

            modelBuilder.Entity<TechnicianCompany>(entity =>
            {
                entity.ToTable("technician_company", "core");

                entity.HasIndex(e => e.Name, "technician_company_name_key")
                    .IsUnique();

                entity.Property(e => e.TechnicianCompanyId).HasColumnName("technician_company_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TechnicianPosition>(entity =>
            {
                entity.ToTable("technician_position", "core");

                entity.HasIndex(e => e.Name, "technician_position_name_key")
                    .IsUnique();

                entity.Property(e => e.TechnicianPositionId).HasColumnName("technician_position_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.ToTable("terminal", "slc");

                entity.Property(e => e.TerminalId).HasColumnName("terminal_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Hostname)
                    .HasMaxLength(256)
                    .HasColumnName("hostname");

                entity.Property(e => e.Ip)
                    .HasMaxLength(15)
                    .HasColumnName("ip");

                entity.Property(e => e.IsEnabled).HasColumnName("is_enabled");
            });

            modelBuilder.Entity<TireCondition>(entity =>
            {
                entity.ToTable("tire_condition", "core");

                entity.HasIndex(e => e.Name, "tire_condition_name_key")
                    .IsUnique();

                entity.Property(e => e.TireConditionId).HasColumnName("tire_condition_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TireSize>(entity =>
            {
                entity.ToTable("tire_size", "core");

                entity.HasIndex(e => e.Name, "tire_size_name_key")
                    .IsUnique();

                entity.Property(e => e.TireSizeId).HasColumnName("tire_size_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TireStatus>(entity =>
            {
                entity.ToTable("tire_status", "core");

                entity.HasIndex(e => e.Name, "tire_status_name_key")
                    .IsUnique();

                entity.Property(e => e.TireStatusId).HasColumnName("tire_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TireTest>(entity =>
            {
                entity.ToTable("tire_test", "core");

                entity.Property(e => e.TireTestId).HasColumnName("tire_test_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.MaintenanceItemId).HasColumnName("maintenance_item_id");

                entity.Property(e => e.Psi)
                    .HasPrecision(18, 5)
                    .HasColumnName("psi");

                entity.Property(e => e.TireConditionId).HasColumnName("tire_condition_id");

                entity.Property(e => e.TireTreadDepthA)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_a");

                entity.Property(e => e.TireTreadDepthB)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_b");

                entity.Property(e => e.TireTreadDepthC)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_c");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.MaintenanceItem)
                    .WithMany(p => p.TireTests)
                    .HasForeignKey(d => d.MaintenanceItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tire_test_maintenance_item_id_fkey");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TireTests)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tire_test_vehicle_id_fkey");
            });

            modelBuilder.Entity<UserCategory>(entity =>
            {
                entity.ToTable("user_category", "slc");

                entity.HasIndex(e => e.Name, "user_category_name_key")
                    .IsUnique();

                entity.Property(e => e.UserCategoryId).HasColumnName("user_category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentUserCategoryId).HasColumnName("parent_user_category_id");

                entity.HasOne(d => d.ParentUserCategory)
                    .WithMany(p => p.InverseParentUserCategory)
                    .HasForeignKey(d => d.ParentUserCategoryId)
                    .HasConstraintName("user_category_parent_user_category_id_fkey");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicle", "core");

                entity.HasIndex(e => e.AttachedVehicleId, "vehicle_attached_vehicle_id_key")
                    .IsUnique();

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.AttachedVehicleId).HasColumnName("attached_vehicle_id");

                entity.Property(e => e.Capacity)
                    //.IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("capacity");

                entity.Property(e => e.ChassisSerial)
                    //.IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("chassis_serial");

                entity.Property(e => e.ChassisType)
                    //.IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("chassis_type");

                entity.Property(e => e.CostCenterId).HasColumnName("cost_center_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EngineSerial)
                    //.IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("engine_serial");

                entity.Property(e => e.EngineType)
                    //.IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("engine_type");

                entity.Property(e => e.ManufacturingYear).HasColumnName("manufacturing_year");

                entity.Property(e => e.Notes)
                    .HasMaxLength(512)
                    .HasColumnName("notes");

                entity.Property(e => e.TireSizeId).HasColumnName("tire_size_id");

                entity.Property(e => e.TiresCount).HasColumnName("tires_count");

                entity.Property(e => e.VehicleBrandId).HasColumnName("vehicle_brand_id");

                entity.Property(e => e.VehicleDepartmentId).HasColumnName("vehicle_department_id");

                entity.Property(e => e.VehicleFamilyId).HasColumnName("vehicle_family_id");

                entity.Property(e => e.VehicleOwnerId).HasColumnName("vehicle_owner_id");

                entity.Property(e => e.VehicleStatusId).HasColumnName("vehicle_status_id");

                entity.HasOne(d => d.AttachedVehicle)
                    .WithOne(p => p.InverseAttachedVehicle)
                    .HasForeignKey<Vehicle>(d => d.AttachedVehicleId)
                    .HasConstraintName("vehicle_attached_vehicle_id_fkey");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_cost_center_id_fkey");

                entity.HasOne(d => d.TireSize)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.TireSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_tire_size_id_fkey");

                entity.HasOne(d => d.VehicleBrand)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicle_brand_id_fkey");

                entity.HasOne(d => d.VehicleDepartment)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicle_department_id_fkey");

                entity.HasOne(d => d.VehicleFamily)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleFamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicle_family_id_fkey");

                entity.HasOne(d => d.VehicleOwner)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicle_owner_id_fkey");

                entity.HasOne(d => d.VehicleStatus)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicle_status_id_fkey");
            });

            modelBuilder.Entity<VehicleArea>(entity =>
            {
                entity.ToTable("vehicle_area", "core");

                entity.HasIndex(e => e.Name, "vehicle_area_name_key")
                    .IsUnique();

                entity.Property(e => e.VehicleAreaId).HasColumnName("vehicle_area_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<VehicleAttachment>(entity =>
            {
                entity.ToTable("vehicle_attachment", "core");

                entity.Property(e => e.VehicleAttachmentId).HasColumnName("vehicle_attachment_id");

                entity.Property(e => e.AttachedVehicleId).HasColumnName("attached_vehicle_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.AttachedVehicle)
                    .WithMany(p => p.VehicleAttachmentAttachedVehicles)
                    .HasForeignKey(d => d.AttachedVehicleId)
                    .HasConstraintName("vehicle_attachment_attached_vehicle_id_fkey");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAttachmentVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_attachment_vehicle_id_fkey");
            });

            modelBuilder.Entity<VehicleBrand>(entity =>
            {
                entity.ToTable("vehicle_brand", "core");

                entity.HasIndex(e => new { e.Name, e.VehicleFamilyId }, "vehicle_brand_name_vehicle_family_id_key")
                    .IsUnique();

                entity.Property(e => e.VehicleBrandId).HasColumnName("vehicle_brand_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.VehicleFamilyId).HasColumnName("vehicle_family_id");

                entity.HasOne(d => d.VehicleFamily)
                    .WithMany(p => p.VehicleBrands)
                    .HasForeignKey(d => d.VehicleFamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_brand_vehicle_family_id_fkey");
            });

            modelBuilder.Entity<VehicleCurrentTire>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vehicle_current_tire", "core");

                entity.Property(e => e.Brand)
                    .HasMaxLength(128)
                    .HasColumnName("brand");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.InventoryItemStatus)
                    .HasMaxLength(128)
                    .HasColumnName("inventory_item_status");

                entity.Property(e => e.Pattern)
                    .HasMaxLength(256)
                    .HasColumnName("pattern");

                entity.Property(e => e.Pressure)
                    .HasPrecision(18, 5)
                    .HasColumnName("pressure");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(64)
                    .HasColumnName("serial_number");

                entity.Property(e => e.StandardTreadDepth)
                    .HasMaxLength(4000)
                    .HasColumnName("standard_tread_depth");

                entity.Property(e => e.StartDts).HasColumnName("start_dts");

                entity.Property(e => e.TirePosition).HasColumnName("tire_position");

                entity.Property(e => e.TireTreadDepthA)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_a");

                entity.Property(e => e.TireTreadDepthB)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_b");

                entity.Property(e => e.TireTreadDepthC)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_c");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            });

            modelBuilder.Entity<VehicleDepartment>(entity =>
            {
                entity.ToTable("vehicle_department", "core");

                entity.Property(e => e.VehicleDepartmentId)
                    .HasColumnName("vehicle_department_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(265)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<VehicleFamily>(entity =>
            {
                entity.ToTable("vehicle_family", "core");

                entity.HasIndex(e => new { e.Name, e.ParentVehicleFamilyId }, "vehicle_family_name_parent_vehicle_family_id_key")
                    .IsUnique();

                entity.Property(e => e.VehicleFamilyId).HasColumnName("vehicle_family_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentVehicleFamilyId).HasColumnName("parent_vehicle_family_id");

                entity.HasOne(d => d.ParentVehicleFamily)
                    .WithMany(p => p.InverseParentVehicleFamily)
                    .HasForeignKey(d => d.ParentVehicleFamilyId)
                    .HasConstraintName("vehicle_family_parent_vehicle_family_id_fkey");
            });

            modelBuilder.Entity<VehicleLicense>(entity =>
            {
                entity.HasKey(e => new { e.VehicleId, e.StartDate })
                    .HasName("vehicle_license_pkey");

                entity.ToTable("vehicle_license", "core");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.LicenseNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("license_number");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleLicenses)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_license_vehicle_id_fkey");
            });

            modelBuilder.Entity<VehicleOwner>(entity =>
            {
                entity.ToTable("vehicle_owner", "core");

                entity.Property(e => e.VehicleOwnerId)
                    .HasColumnName("vehicle_owner_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.VehicleDepartmentId).HasColumnName("vehicle_department_id");

                entity.HasOne(d => d.VehicleDepartment)
                    .WithMany(p => p.VehicleOwners)
                    .HasForeignKey(d => d.VehicleDepartmentId)
                    .HasConstraintName("fk_vehicle_department_owner");
            });

            modelBuilder.Entity<VehicleStatus>(entity =>
            {
                entity.ToTable("vehicle_status", "core");

                entity.HasIndex(e => e.Name, "vehicle_status_name_key")
                    .IsUnique();

                entity.Property(e => e.VehicleStatusId).HasColumnName("vehicle_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<VehicleTire>(entity =>
            {
                entity.HasKey(e => new { e.VehicleId, e.TirePosition, e.StartDts })
                    .HasName("vehicle_tire_pkey");

                entity.ToTable("vehicle_tire", "core");

                entity.HasIndex(e => e.InventoryItemId, "ix_vehicle_tire_1")
                    .IsUnique()
                    .HasFilter("(end_dts IS NULL)");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.TirePosition).HasColumnName("tire_position");

                entity.Property(e => e.StartDts).HasColumnName("start_dts");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EndDts).HasColumnName("end_dts");

                entity.Property(e => e.InventoryItemId).HasColumnName("inventory_item_id");

                entity.Property(e => e.MaintenanceItemId).HasColumnName("maintenance_item_id");

                entity.Property(e => e.Pressure)
                    .HasPrecision(18, 5)
                    .HasColumnName("pressure");

                entity.Property(e => e.TireTreadDepthA)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_a");

                entity.Property(e => e.TireTreadDepthB)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_b");

                entity.Property(e => e.TireTreadDepthC)
                    .HasPrecision(18, 5)
                    .HasColumnName("tire_tread_depth_c");

                entity.HasOne(d => d.MaintenanceItem)
                    .WithMany(p => p.VehicleTires)
                    .HasForeignKey(d => d.MaintenanceItemId)
                    .HasConstraintName("vehicle_tire_maintenance_item_id_fkey");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor", "inv");

                entity.HasIndex(e => e.Name, "vendor_name_key")
                    .IsUnique();

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("warehouse", "inv");

                entity.HasIndex(e => e.Name, "warehouse_name_key")
                    .IsUnique();

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.CanBroadcast).HasColumnName("can_broadcast");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("description");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.InventoryLocationLevelId).HasColumnName("inventory_location_level_id");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasColumnName("is_enabled")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("warehouse_division_id_fkey");

                entity.HasOne(d => d.InventoryLocationLevel)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.InventoryLocationLevelId)
                    .HasConstraintName("warehouse_inventory_location_level_id_fkey");
            });

            modelBuilder.Entity<WarehouseInventoryItemTypeClassification>(entity =>
            {
                entity.HasKey(e => new { e.WarehouseId, e.InventoryItemTypeClassificationId })
                    .HasName("warehouse_inventory_item_type_classification_pkey");

                entity.ToTable("warehouse_inventory_item_type_classification", "inv");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.InventoryItemTypeClassificationId).HasColumnName("inventory_item_type_classification_id");

                entity.HasOne(d => d.InventoryItemTypeClassification)
                    .WithMany(p => p.WarehouseInventoryItemTypeClassifications)
                    .HasForeignKey(d => d.InventoryItemTypeClassificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("warehouse_inventory_item_type_inventory_item_type_classifi_fkey");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseInventoryItemTypeClassifications)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("warehouse_inventory_item_type_classification_warehouse_id_fkey");
            });

            modelBuilder.Entity<WarehousePermission>(entity =>
            {
                entity.HasKey(e => new { e.WarehouseId, e.TargetWarehouseId })
                    .HasName("warehouse_permission_pkey");

                entity.ToTable("warehouse_permission", "inv");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.TargetWarehouseId).HasColumnName("target_warehouse_id");

                entity.HasOne(d => d.TargetWarehouse)
                    .WithMany(p => p.WarehousePermissionTargetWarehouses)
                    .HasForeignKey(d => d.TargetWarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("warehouse_permission_target_warehouse_id_fkey");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehousePermissionWarehouses)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("warehouse_permission_warehouse_id_fkey");
            });

            modelBuilder.Entity<WarehouseStructure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("warehouse_structure", "inv");

                entity.Property(e => e.Structure).HasColumnName("structure");

                entity.Property(e => e.StructureId).HasColumnName("structure_id");

                entity.Property(e => e.StructureName).HasColumnName("structure_name");
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasKey(e => e.WorkOrderNumber)
                    .HasName("work_order_pkey");

                entity.ToTable("work_order", "core");

                entity.Property(e => e.WorkOrderNumber).HasColumnName("work_order_number");

                entity.Property(e => e.CreateDts)
                    .HasColumnName("create_dts")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.IsAccident).HasColumnName("is_accident");

                entity.Property(e => e.IsFinished).HasColumnName("is_finished");

                entity.Property(e => e.MaintenanceFleetId).HasColumnName("maintenance_fleet_id");

                entity.Property(e => e.Mileage)
                    .HasPrecision(18, 2)
                    .HasColumnName("mileage");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.MaintenanceFleet)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.MaintenanceFleetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_maintenance_fleet_id_fkey");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("work_order_schedule_id_fkey");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_vehicle_id_fkey");
            });

            modelBuilder.Entity<WorkOrderWorkshop>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderNumber, e.WorkshopId })
                    .HasName("work_order_workshop_pkey");

                entity.ToTable("work_order_workshop", "core");

                entity.Property(e => e.WorkOrderNumber).HasColumnName("work_order_number");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.Property(e => e.Done).HasColumnName("done");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.Pending).HasColumnName("pending");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.TotalFinding).HasColumnName("total_finding");

                entity.HasOne(d => d.WorkOrderNumberNavigation)
                    .WithMany(p => p.WorkOrderWorkshops)
                    .HasForeignKey(d => d.WorkOrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_workshop_work_order_number_fkey");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.WorkOrderWorkshops)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_order_workshop_workshop_id_fkey");
            });

            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.ToTable("workshop", "core");

                entity.HasIndex(e => e.Name, "workshop_name_key")
                    .IsUnique();

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.Property(e => e.Library)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("library");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<WorkshopMaintenanceType>(entity =>
            {
                entity.HasKey(e => new { e.WorkshopId, e.MaintenanceTypeId })
                    .HasName("workshop_maintenance_type_pkey");

                entity.ToTable("workshop_maintenance_type", "core");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenance_type_id");

                entity.HasOne(d => d.MaintenanceType)
                    .WithMany(p => p.WorkshopMaintenanceTypes)
                    .HasForeignKey(d => d.MaintenanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("workshop_maintenance_type_maintenance_type_id_fkey");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.WorkshopMaintenanceTypes)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("workshop_maintenance_type_workshop_id_fkey");
            });

            modelBuilder.Entity<WorkshopWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.WorkshopId, e.WarehouseId })
                    .HasName("workshop_warehouse_pkey");

                entity.ToTable("workshop_warehouse", "core");

                entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

                entity.Property(e => e.EarlySelect).HasColumnName("early_select");

                entity.Property(e => e.LibraryPath)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("library_path")
                    .HasDefaultValueSql("'Built-in'::character varying");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.WorkshopWarehouses)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("workshop_warehouse_workshop_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
