using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;

namespace JadeLikeFairies.Data.Entities
{
    public class Novel : EntityBase
    {
        private const string AltTitlesSeparator = "|;|";

        public string Title { get; set; }

        /// <summary>
        /// Separated by AltTitlesSeparator (|;|)
        /// </summary>
        public string AltTitles { get; private set; }

        [NotMapped]
        public ImmutableArray<string> AltTitlesCollection
        {
            get => AltTitles
                .Split(new[] {AltTitlesSeparator}, StringSplitOptions.RemoveEmptyEntries)
                .ToImmutableArray();

            set => AltTitles = String.Join(AltTitlesSeparator, value);
        }

        public int TypeId { get; set; }
        public NovelType Type { get; set; }
        

        public List<NovelGenre> Genres { get; set; }

        public List<NovelTag> Tags { get; set; }
    }
}