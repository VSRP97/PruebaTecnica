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
    public class UsuarioTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly ServicioUsuario _servicioUsuario;

        public UsuarioTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfig.CreateMapper();

            _servicioUsuario = new ServicioUsuario(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetUsuarios_WhenCalled_ReturnsUsuarioDTOList()
        {
            // Arrange
            string search = "Juan";
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan",
                    DNI = "12345678",
                }
            };
            _unitOfWorkMock.Setup(u => u.RepositorioUsuario.GetList(search))
                .ReturnsAsync(usuarios);

            // Act
            var result = await _servicioUsuario.GetUsuarios(search);

            // Assert
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<UsuarioDTO>>(result);
        }

        [Fact]
        public async Task GetUsuario_WhenCalled_ReturnsUsuarioDTO()
        {
            // Arrange
            int usuarioId = 1;
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "Juan",
                DNI = "12345678",
            };
            _unitOfWorkMock.Setup(u => u.RepositorioUsuario.Get(usuarioId))
                .ReturnsAsync(usuario);

            // Act
            var result = await _servicioUsuario.GetUsuario(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioDTO>(result);
        }

        [Fact]
        public async Task PostUsuario_WhenCalled_ThrowsNotImplementedException() { 
            // Arrange
            var usuarioDTO = new UsuarioDTO
            {
                Id = 1,
                Nombre = "Juan",
                DNI = "12345678",
            };

            // Act
            async Task Act() => await _servicioUsuario.PostUsuario(usuarioDTO);

            // Assert
            await Assert.ThrowsAsync<NotImplementedException>(Act);
        }

        [Fact]
        public async Task PutUsuario_WhenCalled_UpdatesUsuario()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO
            {
                Id = 1,
                Nombre = "Juan",
                DNI = "12345678",
            };
            _unitOfWorkMock.Setup(u => u.RepositorioUsuario.Update(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _servicioUsuario.PutUsuario(usuarioDTO);

            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioUsuario.Update(It.IsAny<Usuario>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuario_WhenCalled_DeletesUsuario()
        {
            // Arrange
            int usuarioId = 1;

            _unitOfWorkMock.Setup(u => u.RepositorioUsuario.Delete(usuarioId))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            // Act
            await _servicioUsuario.DeleteUsuario(usuarioId);
            // Assert
            _unitOfWorkMock.Verify(u => u.RepositorioUsuario.Delete(usuarioId), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
