using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options){ }


        // Esta instrucción permite crear una tabla de autores basandose
        // en la clase Autor (Modelo) que se pasa como argumento del Objeto DBSet
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
