using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.CrossCutting.Helpers;

namespace Foundation.Extension.CrossCutting.Services
{
	public class TranslationsProvider : ITranslationsProvider
	{
		private readonly ITranslationRepository _translationRepository;
		private readonly IApplicationTranslationRepository _applicationTranslationRepository;
		private readonly IFoundationClientFactory _foundationClientFactory;
		private readonly IEntityPropertyRepository _entityPropertyRepository;

		public TranslationsProvider(
			ITranslationRepository translationRepository,
			IApplicationTranslationRepository applicationTranslationRepository,
			IFoundationClientFactory foundationClientFactory,
			IEntityPropertyRepository entityPropertyRepository

		)
		{
			_translationRepository = translationRepository;
			_applicationTranslationRepository = applicationTranslationRepository;
			_foundationClientFactory = foundationClientFactory;

			_entityPropertyRepository = entityPropertyRepository;
		}


		public async Task<IEnumerable<ApplicationTranslation>> GetMany(Guid applicationId, string languageCode, IEnumerable<string> codes = null)
		{
			var defaultTranslations = await _translationRepository.GetMany(
				new TranslationsFilter()
				{
					Codes = codes
				}
			);

			var filter = new ApplicationTranslationsFilter()
			{
				ApplicationId = applicationId,
				LanguageCode = languageCode,
				Codes = codes
			};

			var applicationTranslations = await _applicationTranslationRepository.GetMany(filter);

			var foundationTranslations = await FetchFoundationTranslations(applicationId, languageCode);

			var defaultEntities = await _entityPropertyRepository.GetMany(new EntityPropertiesFilter());

			var translations = defaultTranslations.GroupJoin(
				applicationTranslations,
				@default => @default.Code,
				tr => tr.TranslationCode,
				(@default, translations) =>
				{
					return new ApplicationTranslation()
					{
						Id = @default.Id,
						TranslationCode = @default.Code,
						Value = TranslationsHelper.GetTranslationValue(languageCode, @default, translations),
						LanguageCode = languageCode
					};
				}
			).ToList();

			return translations
				.Concat(foundationTranslations)
				.DistinctBy(t => new {t.TranslationCode, t.LanguageCode})
				.ToList();
		}

		private async Task<IEnumerable<ApplicationTranslation>> FetchFoundationTranslations(Guid applicationId, string languageCode)
		{
			try
			{
				var client = await _foundationClientFactory.CreateAnonymous(applicationId, languageCode);
				var foundationTranslations = await client.Gateway.Translations.Get(languageCode);

				return foundationTranslations.Select(tr => new ApplicationTranslation()
				{
					Id = Guid.Empty,
					TranslationCode = tr.Code,
					LanguageCode = languageCode,
					Value = tr.Value
				});
			}
			catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
			{
				return Enumerable.Empty<ApplicationTranslation>();
			}
		}
	}
}