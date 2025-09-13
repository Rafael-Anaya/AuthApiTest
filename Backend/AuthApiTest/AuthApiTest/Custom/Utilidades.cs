using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthApiTest.Models;


namespace AuthApiTest.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;

        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encriptarSHA256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[]bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
                return builder.ToString();
            }
        }

        public string generarJWT(Usuario modelo)
        {
            var userClaims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, modelo.Id.ToString()),
                 new Claim(ClaimTypes.Email, modelo.Email)

            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtConfig = new JwtSecurityToken
            (
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);

           
        }

    }
}
