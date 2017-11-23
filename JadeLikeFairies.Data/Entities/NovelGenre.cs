namespace JadeLikeFairies.Data.Entities
{
    public class NovelGenre : EntityBase
    {
        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}