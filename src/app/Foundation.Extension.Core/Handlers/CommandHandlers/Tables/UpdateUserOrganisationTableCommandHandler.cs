using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.Domain.Repositories.Commands;

namespace Foundation.Extension.Core.Handlers
{
  public class UpdateUserOrganisationTableCommandHandler : IMiddleware<UpdateUserOrganisationTableCommand>
  {
    private RequestContext _context;
    private ITableRepository _tableRepository;
    private IUserOrganisationTableRepository _userOrganisationTableRepository;
    private IUserOrganisationColumnRepository _userOrganisationColumnRepository;

    public UpdateUserOrganisationTableCommandHandler
    (
        IRequestContextProvider requestContextProvider,
        ITableRepository tableRepository,
        IUserOrganisationTableRepository userOrganisationTableRepository,
        IUserOrganisationColumnRepository userOrganisationColumnRepository
    )
    {
      _context = requestContextProvider.Context;
      _tableRepository = tableRepository;
      _userOrganisationTableRepository = userOrganisationTableRepository;
      _userOrganisationColumnRepository = userOrganisationColumnRepository;
    }

    public async Task HandleAsync(UpdateUserOrganisationTableCommand command, Func<Task> next, CancellationToken cancellationToken)
    {
      var table = await _tableRepository.GetFromCode(command.TableCode);

      if (table == null)
      {
        throw new Exception(ErrorCode.EntityNotFound);
      }

      var formerColumns = await _userOrganisationColumnRepository.GetMany(new UserOrganisationColumnsFilter()
      {
        UserOrganisationId = _context.ActorOrganisationId.Value,
        TableId = table.Id
      });

      var formerTable = await _userOrganisationTableRepository.Find(table.Code, _context.ActorOrganisationId.Value);


      await _userOrganisationColumnRepository.RemoveRange(formerColumns.Select(c => c.Id));

      await _userOrganisationColumnRepository.CreateRange(command.Columns.Select(c => new CreateUserOrganisationColumn()
      {
        UserOrganisationId = _context.ActorOrganisationId.Value,
        ColumnId = c.ColumnId,
        TableId = table.Id,
        Index = c.Index,
        Hidden = c.Hidden
      }));

      if (formerTable != null)
      {
        await _userOrganisationTableRepository.Remove(formerTable.Id);
      }

      await _userOrganisationTableRepository.Create(new CreateUserOrganisationTable()
      {
        UserOrganisationId = _context.ActorOrganisationId.Value,
        TableId = table.Id,
        Mode = command.Mode,
        RowsPerPage = command.RowsPerPage,
        SortByKey = command.SortByKey,
        SortByOrder = command.SortByOrder
      });
    }
  }
}