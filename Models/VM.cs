namespace AliCloud.Models
{
    public class VM
    {
        public string Name { get; set; }

        public string? CPU { get; set; }

        public string? MemoryGB { get; set; }

        public string Family { get; set; }

        public string? GPU { get; set; }
    }
}
