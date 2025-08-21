using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class RolRepositorie : IRol
    {
        private readonly ContextFacultadDerecho _context;
        public RolRepositorie(ContextFacultadDerecho context)
        {
            _context = context;
        }
        public async Task<List<Rol>> GetRol()
        {
            return _context.Rol.ToList();
        }
        public async Task<bool> PostRol(Rol rol)
        {
            try
            {
                _context.Rol.Add(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> PutRol(Rol rol)
        {
            try
            {
                var existingRol = _context.Rol.Find(rol.Id);
                if (existingRol == null)
                {
                    return false;
                }
                existingRol.Nombre = rol.Nombre;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteRol(int id)
        {
            try
            {
                var rol = _context.Rol.Find(id);
                if (rol == null)
                {
                    return false;
                }
                _context.Rol.Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
