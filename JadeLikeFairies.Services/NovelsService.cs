using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JadeLikeFairies.Data;
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
            var dbnovels = await _dbContext.Novels
                .Include(x => x.Tags).ThenInclude(x => x.Tag)
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Include(x => x.Type).ToListAsync();
            
            return _mapper.Map<List<NovelDto>>(dbnovels);
        }
    }
}
