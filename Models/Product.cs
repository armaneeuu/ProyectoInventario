using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventario.Models
{
    [Table("t_product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("stock")]
        public Decimal? Stock { get; set; }

        [Column("imagename")]
        public String? ImagenName { get; set; }

        [Column("Imagen")]
        public Byte[]? Imagen { get; set; }

        [Column("duedate")]
        public DateTime DueDate { get; set; }

        [Column("status")]
        public String Status { get; set; }

        [Column("Categoria")]
        public String Categoria { get; set; }
        
        public Product()
        {
            DueDate = DateTime.UtcNow;
        }
        
    }
}