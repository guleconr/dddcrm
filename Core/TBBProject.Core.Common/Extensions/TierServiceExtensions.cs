using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Common.Extensions
{
    public static class TierServiceExtensions
    {
        private class TierServiceConfigureModel
        {
            public TierServiceConfigureModel()
            {
                Business = new List<Business>();
                DataLayer = new List<DataLayer>();
            }
            public List<Business> Business { get; set; }
            public List<DataLayer> DataLayer { get; set; }
        }
        private class Business
        {
            public string Key { get; set; }
            public string Value { get; set; }

        }
        private class DataLayer
        {
            public string Key { get; set; }
            public string Value { get; set; }

        }
        private class ExternalService
        {
            public string Key { get; set; }
            public string Value { get; set; }

        }

        private static TierServiceConfigureModel GetTierServiceConfig(IConfiguration configuration)
        {
            var bscModel = new TierServiceConfigureModel();

            var c = configuration.GetSection("TierServiceConfigureModel").GetChildren().ToList();

            var business = c[0].GetChildren();
            foreach (var _m in business)
            {
                bscModel.Business.Add(new Business() { Key = _m.Key, Value = _m.Value });
            }
            var dataLayer = c[1].GetChildren();
            foreach (var _m in dataLayer)
            {
                bscModel.DataLayer.Add(new DataLayer() { Key = _m.Key, Value = _m.Value });
            }
            return bscModel;
        }

        public static IServiceCollection AddTierService(this IServiceCollection services, IConfiguration configuration)
        {
            var model = GetTierServiceConfig(configuration);
            if (model.Business == null || model.Business.Count == 0)
            {
                throw new Exception("Business Service assembly is not found in Configuration File.");
            }

            for (var i = 0; i < model.Business.Count; i++)
            {
                var callingAssembly = Assembly.GetCallingAssembly();
                var path = Path.GetDirectoryName(callingAssembly.Location);
                var serverAssembly = Assembly.LoadFile(path + "\\" + model.Business[i].Value + ".dll");
                foreach (var item in serverAssembly.ExportedTypes.Where(x => x.Name.EndsWith("Business")))
                {
                    var interfaceAssembly = item.GetInterfaces()[0];
                    services.Add(new ServiceDescriptor(interfaceAssembly, item, ServiceLifetime.Scoped));
                }
            }
            for (int i = 0; i < model.DataLayer.Count; i++)
            {
                var callingAssembly = Assembly.GetCallingAssembly();
                var path = Path.GetDirectoryName(callingAssembly.Location);
                var serverAssembly = Assembly.LoadFile(path + "\\" + model.DataLayer[i].Value + ".dll");
                foreach (var item in serverAssembly.ExportedTypes.Where(x => x.Name.EndsWith("DataLayer")))
                {
                    var interfaceAssembly = item.GetInterfaces()[0];
                    services.Add(new ServiceDescriptor(interfaceAssembly, item, ServiceLifetime.Scoped));
                }
            }

            return services;
        }
    }
}