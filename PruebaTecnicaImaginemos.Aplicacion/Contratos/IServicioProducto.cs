using PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos
{
    public interface IServicioProducto
    {
        Task<IEnumerable<ProductoDTO>> GetProductos(string nombre);
        Task<ProductoDTO> GetProducto(int id);
        Task<ProductoDTO> PostProducto(PostProductoRequestDTO producto);
        Task PutProducto(ProductoDTO producto);
        Task DeleteProducto(int id);
    }
}
