
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} de debe de tener más de {1} carácteres")]
        public string Name { get; set; }

        [Range(18, 120)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        
        public int edad { get; set; }
        [CreditCard]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string TarjetaCredito { get; set; }

        [Url]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string URL { get; set; }
        public List<Libro> libros { get; set; }
    }
}
