using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JadeLikeFairies.Data;
using JadeLikeFairies.Data.Entities;
using JadeLikeFairies.Services.Abstract;
using JadeLikeFairies.Services.Dto;
using JadeLikeFairies.Services.Exceptions;
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
            var type = await _dbContext.Types.FindAsync(novelDto.TypeId);

            newnovel.Type = type ?? throw new DeepValidationException(nameof(novelDto.TypeId), $"Type with id:{novelDto.TypeId} was not found");

            newnovel.Genres = new List<NovelGenre>();
            foreach (var genreId in novelDto.GenreIds)
            {
                var genre = await _dbContext.Genres.FindAsync(genreId);

                if (genre == null)
                {
                    throw new DeepValidationException(nameof(novelDto.GenreIds), $"Genre with id:{genreId} was not found");
                }

                newnovel.Genres.Add(new NovelGenre {Genre = genre});
            }

            newnovel.Tags = new List<NovelTag>();
            foreach (var tagId in novelDto.TagIds)
            {
                var tag = await _dbContext.Tags.FindAsync(tagId);

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

        public async Task Remove(int id)
        {
            var novel = await _dbContext.Novels.FindAsync(id);

            if (novel == null)
            {
                throw new ResourceNotFoundException();
            }

            _dbContext.Novels.Remove(novel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<NovelDto> UpdateNovel(int id, NovelPatchDto updates)
        {
            var novel = await GetNovelsWithDeepData().FirstOrDefaultAsync(x => x.Id == id);
            if (novel == null)
            {
                throw new ResourceNotFoundException();
            }

            if (updates.Title != null)
            {
                novel.Title = updates.Title;
            }

            if (updates.AltTitles != null)
            {

                novel.AltTitlesCollection = updates.AltTitles.ToImmutableArray();
            }

            if (updates.TypeId != null)
            {
                var type = await _dbContext.Types.FindAsync(updates.TypeId);

                novel.Type = type 
                    ?? throw new DeepValidationException(nameof(updates.TypeId), 
                                                         $"Type with id:{updates.TypeId} was not found");
            }

            if (updates.GenreIds != null)
            {
                novel.Genres = new List<NovelGenre>();
                foreach (var genreId in updates.GenreIds)
                {
                    var genre = await _dbContext.Genres.FindAsync(genreId);

                    if (genre == null)
                    {
                        throw new DeepValidationException(nameof(updates.GenreIds),
                                                          $"Genre with id:{genreId} was not found");
                    }

                    novel.Genres.Add(new NovelGenre {Genre = genre});
                }
            }

            if (updates.TagIds != null)
            {
                novel.Tags = new List<NovelTag>();
                foreach (var tagId in updates.TagIds)
                {
                    var tag = await _dbContext.Tags.FindAsync(tagId);

                    if (tag == null)
                    {
                        throw new DeepValidationException(nameof(updates.TagIds), $"Tag with id:{tagId} was not found");
                    }

                    novel.Tags.Add(new NovelTag { Tag = tag });
                }
            }
            
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<NovelDto>(novel);
        }


        private IQueryable<Novel> GetNovelsWithDeepData() 
            => _dbContext.Novels
                .Include(x => x.Tags).ThenInclude(x => x.Tag)
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Include(x => x.Type);
    }
}
