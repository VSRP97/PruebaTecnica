using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto
{
    public class PostProductoRequestDTO
    {
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; }
    }
}
