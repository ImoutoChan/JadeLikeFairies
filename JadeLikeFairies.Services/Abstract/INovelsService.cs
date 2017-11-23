using System.Collections.Generic;
using System.Threading.Tasks;
using JadeLikeFairies.Services.Dto;

namespace JadeLikeFairies.Services.Abstract
{
    public interface INovelsService
    {
        Task<List<NovelDto>> GetNovels();
        Task<NovelDto> GetNovel(int id);
        Task<NovelDto> AddNovel(NovelPostDto value);
    }
}