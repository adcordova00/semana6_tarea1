using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Login.Config
{
    class Encriptacion
    {
        public static string Encripta(string textoaencriptar, string codigosecreto) {
            byte[] dto = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(codigosecreto);
                aes.IV = dto;
                ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream()) {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoTransform, CryptoStreamMode.Write)) {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream)) {
                            streamWriter.Write(textoaencriptar);
                        }
                        array = memoryStream.ToArray();                     
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public static string Desencripta(string textoaencriptado, string codigosecreto)
        {
            byte[] dto = new byte[16];
            byte[] buffer = Convert.FromBase64String(textoaencriptado);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(codigosecreto);
                aes.IV = dto;
                ICryptoTransform decryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptoTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            
        }

        public static string MD5Encripter(string texto) {

            using (MD5 md5 = MD5.Create()) {
                byte[] entrada = Encoding.UTF8.GetBytes(texto);
                byte[] hashbytes = md5.ComputeHash(entrada);

                StringBuilder stringBuilder = new StringBuilder();
                
                for (int i = 0; i < hashbytes.Length; i++)
                {
                    stringBuilder.Append(hashbytes[i].ToString("X2"));
                }
                return stringBuilder.ToString();

            }

        }
    }
}

