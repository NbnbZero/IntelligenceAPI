using System;
using System.Collections.Generic;
using System.Net;
using Intelligent.API.Framework;
using Intelligent.API.Models;
using Intelligent.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using Intelligent.Data.AzureFiles;
using Intelligent.Data.AzureTables;
using Intelligent.Data.Cosmos;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            services.AddHttpClient("private-api", c =>
                {
                    c.BaseAddress = new Uri("https://dev-api-intelligent-mixed-reality.azurewebsites.net");
                    c.DefaultRequestHeaders.Add("Accept", MimeTypes.Application.Json);
                    c.DefaultRequestHeaders.Add("User-Agent", "IMR-Public");
                });
            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));
            services.AddSingleton<ITokenService, OktaTokenService>();
            services.AddTransient<IApiService, SimpleApiService>();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-510206.oktapreview.com/oauth2/default";
                    options.Audience = "api://default";
                    options.RequireHttpsMetadata = false;
                });





            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //            services.AddApiVersioning();
      
//            var oktaMvcOptions = new OktaMvcOptions();
//            Configuration.GetSection("Okta").Bind(oktaMvcOptions);
//            // Grab list of updated variables.
//            oktaMvcOptions.Scope = new List<string> { "openid", "profile", "email" };
//            oktaMvcOptions.GetClaimsFromUserInfoEndpoint = true;
//
//            services.AddAuthentication(options =>
//                {
//                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
//                })
//                .AddCookie()
//                .AddOktaMvc(oktaMvcOptions);

            var mvcBuilder = services.AddMvc();
        }

        public void ConfigureStorage()
        {
            CloudFileContext.InitializeContext(
                Configuration["AzureCloudStorage:UserStoreName"],
                Configuration["AzureCloudStorage:UserStoreKey"]
            );
            CloudTableContext.InitializeContext(
                Configuration["AzureCloudStorage:UserStoreName"],
                Configuration["AzureCloudStorage:UserStoreKey"]
            );
            CosmosContext.InitializeContext(
                Configuration["CosmosDb:Database"],
                Configuration["CosmosDb:Endpoint"],
                Configuration["CosmosDb:Key"]
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
