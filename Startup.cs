using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApiAutores.Servicios;

namespace WebApiAutores
{
    public class Startup
    {
        //Constructor
        public Startup(IConfiguration configuration)
        {


            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        //Configuración de los servicios
        public void ConfigureServices(IServiceCollection services)
        {




            services.AddControllers().AddJsonOptions(x =>
                                                                                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //Configurando el DbContext de la app
            services.AddDbContext<ApplicationDBContext>(options =>
                                                                                            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            /*La web api puede hacer de dos implementaciones de servicios para mostrar la inyección de dependencias*/
            //services.AddSingleton<IServicio, ServicioB>();
            services.AddTransient<IServicio, ServicioA>();

            services.AddTransient<ServiciosTransient>();
            services.AddScoped<ServiciosScoped>();
            services.AddSingleton<ServiciosSingleton>();



            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tuto C# - ASP - WebAPIAutores", Version = "V1" });
            });

        }

        // Configurar los middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

            });

        }

    }
}
