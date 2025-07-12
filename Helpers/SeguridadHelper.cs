using BCrypt.Net;


namespace GestionRepuestoAPI.Helpers
{
    public class SeguridadHelper
    {

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerificarPassword(string password, string hashAlmacenado)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashAlmacenado);
        }
    }
}
