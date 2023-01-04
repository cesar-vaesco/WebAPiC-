using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresControles : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>()
            { 
                new Autor(){  Id = 1, Name= "César Vargas" },
                new Autor(){  Id = 2, Name= "Vero Cortez" },
                new Autor(){  Id = 3, Name= "Glo Cortez" }
            };
        }
    }
}
