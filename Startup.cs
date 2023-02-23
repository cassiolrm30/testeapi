using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using TesteAPI.Configurations;
using TesteAPI.Interfaces;
using TesteAPI.Models;
using TesteAPI.Repositories;

namespace TesteAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Configura��o do mapper
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutomapperConfig()); });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Configura��o do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger de Teste de API", Version = "v1.0.0" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(options => { options.AddPolicy("Development", buider => buider.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

            // Configura��o da conex�o com banco de dados
            //services.AddDbContext<Contexto>(options => { options.UseSqlServer(Configuration.GetConnectionString("Conexao")); });
            // Configura��o do contexto, das interfaces e classes de reposit�rios
            services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
            services.AddDbContext<Contexto>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexao")));
            services.AddScoped<ITesteRepository, TesteRepository>();
        }

        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste API"); });
            app.UseCors("Development").UseCors(
                    x => x.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                );
        }
    }
}