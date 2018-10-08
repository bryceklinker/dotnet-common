using Klinked.Cqrs.AspNetCore.Common;
using Klinked.Cqrs.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.AspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Setup cqrs for startup assembly
            services.AddKlinkedCqrs(b => b.UseAssemblyFor<Startup>());
            
            // Add Setup CQRS for startup assembly with logging decorator
            // services.AddKlinkedCqrs(b => b.UseAssemblyFor<Startup>().AddLogging());
            
            services.AddDbContext<FootballContext>(b => b.UseInMemoryDatabase("Football"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}