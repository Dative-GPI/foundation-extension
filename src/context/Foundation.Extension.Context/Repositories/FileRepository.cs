using System;
using System.IO;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Context.Configurations;
using Foundation.Extension.Context.DTOs;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Context.Repositories
{
    public class FileRepository : IFileRepository
    {
        private FileConfiguration _config;
        private DbSet<FileDTO> _dbSet;
        private ILogger<FileRepository> _logger;
        private IBinaryStorage _binaryStorage;

        public FileRepository(
            BaseApplicationContext context,
            IBinaryStorage binaryStorage,
            IOptions<FileConfiguration> options,
            ILogger<FileRepository> logger)
        {
            _config = options.Value;
            _dbSet = context.Files;
            _logger = logger;
            _binaryStorage = binaryStorage;
        }

        public async Task<IEntity<Guid>> Create(CreateFile payload)
        {
            var path = await _binaryStorage.ComputePath(payload.Data);
            var binaryPath = Path.Combine(_config.BinariesFolder, path);

            var existing = await _dbSet.FirstOrDefaultAsync(i => i.Path == binaryPath);

            // si une image existe déjà avec le même chemin (le même md5) alors pas la peine de la sauvegarder
            if (existing != default)
            {
                return existing;
            }

            await _binaryStorage.Store(binaryPath, payload.Data);

            var dto = new FileDTO
            {
                Id = Guid.NewGuid(),
                Path = binaryPath,
                ContentType = payload.ContentType,
                Size = payload.Data.Length,
                ApplicationId = payload.ApplicationId,
                Label = payload.Label,
                OrganisationId = payload.OrganisationId,
                Scope = payload.Scope,
                UserId = payload.UserId
            };

            _dbSet.Add(dto);

            return dto;
        }

        public async Task<FileDetails> Get(Guid id)
        {
            var dto = await _dbSet.FindAsync(id);

            if (dto == default) return null;

            var data = await _binaryStorage.Get(dto.Path);

            return new FileDetails()
            {
                Id = dto.Id,
                Data = data,
                ContentType = dto.ContentType,
                Size = dto.Size,
                ApplicationId = dto.ApplicationId,
                Label = dto.Label,
                OrganisationId = dto.OrganisationId,
                Scope = dto.Scope,
                UserId = dto.UserId,
                Path = dto.Path
            };
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}