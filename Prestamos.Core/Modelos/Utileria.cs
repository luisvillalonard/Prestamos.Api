using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pos.Core.Modelos
{
    public static class Utileria
    {
        public static byte[] NewSalt()
        {
            byte[] _salt = new byte[24];
            (new Random()).NextBytes(_salt);
            return _salt;
        }
        
        public static byte[] GetSalt(string valor)
        {
            byte[] _salt = new byte[16];
            _salt = Encoding.UTF8.GetBytes(valor);
            return _salt;
        }

        public static string RandomString(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?#$%&*-<>=";
            StringBuilder res = new();
            Random rnd = new();
            while (0 < length--)
                res.Append(valid[rnd.Next(valid.Length)]);
            
            return res.ToString();
        }

        public static void GeneratePassword(string texto, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(texto));
            }
        }
        
        public static bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if ((passwordHash is not null && passwordHash.Length == 0) && 
                (passwordSalt is not null && passwordSalt.Length == 0))
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++)
                        if (computedHash[i] != passwordHash[i]) 
                            return false;
                }
            }
            else
                return false;

            return true;
        }

        public static byte[]? GetBytesFromBase64(string base64)
        {
            byte[]? bytes = null;

            if (string.IsNullOrEmpty(base64))
                return bytes;

            var data = base64.Split(';').LastOrDefault();
            if (data == null)
                return bytes;

            return Convert.FromBase64String(data.Replace("base64,", string.Empty));
        }

        public static string? GetBase64FromBytes(byte[] bytes, string extension)
        {
            string? base64 = null;

            if (bytes == null)
                return base64;

            base64 = string.Format("data:image/{1};base64,{0}", Convert.ToBase64String(bytes), extension ?? "png");
            return base64;
        }
    }
}
