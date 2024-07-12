using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Bones.Repository.Interfaces;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Context.Configurations;

namespace Foundation.Extension.Context.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private DbSet<ImageDTO> _dbSet;
        private FileConfiguration _config;
        private IImageHelper _imageHelper;
        private IBinaryStorage _binaryStorage;
        private ILogger<ImageRepository> _logger;

        public ImageRepository(
            BaseApplicationContext context,
            IImageHelper imageHelper,
            IBinaryStorage binaryStorage,
            IOptions<FileConfiguration> options,
            ILogger<ImageRepository> logger
        ) 
        {
            _config = options.Value;
            _imageHelper = imageHelper;
            _binaryStorage = binaryStorage;
            _dbSet = context.Images;
            _logger = logger;
        }


        public async Task<IEntity<Guid>> Create(CreateImage payload)
        {
            var infos = await _imageHelper.Compute(payload.Data, payload.MaxSize);

            var path = await _binaryStorage.ComputePath(infos.Data);
            var rawPath = Path.Combine(_config.ImagesRawFolder, path);
            var thumbnailPath = Path.Combine(_config.ImagesThumbnailFolder, path);

            var existing = await _dbSet.FirstOrDefaultAsync(i => i.Path == rawPath);
            
            // si une image existe déjà avec le même chemin (le même md5) alors pas la peine de la sauvegarder
            if (existing != default)
            {
                return existing;
            }

            await _binaryStorage.Store(rawPath, infos.Data);
            await _binaryStorage.Store(thumbnailPath, infos.Thumbnail);

            var imageDTO = _dbSet.Add(new ImageDTO()
            {
                Id = Guid.NewGuid(),
                Label = payload.Label,
                Path = rawPath,
                ThumbnailPath = thumbnailPath,
                BlurHash = infos.BlurHash,
                Scope = payload.Scope,
                ApplicationId = payload.ApplicationId,
                OrganisationId = payload.OrganisationId,
                Width = infos.Width,
                Height = infos.Height,
                UserId = payload.UserId,
                Disabled = false
            });

            return imageDTO.Entity;
        }

        public async Task<byte[]> GetRaw(Guid imageId)
        {
            var dto = await _dbSet.AsNoTracking()
                .SingleOrDefaultAsync(i => i.Id == imageId);

            if (dto == default)
            {
                return null;
            }

            return await _binaryStorage.Get(dto.Path);
        }

        public async Task<byte[]> GetThumbnail(Guid imageId)
        {
            var dto = await _dbSet.AsNoTracking()
                .SingleOrDefaultAsync(i => i.Id == imageId);

            if (dto == default)
            {
                return null;
            }

            return await _binaryStorage.Get(dto.ThumbnailPath);
        }
    }
}