using Biblioteca_Mia_Raymundo.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca_Mia_Raymundo.Services
{
    public interface IRolService
    {
        Task<List<Rol>> ObtenerTodosLosRolesAsync();
        Task<Rol> ObtenerRolPorIdAsync(int id);
        Task CrearRolAsync(Rol rol);
        Task ActualizarRolAsync(Rol rol);
        Task<bool> EliminarRolAsync(int id); 
    }
}
