### How to use

1. Add class library to your solution
2. Initialize sevice `AliCloud.AliCloud service = new AliCloud.AliCloud(region, key, secret);` where:
    - `region` - any regionId (examples: "cn-hangzhou", "eu-west-1"), *better to use the closest region to you/your server*
    - `key` - AccessKey ID
    - `secret` - AccessKey secret
3. After initialize you can call any method you want: 
    - `GetListAliCloudRegions()` - to get region list
    - `GetListAliCloudVMSizes()` - to get VM list  
4. Theese methods return `Response` that contains:
    - `string RequestId` - requestId to AliCloud server
    - `list<T> Data` - contains list of data that was received from the server where `T`:
        - `Region`
        - `VM`
    - `Error Error` - model containing information about errors if they occur


### List of models:
- Response

        public string RequestId { get; set; }

        public List<T> Data { get; set; }

        public Error Error { get; set; }
- Region  

        public string Status { get; set; }

        public string RegionEndpoint { get; set; }

        public string LocalName { get; set; }

        public string RegionId { get; set; }
- VM  

        public string Name { get; set; }

        public string? CPU { get; set; }

        public string? MemoryGB { get; set; }

        public string Family { get; set; }

        public string? GPU { get; set; } 
- Error  

        public int? StatusCode { get; set; }

        public string? ServerError { get; set; }

        public string? ClientError { get; set; }

        public string? SystemError { get; set; }
  ServerError - error on AliCloud server  
  ClientError - error on sending request
  
### Example

            string regionId = "cn-hangzhou";
            string key = "LTAI5t73XC9sKBTNz48*****";
            string secret = "2DoLrsr69ZlGFJQsAdq5Xsx0p*****";
            AliCloud.AliCloud service = new AliCloud.AliCloud(regionId, key, secret);

            var responseRegions = service.GetListAliCloudRegions(); 
            if(responseRegions.Data.Count > 0) // responseRegions.Data.Count = 25
            {
                Console.WriteLine(responseRegions.RequestId);
                foreach (var region in responseRegions.Data)
                {
                    Console.WriteLine(region.RegionId);
                }
            }

            var responseVMs = service.GetListAliCloudVMSizes();
            if (responseVMs.Data.Count > 0) // responseVMs.Data.Count > 1000
            {
                Console.WriteLine(responseVMs.RequestId);
                foreach (var vm in responseVMs.Data)
                {
                    Console.WriteLine(vm.Name);
                }
            }
