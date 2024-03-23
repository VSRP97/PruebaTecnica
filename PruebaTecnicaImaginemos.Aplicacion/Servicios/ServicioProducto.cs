using AutoMapper;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Servicios
{
    public class ServicioProducto : IServicioProducto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicioProducto(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDTO>> GetProductos(string nombre)
        {
            var productos = await _unitOfWork.RepositorioProducto.GetList(nombre);
            var productosDTO = _mapper.Map<IEnumerable<ProductoDTO>>(productos);

            return productosDTO;
        }

        public async Task<ProductoDTO> GetProducto(int id)
        {
            var producto = await _unitOfWork.RepositorioProducto.Get(id);
            var productoDTO = _mapper.Map<ProductoDTO>(producto);

            return productoDTO;
        }

        public async Task<ProductoDTO> PostProducto(PostProductoRequestDTO postProductoDTO)
        {
            var producto = _mapper.Map<Producto>(postProductoDTO);
            await _unitOfWork.RepositorioProducto.Add(producto);
            await _unitOfWork.CompleteAsync();
            var productoDTO = _mapper.Map<ProductoDTO>(producto);

            return productoDTO;
        }

        public async Task PutProducto(ProductoDTO productoDTO)
        {
            var producto = _mapper.Map<Producto>(productoDTO);
            await _unitOfWork.RepositorioProducto.Update(producto);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProducto(int id)
        {
            await _unitOfWork.RepositorioProducto.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
