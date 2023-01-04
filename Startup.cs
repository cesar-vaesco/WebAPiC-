using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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

            app.UseEndpoints(endpoints => {
            
                endpoints.MapControllers();

            });

        }

    }
}
