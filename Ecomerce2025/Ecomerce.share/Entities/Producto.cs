using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomerce.share.Entities
{
    public class Producto
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es oblifatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es oblifatorio.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es oblifatorio.")]
        [DisplayName("Unidades disponibles.")]
        public int stock { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Descripcion { get; set; }

        [Display(Name = "Foto")]
        public string? URLfoto { get; set; }

        public double Calificacion { get; set; } = 5;
        // relacion producto categoria

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

    }
}
