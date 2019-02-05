using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Intelligent.Data.AzureTables;
using Intelligent.Data.Cosmos;

namespace Intelligent.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureStorage()
        {
            var storageConfig = Configuration.GetSection("AzureCloudStorage");
            var cosmosConfig = Configuration.GetSection("CosmosDb");
            CloudTableContext.InitializeContext(
                storageConfig.GetValue<string>("UserStoreName"),
                storageConfig.GetValue<string>("UserStoreKey")
            );
            CosmosContext.InitializeContext(
                cosmosConfig.GetValue<string>("Database"),
                cosmosConfig.GetValue<string>("Endpoint"),
                cosmosConfig.GetValue<string>("Key")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ConfigureStorage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
