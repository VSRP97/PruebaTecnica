using AutoMapper;
using Moq;
using PruebaTecnicaImaginemos.Aplicacion.Configs.Profiles;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta;
using PruebaTecnicaImaginemos.Aplicacion.Servicios;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Test
{
    public class VentaTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly ServicioVenta _servicioVenta;

        public VentaTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfig.CreateMapper();

            _servicioVenta = new ServicioVenta(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetVentas_WhenCalled_ReturnsVentaDTOList()
        {
            // Arrange
            var ventas = new List<Venta>
            {
                new Venta
                {
                    Id = 1,
                    Fecha = DateTime.Now,
                    UsuarioId = 1,
                    Total = 500
                }
            };
            _unitOfWorkMock.Setup(u => u.RepositorioVenta.GetList(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(ventas);

            // Act
            var result = await _servicioVenta.GetVentas(DateTime.MinValue, DateTime.MaxValue, "");

            // Assert
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<VentaDTO>>(result);
        }

        [Fact]
        public async Task GetVenta_WhenCalled_ReturnsVentaDTO()
        {
            // Arrange
            var venta = new Venta
            {
                Id = 1,
                Fecha = DateTime.Now,
                UsuarioId = 1,
                Total = 500
            };
            _unitOfWorkMock.Setup(u => u.RepositorioVenta.Get(1))
                .ReturnsAsync(venta);

            // Act
            var result = await _servicioVenta.GetVenta(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<VentaDTO>(result);
        }

        [Fact]
        public async Task PostVenta_WhenCalled_AddsVentaAndReturnsPostVentaDTO()
        {
            // Arrange
            var ventaRequestDTO = new PostVentaRequestDTO
            {
                NombreUsuario = "Usuario",
                DNI = "12345678",
                Detalles = new List<PostVentaDetalleDTO>
                {
                    new PostVentaDetalleDTO
                    {
                        ProductoId = 1,
                        Cantidad = 2
                    }
                }
            };
            _unitOfWorkMock.Setup(u => u.RepositorioVenta.Add(It.IsAny<Venta>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.Get(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Producto>());
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _servicioVenta.PostVenta(ventaRequestDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostVentaDTO>(result);
            _unitOfWorkMock.Verify(u => u.RepositorioVenta.Add(It.IsAny<Venta>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task PutVenta_WhenCalled_UpdatesVenta()
        {
            // Arrange
            var ventaRequestDTO = new VentaDTO
            {
                Id = 1,
                Fecha = DateTime.Now,
                UsuarioId = 1,
                NombreUsuario = "Usuario",
                Total = 500
            };

            _unitOfWorkMock.Setup(u => u.RepositorioVenta.Update(It.IsAny<Venta>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioVenta.PutVenta(ventaRequestDTO);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioVenta.Update(It.IsAny<Venta>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteVenta_WhenCalled_DeletesVenta()
        {
            // Arrange
            var ventaId = 1;

            _unitOfWorkMock.Setup(u => u.RepositorioVenta.Delete(ventaId))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioVenta.DeleteVenta(ventaId);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioVenta.Delete(ventaId), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
