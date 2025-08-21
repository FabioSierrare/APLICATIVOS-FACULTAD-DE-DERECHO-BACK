using System.Globalization;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class UsuariosRepositorie : IUsuarios
    {
        private readonly ContextFacultadDerecho context;

        public UsuariosRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<Usuarios>> GetUsuarios()
        {
            var data = await context.Usuarios.ToListAsync();
            return data;
        }

        public async Task<bool> PostUsuarios(Usuarios usuarios)
        {
            await context.Usuarios.AddAsync(usuarios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutUsuarios(Usuarios usuarios)
        {
            context.Usuarios.Update(usuarios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuarios = await context.Usuarios.FindAsync(id);
            if (usuarios == null)
                return false;
            context.Usuarios.Remove(usuarios);
            await context.SaveAsync();
            return true;
        }

        public async Task<Usuarios> GetUsuarioById(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            return usuario; // si no existe devuelve null
        }

        public async Task<bool> PostUsuarios(Usuarios usuarios, UsuarioConsultorio consultorio)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Guardar Usuario primero
                await context.Usuarios.AddAsync(usuarios);
                await context.SaveAsync(); // Necesario para obtener calendario.Id

                // 2️⃣ Guardar Consultorio al cual pertenece el usuario (independiente)
                // 2️⃣ Guardar Consultorio
                consultorio.UsuarioId = usuarios.Id;
                await context.UsuarioConsultorio.AddAsync(consultorio);
                await context.SaveAsync();


                // 3️⃣ Guardar configuración de días (usando calendario.Id)
                // ✅ Confirmar transacción
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // ❌ Revertir cambios si algo falla
                await transaction.RollbackAsync();
                Console.WriteLine($"Error guardando datos: {ex.Message}");
                return false;
            }
        }


    }
}
