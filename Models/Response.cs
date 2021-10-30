using System.Collections.Generic;

namespace AliCloud.Models
{
    public class Response<T> where T : class
    {
        public string RequestId { get; set; }

        public List<T> Data { get; set; }

        public Error Error { get; set; }
    }
}
