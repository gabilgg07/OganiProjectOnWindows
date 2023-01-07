using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ogani.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{

		static string saltKey = "@gabilgg07-Ogani-2022!@";
        static string privatePassword = "!gabilgg07-development-asp";


        // =======================================================

        private static byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            using (MemoryStream memStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();

                memStream.Position = 0;
                byte[] result = new byte[Convert.ToInt32(memStream.Length)];
                memStream.Read(result, 0, Convert.ToInt32(result.Length));

                return result;
            }
        }

        // ------------------ Encryption -------------------------

        public static string Encrypt(this string text)
        {
            return Encrypt(text, privatePassword);
        }

        public static string Encrypt(this string text, string password)
        {
            var input = Encoding.UTF8.GetBytes(text);

            var output = Encrypt(input, password);

            return Convert.ToBase64String(output);
        }

        private static byte[] Encrypt(byte[] input, string password)
        {
            try
            {
                using (TripleDESCryptoServiceProvider service = new TripleDESCryptoServiceProvider())
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    var key = md5.ComputeHash(Encoding.ASCII.GetBytes($"{saltKey}-{password}-{saltKey}"));
                    var iv = md5.ComputeHash(Encoding.ASCII.GetBytes($"{saltKey}-{string.Join("", password.Reverse())}-{saltKey}"));
                    var encryptor = service.CreateEncryptor(key, iv);


                    return Transform(input, encryptor);
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        // ------------------ Decryption -------------------------

        public static string Decrypt(this string text)
        {
            return Decrypt(text, privatePassword);
            //return text.Decrypt(privatePassword); // belede olar
        }

        public static string Decrypt(this string text, string password)
        {
            var input = Convert.FromBase64String(text);

            var output = Decrypt(input, password);

            return Encoding.UTF8.GetString(output);
        }

        private static byte[] Decrypt(byte[] input, string password)
        {

            try
            {
                using (TripleDESCryptoServiceProvider service = new TripleDESCryptoServiceProvider())
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    var key = md5.ComputeHash(Encoding.ASCII.GetBytes($"{saltKey}-{password}-{saltKey}"));
                    var iv = md5.ComputeHash(Encoding.ASCII.GetBytes($"{saltKey}-{string.Join("", password.Reverse())}-{saltKey}"));
                    var decryptor = service.CreateDecryptor(key, iv);

                    return Transform(input, decryptor);
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

    }
}

