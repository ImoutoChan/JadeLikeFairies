using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JadeLikeFairies.Data;
using JadeLikeFairies.Data.Entities;
using JadeLikeFairies.Services.Abstract;
using JadeLikeFairies.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace JadeLikeFairies.Services
{
    public class NovelsService : INovelsService
    {
        private readonly FairiesContext _dbContext;
        private readonly IMapper _mapper;

        public NovelsService(FairiesContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NovelDto>> GetNovels()
        {
            var dbnovels = await GetNovelsWithDeepData()
                .ToListAsync();
            
            return _mapper.Map<List<NovelDto>>(dbnovels);
        }

        public async Task<NovelDto> GetNovel(int id)
        {
            var dbnovel = await GetNovelsWithDeepData()
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<NovelDto>(dbnovel);
        }

        public async Task<NovelDto> AddNovel(NovelPostDto novelDto)
        {
            var newnovel = new Novel
            {
                Title = novelDto.Title,
                AltTitlesCollection = novelDto.AltTitles.ToImmutableArray()
            };

            // set type
            var type = await _dbContext.Types.FirstOrDefaultAsync(x => x.Id == novelDto.TypeId);

            newnovel.Type = type ?? throw new DeepValidationException(nameof(novelDto.TypeId), $"Type with id:{novelDto.TypeId} was not found");

            newnovel.Genres = new List<NovelGenre>();
            foreach (var genreId in novelDto.GenreIds)
            {
                var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == genreId);

                if (genre == null)
                {
                    throw new DeepValidationException(nameof(novelDto.GenreIds), $"Genre with id:{genreId} was not found");
                }

                newnovel.Genres.Add(new NovelGenre {Genre = genre});
            }

            newnovel.Tags = new List<NovelTag>();
            foreach (var tagId in novelDto.TagIds)
            {
                var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == tagId);

                if (tag == null)
                {
                    throw new DeepValidationException(nameof(novelDto.TagIds), $"Tag with id:{tagId} was not found");
                }

                newnovel.Tags.Add(new NovelTag { Tag = tag });
            }

            await _dbContext.Novels.AddAsync(newnovel);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<NovelDto>(newnovel);
        }


        private IQueryable<Novel> GetNovelsWithDeepData() 
            => _dbContext.Novels
                .Include(x => x.Tags).ThenInclude(x => x.Tag)
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Include(x => x.Type);
    }

    public class DeepValidationException : Exception
    {
        public DeepValidationException(string key, string error) 
            : base($"{key} : {error}")
        {
            Key = key;
            Error = error;
        }

        public string Key { get; set; }

        public string Error { get; set; }

    }
}
