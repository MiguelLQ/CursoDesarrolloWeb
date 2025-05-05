using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomerce.share.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Nombre { get; set; } = string.Empty;

    }
}
