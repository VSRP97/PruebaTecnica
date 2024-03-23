using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta
{
    public class PostVentaRequestDTO
    {
        public string NombreUsuario { get; set; }
        public string DNI { get; set; }
        public IEnumerable<PostVentaDetalleDTO> Detalles { get; set; }
    }
}
