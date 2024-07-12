using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoMapper;

using Foundation.Extension.Domain.Models;

using static Foundation.Extension.Core.AutoMapper.Consts;

namespace Foundation.Extension.Core.AutoMapper
{
    public static class TranslationMapper
    {
        private static Type ITranslatableType;
        private static Type StringType;
        private static string TranslationsPropertyName;

        static TranslationMapper()
        {
            ITranslatableType = typeof(ITranslatable<>);
            TranslationsPropertyName = nameof(ITranslatable<ITranslation>.Translations);
            StringType = typeof(string);
        }

        public static void Map(TypeMap typeMap, IMappingExpression mappingExpression)
        {
            var source = typeMap.SourceTypeDetails;
            var destination = typeMap.DestinationType;

            // Si notre type est translatable -> a une liste de translations
            if (source.Type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == ITranslatableType))
            {
                var translationPropertyInfo = source.GetMember(TranslationsPropertyName) as PropertyInfo;

                if (translationPropertyInfo == null)
                {
                    throw new Exception(ErrorCode.UnexpectedError);
                }

                // on récupère le type de la classe qui contient la traduction : ModelTranslation par exemple
                var translationType = translationPropertyInfo.PropertyType.GenericTypeArguments[0];

                // on récupère les propriétés de cette classe ModelTranslation : par exemple Label, Description 
                // (seulement les strings et pas LanguageCode)
                var translationProperties = translationType.GetMembers()
                    .OfType<PropertyInfo>()
                    .Where(m => m.Name != nameof(ITranslation.LanguageCode) && m.PropertyType == StringType)
                    .ToList();

                // pour chacune de ces propriétés (Label, Description)
                foreach (var property in translationProperties)
                {
                    // on cherche si le viewmodel a une propriété avec le même nom (Label/Description)
                    var destinationMember = destination.GetMember(property.Name).SingleOrDefault() as PropertyInfo;

                    // Le viewmodel ne possède pas la propriété, c'est OK (Les infos n'ont pas les traductions)
                    if (destinationMember == null)
                    {
                        continue;
                    }

                    // on s'assure qu'il y a bien également la même propriété dans l'objet du domain 
                    // c'est la valeur du domain qui est la valeur par défaut
                    var sourceMember = source.GetMember(property.Name) as PropertyInfo;

                    if (sourceMember != null && destinationMember.PropertyType == StringType && sourceMember.PropertyType == StringType)
                    {
                        // on créé la fonction qui va mapper la bonne traduction au viewmodel
                        mappingExpression.ForMember(destinationMember.Name, opt =>
                        {

                            opt.MapFrom(
                                (src, _, _, context) =>
                                {
                                    // on récupère la valeur par défaut
                                    string result = sourceMember.GetValue(src) as string;

                                    if (context.TryGetItems(out var items))
                                    {
                                        // si le language est setté dans le context on va chercher la bonne traduction
                                        if (items.TryGetValue(LANGUAGE, out object contextLanguage) && contextLanguage is string languageCode)
                                        {
                                            var srcTranslations = (IEnumerable<ITranslation>)translationPropertyInfo.GetValue(src);

                                            if (srcTranslations != null)
                                            {
                                                var translation = srcTranslations.FirstOrDefault(t => t.LanguageCode == languageCode);

                                                if (translation != null)
                                                {
                                                    var value = property.GetValue(translation) as string;
                                                    if (!String.IsNullOrWhiteSpace(value))
                                                    {
                                                        result = value;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    return result;
                                }
                            );
                        });
                    }
                    else
                    {
                        throw new Exception(ErrorCode.UnexpectedError);
                    }
                }
            }
        }

        public static void MapFromTranslation<TSource, TDestination, TMember, TTranslation>(this IMemberConfigurationExpression<TSource, TDestination, TMember> opt, Func<TSource, IEnumerable<TTranslation>> getTranslations, Func<TTranslation, string> getValue)
            where TTranslation : ITranslation
        {
            var destinationMember = opt.DestinationMember as PropertyInfo;

            var source = typeof(TSource);
            var sourceProperty = source.GetProperty(destinationMember.Name);

            if (sourceProperty != null)
            {
                opt.MapFrom((src, _, _, context) =>
                {
                    string result = sourceProperty.GetValue(src) as string; // Get value from source as if it was the mapper 
                    if (context.TryGetItems(out var items))
                    {
                        if (items.TryGetValue(LANGUAGE, out object contextLanguage) && contextLanguage is string languageCode)
                        {
                            var translations = getTranslations(src);

                            if (translations != null)
                            {
                                var translation = translations.FirstOrDefault(t => t.LanguageCode == languageCode);

                                if (translation != null)
                                {
                                    var value = getValue(translation);
                                    if (!String.IsNullOrWhiteSpace(value))
                                    {
                                        result = value;
                                    }
                                }
                            }
                        }
                    }
                    return result;
                });
            }
            else
            {
                throw new Exception(ErrorCode.NoMappableProperty);
            }
        }
    }
}