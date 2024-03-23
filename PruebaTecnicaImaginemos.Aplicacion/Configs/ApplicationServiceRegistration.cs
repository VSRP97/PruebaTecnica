using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Configs
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IServicioVenta, ServicioVenta>();
            services.AddTransient<IServicioDetalleVenta, ServicioDetalleVenta>();
            services.AddTransient<IServicioProducto, ServicioProducto>();
            services.AddTransient<IServicioUsuario, ServicioUsuario>();

            return services;
        }
    }
}
