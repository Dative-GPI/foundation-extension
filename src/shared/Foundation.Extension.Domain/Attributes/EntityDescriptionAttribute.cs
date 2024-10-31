using System;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class EntityDescriptionAttribute : Attribute
    {
        public string EntityType { get; }
        public EntityKind EntityKind { get; }

        public EntityDescriptionAttribute(string entityType, EntityKind entityKind)
        {
            EntityType = entityType;
            EntityKind = entityKind;
        }
    }
}