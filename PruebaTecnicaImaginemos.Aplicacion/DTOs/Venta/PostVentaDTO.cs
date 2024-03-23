using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta
{
    public class PostVentaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<int> DetalleVentaIds { get; set; }
    }
}
