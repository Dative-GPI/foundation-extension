using System;

namespace Foundation.Extension.Domain.Repositories.Filters
{
    public class ColumnsFilter
    {
        public Guid ApplicationId { get; set; }
        public Guid? TableId { get; set; }
    }
}