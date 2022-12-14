using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiAutores.Servicios;
using WebApiAutores.Controllers;
using WebApiAutores.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiAutores.Filtros;

namespace WebApiAutores
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
            //var AutoresController = new AutoresController(new AplicationDbContext(null),
            //    new ServicioA(new Logger())
            //    );

            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones =>
            
            opciones.Filters.Add(typeof(FiltroDeExcepcion))
            
            ).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);
            services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //services.AddEndpointsApiExplorer();
            services.AddTransient<IServicio, ServicioA>();

            services.AddTransient<ServicioTransient>();
            services.AddScoped<ServicioScoped>();
            services.AddSingleton<ServicioSingleton>();
            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddSwaggerGen();
            services.AddTransient<MiFiltroDeAccion>();
            services.AddHostedService<EscribirEnArchivo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup>logger)
        {

            //app.Use(async (contexto, siguiente) =>
            //{

            //});
            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
            app.UseLoguearRespuestaHTTP();   
            app.Map("/ruta1", app =>

                {

                    app.Run(async contexto =>

                    {
                        await contexto.Response.WriteAsync("Estoy interceptando la tuberia");
                    });

                });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseResponseCaching();

            app.UseAuthorization();
            //app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
