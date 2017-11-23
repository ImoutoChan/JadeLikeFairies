using System.Collections.Generic;

namespace JadeLikeFairies.Data.Entities
{
    public class NovelType : EntityBase
    {
        public string Name { get; set; }

        public List<Novel> Novels { get; set; }
    }
}