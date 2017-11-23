using System.Collections.Generic;

namespace JadeLikeFairies.Data.Entities
{
    public class Tag : EntityBase
    {
        public string Name { get; set; }

        public string Hint { get; set; }

        public List<NovelTag> Novels { get; set; }
    }
}