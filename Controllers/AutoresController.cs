using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {

        //Declaración de atributos
        private readonly ApplicationDBContext context;

        //Constructor
        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]// api/listado
        public async Task<List<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
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

        [HttpGet("{nombre}")] //param2 con un valor por defecto https://localhost:7182/api/autores/Gloria/carro
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
