using System.Collections.Generic;

namespace JadeLikeFairies.Data.Entities
{
    public class Genre : EntityBase
    {
        public string Name { get; set; }

        public string Hint { get; set; }

        public List<NovelGenre> Novels { get; set; }
    }
}