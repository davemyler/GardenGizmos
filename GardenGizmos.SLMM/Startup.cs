﻿using GardenGizmos.SLMM.Model;
using GardenGizmos.SLMM.Navigation;
using GardenGizmos.SLMM.WorkSimulation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GardenGizmos.SLMM
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
            var mowingMachine = new MowingMachine(new SleepTimer()) { Position = new Position() };
            Configuration.GetSection("MowingMachine").Bind(mowingMachine);
            services.AddSingleton(mowingMachine);

            var lawn = new Lawn();
            Configuration.GetSection("Lawn").Bind(lawn);
            services.AddSingleton(lawn);

            var navigator = new Navigator(mowingMachine, lawn);
            navigator.StartNavigation();
            services.AddSingleton(navigator);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
