using AutoMapper;
using Moq;
using PruebaTecnicaImaginemos.Aplicacion.Configs.Profiles;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto;
using PruebaTecnicaImaginemos.Aplicacion.Servicios;
using PruebaTecnicaImaginemos.Dominio.Entidades;

namespace PruebaTecnicaImaginemos.Test
{
    public class ProductoTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly ServicioProducto _servicioProducto;

        public ProductoTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfig.CreateMapper();

            _servicioProducto = new ServicioProducto(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetProductos_WhenCalled_ReturnsProductoDTOList()
        {
            // Arrange
            string nombre = "Agua";
            var productos = new List<Producto>
            {   
                new Producto
                {
                    Id = 1,
                    Nombre = "Agua",
                    Descripcion = "Agua Mineral",
                    Precio = 500
                },
                new Producto
                {
                    Id = 2,
                    Nombre = "Agua carbonatada",
                    Descripcion = "Agua con gas",
                    Precio = 800
                }
            };
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.GetList(nombre))
                .ReturnsAsync(productos);

            // Act
            var result = await _servicioProducto.GetProductos(nombre);

            // Assert
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<ProductoDTO>>(result);
        }

        [Fact]
        public async Task GetProducto_WhenCalled_ReturnsProductoDTO()
        {
            // Arrange
            int productoId = 1;
            var producto = new Producto
            {
                Id = productoId,
                Nombre = "Agua",
                Descripcion = "Agua Mineral",
                Precio = 500
            };
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.Get(productoId))
                .ReturnsAsync(producto);

            // Act
            var result = await _servicioProducto.GetProducto(productoId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductoDTO>(result);
        }

        [Fact]
        public async Task PostProducto_WhenCalled_AddsProductoAndReturnsProductoDTO()
        {
            // Arrange
            var postProductoDTO = new PostProductoRequestDTO
            {
                Nombre = "Agua",
                Descripcion = "Agua Mineral",
                Precio = 500
            };
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.Add(It.IsAny<Producto>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);


            // Act
            var result = await _servicioProducto.PostProducto(postProductoDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductoDTO>(result);
            _unitOfWorkMock.Verify(u => u.RepositorioProducto.Add(It.IsAny<Producto>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task PutProducto_WhenCalled_UpdatesProducto()
        {
            // Arrange
            var productoDTO = new ProductoDTO
            {
                Id = 1,
                Nombre = "Agua",
                Descripcion = "Agua Mineral",
                Precio = 500
            };
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.Update(It.IsAny<Producto>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioProducto.PutProducto(productoDTO);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioProducto.Update(It.IsAny<Producto>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProducto_WhenCalled_DeletesProducto()
        {
            // Arrange
            int productoId = 1;
            _unitOfWorkMock.Setup(u => u.RepositorioProducto.Delete(productoId))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioProducto.DeleteProducto(productoId);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioProducto.Delete(productoId), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}