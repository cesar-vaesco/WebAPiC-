using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{

    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/autores")]
    public class AutoresControles : ControllerBase
    {

        private readonly ApplicationDBContext context;
        public AutoresControles(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]// api/listado
        [HttpGet("listado")]// api/autores/listado
        [HttpGet("/listado")] //listado
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x => x.libros).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> primerAutor() 
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
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
