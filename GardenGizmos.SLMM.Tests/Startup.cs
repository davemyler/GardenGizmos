using GardenGizmos.SLMM.Model;
using GardenGizmos.SLMM.Navigation;
using GardenGizmos.SLMM.WorkSimulation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GardenGizmos.SLMM.Tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mowingMachine = new MowingMachine(TestTimer.Instance) {
                Orientation = "North",
                Position = new Position()
                {
                    Length = 0,
                    Width = 0
                }
            };
            services.AddSingleton(mowingMachine);

            var lawn = new Lawn()
            {
                Length = 5,
                Width = 5
            };
            services.AddSingleton(lawn);

            var navigator = new Navigator(mowingMachine, lawn);
            navigator.StartNavigation();
            services.AddSingleton(navigator);

            var controllersAssembly = Assembly.Load(new AssemblyName("GardenGizmos.SLMM"));
            services.AddMvc()
                    .AddApplicationPart(controllersAssembly)
                    .AddControllersAsServices()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
