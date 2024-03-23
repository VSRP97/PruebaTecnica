using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }

        #region Relaciones
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>(); 
        #endregion
    }
}
