using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace novelangularcore.Server.Infrastructure
{
    public static class PasswordUtils
    {
        // Método para gerar um salt aleatório
        public static string GerarSalt()
        {
            byte[] salt = new byte[128 / 8]; // 128 bits de salt
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        // Método para gerar o hash da senha com salt usado nos logins
        public static string GerarHashSenha(string senha, string saltBase64)
        {
            byte[] salt = Convert.FromBase64String(saltBase64);

            byte[] hash = KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8
            );

            return Convert.ToBase64String(hash);
        }

        public static bool VerificarSenha(string senha, string saltBase64, string hashArmazenado)
        {
            string hashTentativa = GerarHashSenha(senha, saltBase64);
            return hashArmazenado == hashTentativa;
        }
    }
}
