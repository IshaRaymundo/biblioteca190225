using Biblioteca_Mia_Raymundo.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca_Mia_Raymundo.Context;

namespace Biblioteca_Mia_Raymundo.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerTodosLosRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> ObtenerRolPorIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task CrearRolAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarRolAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarRolAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return false;
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true; 
        }

    }
}
