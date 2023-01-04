﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public LibrosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<Libro>> Get(int id) 
        {

            var existe = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {

            var existeAutor = await context.Autores.AnyAsync(x => x.Id ==  libro.AutorId);
            if (!existeAutor)
            {
                return BadRequest($"No existe el autor de Id:  {libro.AutorId}"); 
             }

            context.Add(libro);
            context.SaveChanges();
            return Ok();
        }

    }
}
