namespace TARge21Shop.Core.Dto
{
    public class FileToApiDto
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
