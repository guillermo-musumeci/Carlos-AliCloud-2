using Newtonsoft.Json;
using System.Collections.Generic;

namespace AliCloud.Models
{
    public class Instance
    {
        public string InstanceTypeFamily { get; set; }
        public int? CpuCoreCount { get; set; }
        public int? GPUAmount { get; set; }
        public string GPUSpec { get; set; }
        public string InstanceTypeId { get; set; }
        public float? MemorySize { get; set; }
    }

    public class Data
    {
        public string RequestId { get; set; }

        public string NextToken { get; set; }

        [JsonProperty("InstanceTypes")]
        public InstanceTypes InstanceTypes { get; set; }
    }

    public class InstanceTypes
    {
        [JsonProperty("InstanceType")]
        public List<Instance> Instances { get; set; }
    }
}
