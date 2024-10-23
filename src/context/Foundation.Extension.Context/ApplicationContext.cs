using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;

namespace Foundation.Extension.Context
{
    public class BaseApplicationContext : DbContext
    {

        #region Common
        public DbSet<ImageDTO> Images { get; set; }
        public DbSet<FileDTO> Files { get; set; }
        public DbSet<ApplicationDTO> Applications { get; set; }
        #endregion

        #region Pages
        public DbSet<PageDTO> Pages { get; set; }
        #endregion

        #region PermissionOrganisations
        public DbSet<PermissionOrganisationDTO> PermissionOrganisations { get; set; }
        public DbSet<PermissionOrganisationTypeDTO> PermissionOrganisationTypes { get; set; }
        public DbSet<PermissionOrganisationCategoryDTO> PermissionOrganisationCategories { get; set; }
        public DbSet<RolePermissionOrganisationDTO> RolePermissionOrganisations { get; set; }
        #endregion

        #region PermissionApplications
        public DbSet<PermissionApplicationDTO> PermissionApplications { get; set; }
        public DbSet<PermissionApplicationCategoryDTO> PermissionApplicationCategories { get; set; }
        public DbSet<RolePermissionApplicationDTO> RolePermissionApplications { get; set; }
        #endregion


        #region Translations
        public DbSet<TranslationDTO> Translations { get; set; }
        public DbSet<ApplicationTranslationDTO> ApplicationTranslations { get; set; }
        #endregion

        #region Tables
        public DbSet<TableDTO> Tables { get; set; }
        public DbSet<ColumnDTO> Columns { get; set; }
        public DbSet<OrganisationTypeDispositionDTO> OrganisationTypeDispositions { get; set; }
        public DbSet<EntityPropertyDTO> EntityProperties { get; set; }
        public DbSet<EntityPropertyApplicationTranslationDTO> EntityPropertyApplicationTranslations { get; set; }
        public DbSet<UserOrganisationTableDTO> UserOrganisationTables { get; set; }
        public DbSet<UserOrganisationColumnDTO> UserOrganisationColumns { get; set; }
        #endregion



        public BaseApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Common
            modelBuilder.Entity<ImageDTO>(m =>
            {
                m.HasKey(i => i.Id);
            });

            modelBuilder.Entity<FileDTO>(m =>
            {
                m.HasKey(i => i.Id);
                m.HasOne(i => i.Application)
                    .WithMany()
                    .HasForeignKey(i => i.ApplicationId);
            });

            modelBuilder.Entity<ApplicationDTO>(m =>
            {
                m.HasKey(a => a.Id);
            });
            #endregion

			#region EntityProperties
            modelBuilder.Entity<EntityPropertyDTO>(m =>
            {
                m.HasKey(ep => ep.Id);

                m.Property(ep => ep.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<EntityPropertyApplicationTranslationDTO>(m =>
            {
                m.HasKey(et => et.Id);

                m.HasOne(et => et.EntityProperty)
                    .WithMany()
                    .HasForeignKey(et => et.EntityPropertyId);

                m.HasOne(et => et.Application)
                    .WithMany()
                    .HasForeignKey(et => et.ApplicationId);
            });
            #endregion

            #region Pages
            modelBuilder.Entity<PageDTO>(m =>
            {
                m.HasKey(p => p.Id);
            });
            #endregion

            #region Permissions
            modelBuilder.Entity<PermissionOrganisationDTO>(m =>
            {
                m.HasKey(p => p.Id);
                m.Property(p => p.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<PermissionApplicationDTO>(m =>
            {
                m.HasKey(p => p.Id);
                m.Property(p => p.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<PermissionOrganisationCategoryDTO>(m =>
            {
                m.HasKey(p => p.Id);
                m.Property(p => p.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<PermissionApplicationCategoryDTO>(m =>
            {
                m.HasKey(p => p.Id);
                m.Property(p => p.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<PermissionOrganisationTypeDTO>(m =>
            {
                m.HasKey(otp => otp.Id);
                m.HasOne(otp => otp.Permission)
                    .WithMany()
                    .HasForeignKey(otp => otp.PermissionId);
            });

            modelBuilder.Entity<RolePermissionOrganisationDTO>(m =>
            {
                m.HasKey(rp => rp.Id);
                m.HasOne(rp => rp.PermissionOrganisation)
                    .WithMany()
                    .HasForeignKey(rp => rp.PermissionOrganisationId);
            });

            modelBuilder.Entity<RolePermissionApplicationDTO>(m =>
            {
                m.HasKey(rp => rp.Id);
                m.HasOne(rp => rp.PermissionApplication)
                    .WithMany()
                    .HasForeignKey(rp => rp.PermissionApplicationId);
            });
            #endregion

			#region Tables
			modelBuilder.Entity<ColumnDTO>(m =>
			{
				m.HasKey(c => c.Id);
				m.HasOne(c => c.EntityProperty)
					.WithMany()
					.HasForeignKey(c => c.EntityPropertyId)
					.OnDelete(DeleteBehavior.Cascade);
			});
			#endregion

            #region Translations
            modelBuilder.Entity<TranslationDTO>(m =>
            {
                m.HasKey(t => t.Id);
                m.HasIndex(t => t.Code).IsUnique();

                m.Property(t => t.Translations)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<ApplicationTranslationDTO>(m =>
            {
                m.HasKey(t => t.Id);
                m.HasOne(t => t.Translation)
                    .WithMany()
                    .HasForeignKey(t => t.TranslationId);
                m.HasOne(t => t.Application)
                    .WithMany()
                    .HasForeignKey(t => t.ApplicationId);
            });
            #endregion
        }
    }
}