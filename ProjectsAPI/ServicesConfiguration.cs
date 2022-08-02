using ProductsAPI.DataAccess;
using ProductsAPI.Logic.Contracts;
using ProductsAPI.Logic;
using ProductsAPI.DataAccess;
using HTMLParser;

namespace ProductsAPI
{
    public static class ServicesConfiguration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreConnection");
            services.AddTransient<IDbContext, ApplicationContext>(x => { return new ApplicationContext(connectionString); });
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IParser, Parser>();
        }
    }
}
