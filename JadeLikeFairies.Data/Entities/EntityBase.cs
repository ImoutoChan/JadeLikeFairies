namespace JadeLikeFairies.Data.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedDate { get; set; }
    }
}