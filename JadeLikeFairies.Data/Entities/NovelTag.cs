namespace JadeLikeFairies.Data.Entities
{
    public class NovelTag : EntityBase
    {
        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}