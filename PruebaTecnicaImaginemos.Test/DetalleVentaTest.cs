using AutoMapper;
using Moq;
using PruebaTecnicaImaginemos.Aplicacion.Configs.Profiles;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs;
using PruebaTecnicaImaginemos.Aplicacion.Servicios;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Test
{
    public class DetalleVentaTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly ServicioDetalleVenta _servicioDetalleVenta;

        public DetalleVentaTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfig.CreateMapper();

            _servicioDetalleVenta = new ServicioDetalleVenta(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetDetalleVentas_WhenCalled_ReturnsDetalleVentaDTOList()
        {
            // Arrange
            int ventaId = 1;
            var detalleVentas = new List<DetalleVenta>
            {
                new DetalleVenta
                {
                    Id = 1,
                    ProductoId = 1,
                    VentaId = 1,
                    Cantidad = 2,
                    PrecioUnitario = 100,
                    Total = 200
                }
            };
            _unitOfWorkMock.Setup(u => u.RepositorioDetalleVenta.GetList(ventaId))
                .ReturnsAsync(detalleVentas);

            // Act
            var result = await _servicioDetalleVenta.GetDetalleVentas(1);

            // Assert
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DetalleVentaDTO>>(result);
        }

        [Fact]
        public async Task GetDetalleVenta_WhenCalled_ReturnsDetalleVentaDTO()
        {
            // Arrange
            int ventaId = 1;
            int detalleId = 1;
            var detalleVenta = new DetalleVenta
            {
                Id = 1,
                ProductoId = 1,
                VentaId = 1,
                Cantidad = 2,
                PrecioUnitario = 100,
                Total = 200
            };
            _unitOfWorkMock.Setup(u => u.RepositorioDetalleVenta.Get(ventaId, detalleId))
                .ReturnsAsync(detalleVenta);

            // Act
            var result = await _servicioDetalleVenta.GetDetalleVenta(ventaId, detalleId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DetalleVentaDTO>(result);
        }

        [Fact]
        public async Task PostDetalleVenta_WhenCalled_ThrowsNotImplementedException()
        {
            // Arrange
            var detalleVentaDTO = new DetalleVentaDTO
            {
                Id = 1,
                ProductoId = 1,
                Cantidad = 2,
                PrecioUnitario = 100,
                Total = 200
            };

            // Act
            async Task Act() => await _servicioDetalleVenta.PostDetalleVenta(detalleVentaDTO);

            // Assert
            await Assert.ThrowsAsync<NotImplementedException>(Act);
        }

        [Fact]
        public async Task PutDetalleVenta_WhenCalled_DeletesDetalleVenta()
        {
            // Arrange
            int ventaId = 1;
            int detalleId = 1;
            
            _unitOfWorkMock.Setup(u => u.RepositorioDetalleVenta.Delete(ventaId, detalleId))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioDetalleVenta.DeleteDetalleVenta(ventaId, detalleId);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioDetalleVenta.Delete(ventaId, detalleId), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
