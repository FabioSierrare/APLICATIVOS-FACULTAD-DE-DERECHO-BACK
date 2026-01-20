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

        public async Task<List<Usuarios>> GetUsuariosProfesores()
        {
            var data = await context.Usuarios.Where(u => u.RolId == 3).ToListAsync();
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

        public async Task<bool> PostUsuariosListado(List<UsuarioEstudianteRegistro> usuarios)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                foreach (var user in usuarios)
                {
                    //Desgloso el usuario y el consultorio
                    var usuario = user.Usuarios;
                    var consultorio = user.Consultorio;

                    var existeUsuario = await context.Usuarios
                        .FirstOrDefaultAsync(u => u.Documento == usuario.Documento);

                    if (existeUsuario != null)
                    {
                        //Se actualizan los datos del usuario existente
                        existeUsuario.Correo = usuario.Correo;
                        existeUsuario.Contrasena = usuario.Contrasena;
                        existeUsuario.Nombre = usuario.Nombre;
                        existeUsuario.TipoDocumentoId = usuario.TipoDocumentoId;

                        //Actualizar usuario
                        context.Usuarios.Update(existeUsuario);

                        //Se busca si el usuario ya tiene un consultorio asignado
                        var consultorioExiste = await context.UsuarioConsultorio
                            .FirstOrDefaultAsync(uc => uc.UsuarioId == existeUsuario.Id);

                        //Si ya tiene un consultorio, se actualiza de lo contrario se crea
                        if (consultorioExiste != null)
                        {
                            consultorioExiste.ConsultorioId = consultorio.ConsultorioId;
                            context.UsuarioConsultorio.Update(consultorioExiste);
                        }
                        else
                        {
                            consultorio.UsuarioId = existeUsuario.Id;
                            await context.UsuarioConsultorio.AddAsync(consultorio);
                        }

                        continue;
                    }

                    await context.Usuarios.AddAsync(usuario);
                    await context.SaveAsync();

                    consultorio.UsuarioId = usuario.Id;
                    await context.UsuarioConsultorio.AddAsync(consultorio);

                }

                await context.SaveAsync();
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

        public async Task<InfoUser> GetInfoUser(int id)
        {
            var infoUser = new InfoUser();
            infoUser.usuario = GetUsuarioById(id).Result;

            infoUser.turno = await context.Turnos
                .Where(t => t.UsuarioId == id)
                .ToListAsync();

            infoUser.tipoDocumento = await context.TiposDocumento.ToListAsync(); 
            infoUser.consultorioInfo = await context.Consultorios.ToListAsync();
            infoUser.consultorioId = await context.UsuarioConsultorio
                .Where(uc => uc.UsuarioId == id)
                .Select(uc => uc.ConsultorioId)
                .FirstOrDefaultAsync();

            return infoUser;
        }

        public async Task<bool> PutUsuarioEstudiante(Usuarios usuarios, UsuarioConsultorio consultorio)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Actualiza Usuario primero
                context.Usuarios.Update(usuarios);

                // 2️⃣ Guardar Consultorio al cual pertenece el usuario (independiente)
                // 2️⃣ Guardar Consultorio
                var idconsultorio = await context.UsuarioConsultorio.Where(ct => ct.UsuarioId == consultorio.UsuarioId).Select(ct => ct.Id).FirstOrDefaultAsync();
                consultorio.Id = idconsultorio;
                context.UsuarioConsultorio.Update(consultorio);
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
