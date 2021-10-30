using AliCloud.Models;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Ecs.Model.V20140526;
using AutoMapper;
using System;
using System.Collections.Generic;
using static Aliyun.Acs.Ecs.Model.V20140526.DescribeRegionsResponse;

namespace AliCloud
{
    public class AliCloud
    {
        private readonly IClientProfile profile;
        private readonly DefaultAcsClient client;

        private Mapper regionMapper;
        private Mapper instanceMapper;

        public AliCloud(string regionId, string accessKeyId, string accessKeySecret)
        {
            profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
            client = new DefaultAcsClient(profile);

            CreateMapper();
        }

        public Response<Region> GetListAliCloudRegions()
        {
            Response<Region> result = new Response<Region>
            {
                Error = new Error()
            };
            try
            {
                DescribeRegionsRequest request = new DescribeRegionsRequest();
                DescribeRegionsResponse response = client.GetAcsResponse(request);
                result.RequestId = response.RequestId;
                result.Data = regionMapper.Map<List<Region>>(response.Regions);
            }
            catch (ServerException ex)
            {
                result.Error = new Error() { ServerError = ex.ErrorMessage };
            }
            catch (ClientException ex)
            {
                result.Error = new Error() { ClientError = ex.ErrorMessage };
            }
            catch (Exception ex)
            {
                result.Error = new Error() { SystemError = ex.Message };
            }
            return result;
        }

        public Response<VM> GetListAliCloudVMSizes()
        {
            Response<VM> result = new Response<VM>();
            try
            {
                DescribeInstanceTypesRequest request = new DescribeInstanceTypesRequest();
                HttpResponse response = client.DoAction(request);
                if (response.isSuccess())
                {
                    string str = System.Text.Encoding.UTF8.GetString(response.Content);
                    Data data = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>(str);
                    result.RequestId = data.RequestId;
                    result.Data = instanceMapper.Map<List<VM>>(data.InstanceTypes.Instances);
                }
                else
                {
                    result.Error = new Error() { StatusCode = response.Status };
                }
            }
            catch (ServerException ex)
            {
                result.Error = new Error() { ServerError = ex.ErrorMessage };
            }
            catch (ClientException ex)
            {
                result.Error = new Error() { ClientError = ex.ErrorMessage };
            }
            catch (Exception ex)
            {
                result.Error = new Error() { SystemError = ex.Message };
            }
            return result;
        }

        private void CreateMapper()
        {
            MapperConfiguration regionConfig = new MapperConfiguration(cfg => cfg.CreateMap<DescribeRegions_Region, Region>());
            regionMapper = new Mapper(regionConfig);

            MapperConfiguration instanceConfig = new MapperConfiguration(cfg => cfg.CreateMap<Instance, VM>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.InstanceTypeId))
                .ForMember(x => x.Family, opt => opt.MapFrom(c => c.InstanceTypeFamily))
                .ForMember(x => x.CPU, opt => opt.MapFrom(c => c.CpuCoreCount))
                .ForMember(x => x.MemoryGB, opt => opt.MapFrom(c => c.MemorySize))
                .ForMember(x => x.GPU, opt =>
                {
                    opt.PreCondition(c => (c.GPUAmount > 0));
                    opt.MapFrom(z => z.GPUAmount + " x " + z.GPUSpec);
                }));
            instanceMapper = new Mapper(instanceConfig);
        }
    }
}
