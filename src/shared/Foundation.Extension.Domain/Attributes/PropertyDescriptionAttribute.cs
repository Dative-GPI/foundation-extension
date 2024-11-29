using System;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomPropertyAttribute : DefaultTranslationAttribute
    {
        public string Description { get; }

        public CustomPropertyAttribute(string defaultTranslation, string description) : base(defaultTranslation)
        {
            Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StandardPropertyAttribute : DefaultTranslationAttribute
    {
        public PropertyKind PropertyKind { get; }

        public StandardPropertyAttribute(PropertyKind propertyKind) : base(propertyKind.ToString())
        {
            PropertyKind = propertyKind;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StandardForeignPropertyAttribute : DefaultTranslationAttribute
    {
        public string Owner { get; }
        public PropertyKind PropertyKind { get; }

        public StandardForeignPropertyAttribute(string owner, PropertyKind propertyKind) : base($"{owner} {propertyKind.ToString().ToLower()}")
        {
            Owner = owner;
            PropertyKind = propertyKind;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CustomForeignPropertyAttribute : DefaultTranslationAttribute
    {
        public string Owner { get; }
        public string PropertyName { get; }

        public CustomForeignPropertyAttribute(string owner, string propertyName, string defaultTranslation = null) : base(defaultTranslation)
        {
            Owner = owner;
            PropertyName = propertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultTranslationAttribute : Attribute
    {
        public string DefaultTranslation { get; }

        public DefaultTranslationAttribute(string defaultTranslation)
        {
            DefaultTranslation = defaultTranslation;
        }
    }
}