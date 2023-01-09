using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{

    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/autores")]
    public class AutoresControles : ControllerBase
    {

        private readonly ApplicationDBContext context;
        private readonly IServicio servicio;
        private readonly ServiciosTransient serviciosTransient;
        private readonly ServiciosScoped serviciosScoped;
        private readonly ServiciosSingleton serviciosSingleton;

        public AutoresControles(ApplicationDBContext context,
                                        IServicio servicio, ServiciosTransient serviciosTransient,
                                        ServiciosScoped serviciosScoped, ServiciosSingleton serviciosSingleton)
        {

            this.servicio = servicio;
            this.serviciosTransient = serviciosTransient;
            this.serviciosScoped = serviciosScoped;
            this.serviciosSingleton = serviciosSingleton;
            this.context = context;
        }

        [HttpGet("GUID")]
        public ActionResult obtenerGuids()
        {

            return Ok(new
            {
                AutoresController_Trasient = serviciosTransient.Guid,
                ServicioA_Transient = servicio.ObtenerTransient(),
                AutoresControles_Scoped = serviciosScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresControles_Singleton = serviciosSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });

        }

        [HttpGet]// api/listado
        [HttpGet("listado")]// api/autores/listado
        [HttpGet("/listado")] //listado
        public async Task<List<Autor>> Get()
        {

            servicio.RealizarTarea();
            return await context.Autores.Include(x => x.libros).ToListAsync();
        }

        [HttpGet("primero")] //api/autores/primero?nombre=""
        public async Task<ActionResult<Autor>> primerAutor([FromHeader] int miValor, [FromQuery] string nombre)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")] //https://localhost:7182/api/autores/2
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        //parametro opcional en la ruta 
        //[HttpGet("{nombre}/{param2?}")] //https://localhost:7182/api/autores/{nombre} https://localhost:7182/api/autores/César
        [HttpGet("{nombre}/{params2=persona}")] //param2 con un valor por defecto https://localhost:7182/api/autores/Gloria/carro
        public async Task<ActionResult<Autor>> GetNombreAutor(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {

            var existeAutorConMismoNombre = await context.Autores.AnyAsync(x => x.Name == autor.Name);

            if (existeAutorConMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Name}");
            }


            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
