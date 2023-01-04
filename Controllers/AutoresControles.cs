using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresControles : ControllerBase
    {

        private readonly ApplicationDBContext context;
        public AutoresControles(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor) 
        {
           context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
