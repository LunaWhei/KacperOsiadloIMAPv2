using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

namespace KacperOsiadloIMAP.Security
{
    public class Decryptor
    {
        public static string Decrypt(string encryptedMessage)
        {
            string EncryptionKey = Settings1.Default.EDPassword.ToString();
            encryptedMessage = encryptedMessage.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(encryptedMessage);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encryptedMessage = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return encryptedMessage;
        }
    }
}
