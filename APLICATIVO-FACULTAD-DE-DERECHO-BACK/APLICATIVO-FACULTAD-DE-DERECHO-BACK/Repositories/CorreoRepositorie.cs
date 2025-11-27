using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class CorreoRepositorie : ICorreo
    {
        private readonly IConfiguration _configuration;

        public CorreoRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarCorreo(Correo correo)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(
                    emailSettings["Correo"],
                    emailSettings["Correo"]
                ));
                email.To.Add(MailboxAddress.Parse(correo.Para));
                email.Subject = correo.Asunto;

                email.Body = new TextPart("html")
                {
                    Text = correo.Cuerpo
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    emailSettings["ServidorSMTP"],
                    int.Parse(emailSettings["PuertoSMTP"]),
                    SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    emailSettings["Correo"],
                    emailSettings["Contraseña"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para envíos masivos
        public async Task<bool> EnviarCorreosMasivos(List<Correo> correos)
        {
            try
            {
                foreach (var correo in correos)
                {
                    await EnviarCorreo(correo);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
