using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Dominio.Entidades
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int VentaId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        #region Relaciones
        public virtual Producto ProductoNavigation { get; set; }
        public virtual Venta VentaNavigation { get; set; } 
        #endregion
    }
}
