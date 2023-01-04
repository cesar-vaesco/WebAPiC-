
using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

//Instanciando la clase Startup
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();