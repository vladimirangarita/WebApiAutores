using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido {0}")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} no debe tener mas de {1}  caracteres")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        //[Range(18, 120)]
        //[NotMapped]
        //public int Edad { get; set; }
        //[CreditCard]
        //[Required(ErrorMessage = "Requerido {0}")]
        //[NotMapped]
        //public string TarjetaDeCredito { get; set; }
        //[Url]
        //[NotMapped]
        //public string URL { get; set; }
        [NotMapped]
        public int Menor { get; set; }
        public int Mayor { get; set; }
        public List<Libro> Libros { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera Letra debe ser mayuscula", 
                    new string[] { nameof(Nombre) });
                }
            }
            if (Menor>Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande ue el campo mayor",
                new string[] { nameof(Menor) });
            }
        }
    }
}
