using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApiAutores.Controllers;
using WebApiAutores.Middleware;
using WebApiAutores.Servicios;

namespace WebApiAutores
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;

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

            services.AddResponseCaching();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tuto C# - ASP - WebAPIAutores", Version = "V1" });
            });

        }

        // Configurar los middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            #region middleware_reubicado
            /*
            app.Use(async (contexto, siguiente) =>
            {

                // Entrada del middleware

                // 1. Creo un MemoryStream para poder manipular
                // y copiarme el cuerpo de la respuesta.
                // Esto se hace porque el stream del cuerpo de la
                // respuesta no tiene permisos de lectura.
                using var ms = new MemoryStream();

                // 2. Guardo la referencia del Stream donde se
                // escribe el cuerpo de la respuesta
                var cuerpoOriginlaRespuesta = contexto.Response.Body;


                // 3. Cambio el stream por defecto del cuerpo
                // de la respuesta por el MemoryStream creado
                // para poder manipularlo
                contexto.Response.Body = ms;

                // 4. Esperamos a que el siguiente middleware
                // devuelva la respuesta.
                await siguiente.Invoke();

                // 5. Nos movemos al principio del MemoryStream
                // Para copiar el cuerpo de la respuesta
                ms.Seek(0, SeekOrigin.Begin);

                // Salida del middleware

                // 6. Leemos stream hasta el final y almacenamos
                // el cuerpo de la respuesta obtenida
                var respuesta = new StreamReader(ms).ReadToEnd();

                // 5. Nos volvemos a posicionar al principio
                // del MemoryStream para poder copiarlo al 
                // cuerpo original de la respuesta
                ms.Seek(0, SeekOrigin.Begin);

                // 7. Copiamos el contenido del MemoryStream al
                // stream original del cuerpo de la respuesta
                await ms.CopyToAsync(cuerpoOriginlaRespuesta);
                
                // 8.Volvemos asignar el stream original al el cuerpo
                // de la respuesta para que siga el flujo normal.
                contexto.Response.Body = cuerpoOriginlaRespuesta;
                    
                // Se muestra la respuesta
                logger.LogInformation(respuesta);
                
            });
            */
            #endregion
            //Middleware que limira a la ruta /ruta1  interceptando la petición

            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();   
            app.UseLoguearRespuestaHTTP();

            app.Map("/ruta1", app =>
            {
                // Ejemplo de middleware que intercepta la ejecución de la aplicación...
                app.Run(async contexto =>
                {
                    await contexto.Response.WriteAsync("Estoy interceptando la tubería..");
                });
            });


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiAutores v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
