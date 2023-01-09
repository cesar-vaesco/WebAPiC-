namespace WebApiAutores.Servicios
{
    public interface IServicio
    {

        Guid ObtenerScoped(); 
        Guid ObtenerTransient(); 
        Guid ObtenerSingleton(); 
        void RealizarTarea();
    }

    public class ServicioA : IServicio
    {

        private readonly ILogger<ServicioA> logger;
        private readonly ServiciosTransient serviciosTransient;
        private readonly ServiciosScoped serviciosScoped;
        private readonly ServiciosSingleton serviciosSingleton;

        public ServicioA(ILogger<ServicioA> logger,
            ServiciosTransient serviciosTransient, ServiciosScoped serviciosScoped, ServiciosSingleton serviciosSingleton)
        {
            this.logger = logger;
            this.serviciosTransient = serviciosTransient;
            this.serviciosScoped = serviciosScoped;
            this.serviciosSingleton = serviciosSingleton;
        }

        public Guid ObtenerTransient() { return serviciosTransient.Guid; }
        public Guid ObtenerScoped() { return serviciosScoped.Guid; }
        public Guid ObtenerSingleton() { return serviciosSingleton.Guid; }

        public void RealizarTarea()
        {

        }
    }

    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {

        }
    }

    public class ServiciosTransient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiciosScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiciosSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }

}
