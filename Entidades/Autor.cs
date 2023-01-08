
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} de debe de tener más de {1} carácteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        //[Range(18, 120)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public int edad { get; set; }
        //[CreditCard]
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public string TarjetaCredito { get; set; }

        //[Url]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public string URL { get; set; }
        public List<Libro> libros { get; set; }
        
      
        public int Menor { get; set; }
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var primeraLetra = Name[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Name) });
                }
            }
            if (Menor > Mayor)
            {
                yield return new ValidationResult("El número menor no puede ser más grande que el número mayor", new string[] { nameof(Menor)});
            }

        }
    }
}
