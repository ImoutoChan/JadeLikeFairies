using System.Collections.Generic;

namespace JadeLikeFairies.Services.Dto
{
    public class NovelDto : DtoBase
    {
        public string Title { get; set; }
        
        public string[] AltTitles { get; set; }

        public NovelTypeDto Type { get; set; }
        
        public List<GenreDto> Genres { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}