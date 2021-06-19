using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

[assembly: OwinStartupAttribute(typeof(Vidly.Startup))]
namespace Vidly
{
    public partial class Startup
    {
        // THE CALL OF THIS FUNCTION IS COMMENTED OUT, IT WILL NOT RUN
        // This method, by default, on this version of ASP.NET, does not exist.
        // To use it, install the Microsoft.Extensions.DependencyInjection NuGet
        // package, then write the method and use it on the Configuration method
        // as per the instructions on the links below
        // https://scottdorman.blog/2016/03/17/integrating-asp-net-core-dependency-injection-in-mvc-4/
        // https://stackoverflow.com/questions/37131294/iservicecollection-not-found-in-web-api-with-mvc-6
        public void ConfigureServices(IServiceCollection services)
        {
            // Added AutoMapper functionality as per the instructions on this link
            // https://stackoverflow.com/questions/52703736/automapper-some-static-class-inside-project
            // in order to be able to map Customer to CustomerDto and the reverse.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customer>();
            });
            var mapper = config.CreateMapper();
            // This statement below will add this mapper to the Dependency Injection
            // store, also known as the DI store. From there, for any object created
            // by the MVC, if it has an IMapper mapper variable as a given parameter,
            // it will automatically pick the mapper from the DI store.
            services.AddScoped<IMapper>(c => mapper);
        }

        public void Configuration(IAppBuilder app)
        {
            // var services = new ServiceCollection();
            ConfigureAuth(app);
            // ConfigureServices(services);
        }
    }
}
