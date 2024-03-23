using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Dominio.Entidades
{
    public class Venta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        #region Relaciones
        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>(); 
        #endregion
    }
}
