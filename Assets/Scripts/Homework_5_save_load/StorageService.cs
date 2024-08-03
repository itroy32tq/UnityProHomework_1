using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class StorageService : IStorageService
    {
        private DESCryptoServiceProvider DES;
        private readonly string _sKey;
        private readonly DESCryptoServiceProvider _des;
        private readonly UnicodeEncoding _unicodeEncoding;

        public StorageService()
        {
            _sKey = GenerateKey();
            _des = new DESCryptoServiceProvider();
            _unicodeEncoding = new UnicodeEncoding();
        }

        public void Save(string key, object data)
        {
            string path = BuildPath(key);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            EncryptData(json, path);
        }

        string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            //return Encoding.ASCII.GetString(desCrypto.Key);
            
            // захардкодил ключ для простоты
            return "ABCDEFGH";
        }

        private void EncryptData(string data, string outName)
        {

            byte[] streamData = _unicodeEncoding.GetBytes(data);
            MemoryStream fin = new(streamData);

            FileStream fout = new(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.


            var desKey = Encoding.ASCII.GetBytes(_sKey);
            byte[] desIV = Encoding.ASCII.GetBytes(_sKey);
            CryptoStream encStream = new(fout, _des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        private string DecryptFile(string inName)
        {
            FileStream fin = new(inName, FileMode.Open, FileAccess.Read);
            MemoryStream fout = new();
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.


            var desKey = Encoding.ASCII.GetBytes(_sKey);
            byte[] desIV = Encoding.ASCII.GetBytes(_sKey);
            CryptoStream decStream = new(fout, _des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                decStream.Write(bin, 0, len);
                rdlen = rdlen + len;
                Console.WriteLine("{0} bytes processed", rdlen);
            }

            string result = _unicodeEncoding.GetString(fout.ToArray());

            decStream.Close();
            fin.Close();
            fout.Close();

            return result;
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);
            var decryptData = DecryptFile(path);

            T data = JsonConvert.DeserializeObject<T>(decryptData);
            callback?.Invoke(data);
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}
