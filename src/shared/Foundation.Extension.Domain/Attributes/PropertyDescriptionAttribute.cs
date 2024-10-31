using System;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomPropertyAttribute : Attribute
    {
        public string Description { get; }

        public CustomPropertyAttribute(string description)
        {
            Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StandardPropertyAttribute : Attribute
    {
        public PropertyKind PropertyKind { get; }

        public StandardPropertyAttribute(PropertyKind propertyKind)
        {
            PropertyKind = propertyKind;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StandardForeignPropertyAttribute : Attribute
    {
        public string Owner { get; }
        public PropertyKind PropertyKind { get; }

        public StandardForeignPropertyAttribute(string owner, PropertyKind propertyKind)
        {
            Owner = owner;
            PropertyKind = propertyKind;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CustomForeignPropertyAttribute : Attribute
    {
        public string Owner { get; }
        public string PropertyName { get; }

        public CustomForeignPropertyAttribute(string owner, string propertyName)
        {
            Owner = owner;
            PropertyName = propertyName;
        }
    }
}