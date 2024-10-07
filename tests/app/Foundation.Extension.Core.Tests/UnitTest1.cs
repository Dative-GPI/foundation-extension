using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Handlers;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Moq;

namespace Foundation.Extension.Core.Tests
{
  public class UnitTest1
  {
    // {"Authorizations":"[]","TableCode":"\u0022ui.tables.test\u0022"}
    [Fact]
    public async Task Test1Async()
    {
      var tableId = new Guid("289f07a1-726c-45fc-950d-2b6b482f4043");

      var _tableRepository = new Mock<ITableRepository>();
      _tableRepository.Setup(repo => repo.GetFromCode(It.Is<string>(command => true)))
        .ReturnsAsync((string t) => new Table()
        {
          Id = tableId,
          Code = "ui.tables.test",
          EntityType = "A REMPLIR MANUELLEMENT",
          Label = null
        });
      var _entityPropertyTranslationRepository = new Mock<IEntityPropertyApplicationTranslationRepository>();
      _entityPropertyTranslationRepository.Setup(repo => repo.GetMany(It.Is<EntityPropertyApplicationTranslationsFilter>(command => true)))
        .ReturnsAsync((EntityPropertyApplicationTranslationsFilter filter) => new List<EntityPropertyApplicationTranslation>());
      var _columnRepository = new Mock<IColumnRepository>();
      _columnRepository.Setup(repo => repo.GetMany(It.Is<ColumnsFilter>(command => true)))
        .ReturnsAsync((ColumnsFilter filter) => new List<Column>());
      var _requestContextProvider = new Mock<IRequestContextProvider>();
      _requestContextProvider.Setup(repo => repo.Context)
        .Returns(new RequestContext()
        {
          ActorOrganisationId = Guid.NewGuid(),
          ApplicationId = Guid.NewGuid(),
          ActorId = Guid.NewGuid()
        });
      var _userOrganisationTableRepository = new Mock<IUserOrganisationTableRepository>();
      _userOrganisationTableRepository.Setup(repo => repo.Find(It.Is<string>(command => true), It.Is<Guid>(command => true)))
        .ReturnsAsync((string code, Guid actorId) => null);
      var _userOrganisationColumnRepository = new Mock<IUserOrganisationColumnRepository>();
      _userOrganisationColumnRepository.Setup(repo => repo.GetMany(It.Is<UserOrganisationColumnsFilter>(command => true)))
        .ReturnsAsync((UserOrganisationColumnsFilter filter) => new List<UserOrganisationColumnInfos>());
      var _organisationTypeDispositionRepository = new Mock<IOrganisationTypeDispositionRepository>();
      _organisationTypeDispositionRepository.Setup(repo => repo.GetMany(It.Is<ColumnOrganisationTypesFilter>(command => true)))
        .ReturnsAsync((ColumnOrganisationTypesFilter filter) => new List<OrganisationTypeColumnInfos>());

      //simulate an http get on my Foundation.Extension.Core.API.Controllers.UserOrganisationTablesController.GetMany method
      var tableCode = "ui.tables.test";
      var userOrganisationTableQueryHandler = new UserOrganisationTableQueryHandler(
        _requestContextProvider.Object,
        _tableRepository.Object,
        _columnRepository.Object,
        _entityPropertyTranslationRepository.Object,
        _organisationTypeDispositionRepository.Object,
        _userOrganisationTableRepository.Object,
        _userOrganisationColumnRepository.Object
      );

      var result = await userOrganisationTableQueryHandler.HandleAsync(new UserOrganisationTableQuery()
      {
        TableCode = tableCode
      }, null, CancellationToken.None);
      Assert.NotNull(result);
    }
  }
}