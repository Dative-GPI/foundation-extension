using System;
using System.Collections.Generic;

namespace Foundation.Extension.Domain.Models
{
    public interface ITranslatable<TTranslation> where TTranslation : ITranslation
    {
        List<TTranslation> Translations { get; }
    }

    public interface ITranslation
    {
        string LanguageCode { get; }
    }
}