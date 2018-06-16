using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using WebApplication1.Data.Interfaces;
using WebApplication1.Data.Repositories;
using WebApplication1.Services.Implementation;
using WebApplication1.Services.Interfaces;

namespace WebApplication1
{
    public static class Injections
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<CustomContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddSingleton<IPeopleRepository, PeopleRepository>();
        }
    }
}
