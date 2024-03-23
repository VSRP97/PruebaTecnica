using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Persistencia.DbContexts;
using PruebaTecnicaImaginemos.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.Configs
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PruebaTecnicaContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("PruebaConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
