using Microsoft.Extensions.DependencyInjection;
using TodoApp.Infrastructure.Mappers;

namespace TodoApp.Infrastructure.Extentions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig)); 
        }
    }
}
