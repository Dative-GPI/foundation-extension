using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Bones.Flow;
using Bones.Exceptions;

namespace Foundation.Extension.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseApplicationContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(BaseApplicationContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Commit()
        {
            try
            {
                var saves = await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical(ex, "Unable to update Database");
                throw new DbUpdateException(ex.Message, ex);
            }
        }
    }
}