namespace AliCloud.Models
{
    public class Error
    {
        public int? StatusCode { get; set; }

        public string? ServerError { get; set; }

        public string? ClientError { get; set; }

        public string? SystemError { get; set; }
    }
}
