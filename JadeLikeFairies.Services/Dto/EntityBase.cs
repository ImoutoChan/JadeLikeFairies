namespace JadeLikeFairies.Services.Dto
{
    public abstract class DtoBase
    {
        public int Id { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedDate { get; set; }
    }
}