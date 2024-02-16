using System;

using Bones.Repository.Interfaces;

namespace Foundation.Template.Context.DTOs
{
    public class UserApplicationDispositionDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public ColumnDTO Column { get; set; }
        public Guid UserApplicationId { get; set; }
        // public UserApplicationDTO UserApplication { get; set; }
        public bool Hidden { get; set; }
        public int Index { get; set; }
        public bool Disabled { get; set; }
    }
}