using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using CBM_API.Entities;
using Minio.DataModel;

namespace CBM_API.Ultilities
{
    public class Crypto
    {
        static IConfiguration _config;
        public Crypto(IConfiguration config)
        {
            _config = config;

        }

        //public static string PrivateKey = "Secret key - chuoi bi mat";
        public static string Hash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes;
            // SHA
            hashBytes = SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes);
        }

        public static bool VerifyJwt(string jwt)
        {
            try
            {
                // 1. tạo key để xác thực
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:PrivateKey"]));
                SecurityToken stoken;
                // 2. sử dụng phương thức validateToken
                var jwtHandler = new JwtSecurityTokenHandler().ValidateToken(jwt, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out stoken);                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public static string GenerateJwt(Account acc)
        {
            // 1. Tao key để thực hiện ký trên jwt
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:PrivateKey"]));
                       
            // 2. List claims --> chính là phần payload
            List<Claim> claims = new List<Claim>
            {
                new Claim("uid", acc.Id.ToString()),
                new Claim("Email", acc.Email),
                new Claim("Name", acc.Name),
                new Claim("FullName", acc.FullName),
                new Claim("DepartmentId", acc.DepartmentID.ToString()),
            };
            foreach (var item in acc.Roles)
            {
                claims.Add(new Claim (ClaimTypes.Role, item.Name));
            }

            // 3. Tạo chữ ký
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // 4. Tạo Token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: creds
               );

            // 5. Lấy token dưới dạng string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}

