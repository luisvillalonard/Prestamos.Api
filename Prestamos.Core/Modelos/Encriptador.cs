using System.Security.Cryptography;
using System.Text;

namespace Pos.Core.Modelos
{
    public class Encriptador
    {
        public static string key = "C52E0917C3B54DE8A42403181023AF3B";

        public static byte[] GetBytesFromKey()
        {
            return Encoding.UTF8.GetBytes(key);
        }

        public class Base64
        {
            public static string Encode(string texto)
            {
                try
                {
                    byte[] encData_byte = new byte[texto.Length];
                    encData_byte = Encoding.UTF8.GetBytes(texto);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    return encodedData;
                }
                catch (Exception)
                {
                    throw new Exception($"Base64: Error tratando de encriptar la clave del usuario.");
                }
            }

            public static string Decode(string texto)
            {
                try
                {
                    UTF8Encoding encoder = new();
                    Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(texto);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string result = new(decoded_char);
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception($"Base64: Error desencriptando la clave del usuario.");
                }
            }
        }

        public class AES
        {
            public static string Encrypt(string text, byte[] salt)
            {
                try
                {
                    using (var aesAlg = Aes.Create())
                    {
                        using (var encryptor = aesAlg.CreateEncryptor(salt, aesAlg.IV))
                        {
                            using (var msEncrypt = new MemoryStream())
                            {
                                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                using (var swEncrypt = new StreamWriter(csEncrypt))
                                {
                                    swEncrypt.Write(text);
                                }

                                var iv = aesAlg.IV;

                                var decryptedContent = msEncrypt.ToArray();

                                var result = new byte[iv.Length + decryptedContent.Length];

                                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                                Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                                return Convert.ToBase64String(result);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

            public static string Decrypt(string cipherText, byte[] salt)
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[16];

                try
                {
                    Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                    Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

                    using (var aesAlg = Aes.Create())
                    {
                        using (var decryptor = aesAlg.CreateDecryptor(salt, iv))
                        {
                            string result;
                            using (var msDecrypt = new MemoryStream(cipher))
                            {
                                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    using (var srDecrypt = new StreamReader(csDecrypt))
                                    {
                                        result = srDecrypt.ReadToEnd();
                                    }
                                }
                            }

                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public class Hexa
        {
            public static string ByteArrayToString(byte[] bytes)
            {
                try
                {
                    StringBuilder hex = new StringBuilder(bytes.Length * 2);
                    foreach (byte b in bytes)
                        hex.AppendFormat("{0:x2}", b);
                    return hex.ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

            public static byte[] StringToByteArray(String cadena)
            {
                try
                {
                    if (cadena == null)
                        return new byte[] { };
                    else if (cadena.Length == 0)
                        return new byte[] { };

                    int NumberChars = cadena.Length;
                    byte[] bytes = new byte[NumberChars / 2];
                    for (int i = 0; i < NumberChars; i += 2)
                        bytes[i / 2] = Convert.ToByte(cadena.Substring(i, 2), 16);
                    return bytes;
                }
                catch (Exception)
                {
                    return new byte[] { };
                }
            }

            public static string StringToHexadecimal(String cadena)
            {
                try
                {
                    byte[] _bytes = StringToByteArray(cadena);
                    return ByteArrayToString(_bytes);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}
