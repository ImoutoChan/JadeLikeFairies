using System.Linq;
using AutoMapper;
using JadeLikeFairies.Data.Entities;
using JadeLikeFairies.Services.Dto;

namespace JadeLikeFairies.Services
{
    public class ServicesAutoMapperProfile : Profile
    {
        public ServicesAutoMapperProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<NovelType, NovelTypeDto>();
            CreateMap<Novel, NovelDto>()
                .ForMember(dto => dto.Tags, expression => expression.MapFrom(novel => novel.Tags.Select(x => x.Tag)))
                .ForMember(dto => dto.Genres, expression => expression.MapFrom(novel => novel.Genres.Select(x => x.Genre)))
                .ForMember(dto => dto.AltTitles, expression => expression.MapFrom(novel => novel.AltTitlesCollection.ToList()));
        }
    }
}
