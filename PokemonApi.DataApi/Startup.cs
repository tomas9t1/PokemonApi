using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokemonApi.DataApi.CacheHandler;
using PokemonApi.DataApi.CSVReader;
using PokemonApi.Repository.MongoDB;
using Swashbuckle.AspNetCore.Swagger;


namespace PokemonApi.DataApi
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
            services.AddMemoryCache();
            services.AddAutoMapper();
            services.AddScoped<MongoContext>();
            services.AddScoped<IPokemonDataService, PokemonDataService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<ICSVReader, CSVReader.CSVReader>();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Data API",
                    Description = "This is Pokemon Data API.",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Tomas Labanausas", Email = "labanauskastomas@yahoo.com", Url = "https://www.linkedin.com/in/tomas-labanauskas-b95949101" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon Data API");
            });
        }
    }
}