using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Extension.Fixtures.Abstractions
{
    public interface IFixtureHelper
    {
        Task<IEnumerable<TEntity>> Get<TEntity>();
        void Feed<TEntity>(ModelBuilder builder) where TEntity : class;
        Task Save<TEntity>(IEnumerable<TEntity> entities);
    }
}