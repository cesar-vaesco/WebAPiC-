namespace WebApiAutores.Middleware
{
    public class LoguearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> logger;

        public LoguearRespuestaHTTPMiddleware(RequestDelegate siguiente, ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        // Invoke o invokeAsync
        public async Task InvokeAsync(HttpContext contexto)
        {


            using (var ms = new MemoryStream())
            {

        
                var cuerpoOriginlaRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                var respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);
              
                await ms.CopyToAsync(cuerpoOriginlaRespuesta);
                contexto.Response.Body = cuerpoOriginlaRespuesta;

                // Se muestra la respuesta
                logger.LogInformation(respuesta);

            }
        }
    }

}

