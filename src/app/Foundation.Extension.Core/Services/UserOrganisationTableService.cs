using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.ViewModels;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Core.Services
{
  public class UserOrganisationTableService : IUserOrganisationTableService
  {
    private IQueryHandler<UserOrganisationTableQuery, UserOrganisationTableDetails> _tableQueryHandler;
    private ICommandHandler<UpdateUserOrganisationTableCommand> _updateTableCommandHandler;
    private IMapper _mapper;

    public UserOrganisationTableService
    (
        IQueryHandler<UserOrganisationTableQuery, UserOrganisationTableDetails> tableQueryHandler,
        ICommandHandler<UpdateUserOrganisationTableCommand> updateTableCommandHandler,
        IMapper mapper
    )
    {
      _tableQueryHandler = tableQueryHandler;
      _updateTableCommandHandler = updateTableCommandHandler;
      _mapper = mapper;
    }

    public async Task<UserOrganisationTableDetailsViewModel> GetMany(string tableCode)
    {
      var query = new UserOrganisationTableQuery()
      {
        TableCode = tableCode
      };

      var result = await _tableQueryHandler.HandleAsync(query);

      return _mapper.Map<UserOrganisationTableDetails, UserOrganisationTableDetailsViewModel>(result);
    }

    public async Task Update(string tableCode, UpdateUserOrganisationTableViewModel payload)
    {
      var command = new UpdateUserOrganisationTableCommand()
      {
        TableCode = tableCode,
        Mode = payload.Mode,
        SortByKey = payload.SortBy,
        SortByOrder = payload.SortOrder,
        RowsPerPage = payload.RowsPerPage,
        Columns = payload.Columns.Select(c => new UpdateUserOrganisationColumnCommand()
        {
          ColumnId = c.ColumnId,
          Index = c.Index,
          Hidden = c.Hidden,
          Sortable = c.Sortable,
          Filterable = c.Filterable
        })
      };

      await _updateTableCommandHandler.HandleAsync(command);
    }
  }
}