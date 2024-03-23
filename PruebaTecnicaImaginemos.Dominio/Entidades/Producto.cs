namespace PruebaTecnicaImaginemos.Dominio.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }

        #region Relaciones
        public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>(); 
        #endregion
    }
}
