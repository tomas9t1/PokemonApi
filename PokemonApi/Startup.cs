using ApplicationService.BattleModule;
using ApplicationService.PokedexModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.RabbitHandler;
using PokemonApi.RabbitHandler;
using Swashbuckle.AspNetCore.Swagger;

namespace PokemonApi
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
            services.AddMvc();

            services.AddScoped<IPokedexService, PokedexService>();
            services.AddScoped<IBattleService, BattleService>();

            var rabbitClient = new RabbitClient();
            services.AddSingleton<IRabbitClient>(rabbitClient);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Pokemon API",
                    Description = "This is Pokemon API",
                    Contact = new Contact()
                    {
                        Name = "Tomas Labanauskas",
                        Email = "labanauskastomas@yahoo.com",
                        Url = "https://www.linkedin.com/in/tomas-labanauskas-b95949101"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMemoryCache cache, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1"); });
        }
    }
}