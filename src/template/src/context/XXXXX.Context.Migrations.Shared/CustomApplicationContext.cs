using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Fixtures.Abstractions;

using XXXXX.Context.Kernel;

namespace XXXXX.Context.Migrations.Shared
{
  public class CustomApplicationContext : ApplicationContext
  {
    private ILogger<CustomApplicationContext> _logger;
    private IFixtureHelper _fixtureHelper;

    public CustomApplicationContext(
        ILogger<CustomApplicationContext> logger,
        DbContextOptions<CustomApplicationContext> options,
        IFixtureHelper fixtureHelper) : base(new DbContextOptions<ApplicationContext>(
            options.Extensions.ToDictionary(e => e.GetType()))
        )
    {
      _logger = logger;
      _fixtureHelper = fixtureHelper;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      _fixtureHelper.Feed<PermissionOrganisationDTO>(modelBuilder);
      _fixtureHelper.Feed<PermissionOrganisationCategoryDTO>(modelBuilder);
      _fixtureHelper.Feed<PermissionApplicationDTO>(modelBuilder);
      _fixtureHelper.Feed<PermissionApplicationCategoryDTO>(modelBuilder);
      _fixtureHelper.Feed<TranslationDTO>(modelBuilder);
      _fixtureHelper.Feed<TableDTO>(modelBuilder);
      _fixtureHelper.Feed<EntityPropertyDTO>(modelBuilder);
      _fixtureHelper.Feed<PageDTO>(modelBuilder);
    }
  }
}