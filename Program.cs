using Microsoft.Extensions.Logging;
using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

//Instanciando la clase Startup
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var logger = app.Services.GetService(typeof(ILogger<Startup>)) as ILogger<Startup>;
startup.Configure(app, app.Environment, logger);


app.Run();