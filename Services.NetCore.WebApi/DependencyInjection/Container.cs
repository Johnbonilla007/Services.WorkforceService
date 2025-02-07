using System;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork.Data.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using System.Linq;
using Services.NetCore.Infraestructure.Mapping.Commons;
using Services.Workforce.Infraestructure.Mapping.User;

namespace Services.NetCore.WebApi.DependencyInjection
{
    public class Container
    {
        private readonly IServiceCollection _services;
        private enum DependencyInjectionTypes
        {
            ApplicationServices,
            DomainServices
        };
        public Container(IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(nameof(services));

            _services = services;

            InitializeUnitsOfWork();
            InitializeDomainServices();
            InitializeApplicationServices();
            InitializeRepositories();
        }

        private void InitializeRepositories()
        {
            _services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommonsProfile>();
                cfg.AddProfile<UserProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            _services.AddSingleton(mapper);
        }

        private void InitializeDomainServices()
        {
            CreateDependencyInjection(DependencyInjectionTypes.DomainServices);
        }

        private void InitializeApplicationServices()
        {
            CreateDependencyInjection(DependencyInjectionTypes.ApplicationServices);

            //_services.AddScoped(typeof(IRestClientFactory), typeof(HttpRestClientFactory));
            //_services.AddScoped(typeof(IRestClient), typeof(HttpRestClient));
            //var clientFactory = new HttpRestClientFactory();

            //RestClientFactory.SetCurrent(clientFactory);
        }

        private void CreateDependencyInjection(DependencyInjectionTypes dependencyInjectionType)
        {
            string projectClassLibrary = dependencyInjectionType == DependencyInjectionTypes.ApplicationServices ? "Services.Workforce.Application" : "Services.Workforce.Domain";

            string servicesType = dependencyInjectionType == DependencyInjectionTypes.ApplicationServices ? "AppService" : "DomainService";


            var services = Assembly.Load(projectClassLibrary)
                                   .GetTypes()
                                   .Where(type => type.Name.EndsWith(servicesType) || type.Name.EndsWith("Service")).ToList();

            var interfaces = services.Where(x => x.IsInterface);

            var implementations = services.Where(x => !x.IsInterface);

            foreach (var item in interfaces)
            {
                var implementationType = implementations.FirstOrDefault(type => type.Name == item.Name[1..]); // Remove 'I' from the interface name

                if (implementationType != null)
                {
                    _services.AddScoped(item, implementationType);
                }
            }
        }

        private void InitializeUnitsOfWork()
        {
            _services.AddScoped(typeof(IQueryableUnitOfWork), typeof(DataContext));
        }
    }
}