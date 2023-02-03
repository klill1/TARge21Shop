namespace TARge21Shop.Models.Car
{
    public class CarDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int EnginePower { get; set; }
        public decimal Weight { get; set; }
        public decimal Lenght { get; set; }
        public decimal Width { get; set; }
        public int MaxSpeed { get; set; }
        public decimal Mileage { get; set; }
        public string FuelType { get; set; }
        public decimal FuelConsumption { get; set; }
        public DateTime BuiltDate { get; set; }
        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();

        // only in database
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
