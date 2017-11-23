using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public class NovelPostDto
    {
        [Required]
        public string Title { get; set; }

        public string[] AltTitles { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int TypeId { get; set; }

        public List<int> GenreIds { get; set; }

        public List<int> TagIds { get; set; }
    }
}