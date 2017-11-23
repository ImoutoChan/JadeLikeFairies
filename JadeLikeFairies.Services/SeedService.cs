using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using JadeLikeFairies.Data;
using JadeLikeFairies.Data.Entities;
using JadeLikeFairies.Services.Abstract;

namespace JadeLikeFairies.Services
{
    public class SeedService : ISeedService
    {
        private readonly FairiesContext _dbContext;

        public SeedService(FairiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();

            var tag1 = new Tag {Name = "tag1", Hint = "This is tag1"};
            var tag2 = new Tag {Name = "tag2", Hint = "This is tag2"};

            var genre1 = new Genre {Name = "genre1", Hint = "This is genre1"};
            var genre2 = new Genre {Name = "genre2", Hint = "This is genre2"};
            var genre3 = new Genre {Name = "genre3", Hint = "This is genre3"};

            var type1 = new NovelType {Name = "Type1"};
            var type2 = new NovelType {Name = "Type2"};

            var novel1 = new Novel
            {
                Title = "Novel1Title",
                AltTitlesCollection = (new [] {"Novel1AltTitle1", "Novel1AltTitle2" }).ToImmutableArray(),
                Type = type1,
                Genres = new List<NovelGenre> { new NovelGenre { Genre = genre1}, new NovelGenre { Genre = genre2} },
                Tags = new List<NovelTag> { new NovelTag { Tag = tag1}, new NovelTag { Tag = tag2} }
            };

            var novel2 = new Novel
            {
                Title = "Novel2Title",
                AltTitlesCollection = (new[] { "Novel2AltTitle1", "Novel2AltTitle2" }).ToImmutableArray(),
                Type = type2,
                Genres = new List<NovelGenre> { new NovelGenre { Genre = genre3 }, new NovelGenre { Genre = genre2 } },
                Tags = new List<NovelTag> { new NovelTag { Tag = tag2 } }
            };

            await _dbContext.Novels.AddRangeAsync(novel1, novel2);

            await _dbContext.SaveChangesAsync();
        }
    }
}
