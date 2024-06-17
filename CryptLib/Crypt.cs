using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Linq;

namespace CryptLib
{
    class Crypt
    {
        const int passlen = 1;
        const int sourceLen = 1;
        /// <summary>
        /// 暗号化
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private byte[] EncryptStringToBytes_Aes_BouncyCastle(byte[] buffer,string password)
        {
            if (passlen > password.Length || sourceLen > buffer.Length)
                throw new CryptLibException($"Invalid pass or source string length.:source length({buffer.Length}).pass length({password.Length})");
            var key = Hash.GetHashMD5(password).ToBytesFromHexStr();
            //var key = Encoding.UTF8.GetBytes(password);
            var iv = Guid.NewGuid().ToByteArray().Take(16).ToArray();
            var engine = new AesEngine();
            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(blockCipher); // PKCS5/7 padding
            var keyParam = new KeyParameter(key);
            var keyParamWithIV = new ParametersWithIV(keyParam, iv);

            // Encrypt
            cipher.Init(true, keyParamWithIV);
            var encrypted = new byte[cipher.GetOutputSize(buffer.Length)];
            var length = cipher.ProcessBytes(buffer, encrypted, 0);
            cipher.DoFinal(encrypted, length);
            var result = Enumerable.Concat(iv, encrypted).ToArray();

            return result;
        }
        /// <summary>
        /// 複合化
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private byte[] DecryptStringFromBytes_Aes_BouncyCastle(byte[] buffer, string password)
        {
            if (passlen > password.Length || sourceLen > buffer.Length)
                throw new CryptLibException($"Invalid pass or source string length.:source length({buffer.Length}).pass length({password.Length})");
            var key = Hash.GetHashMD5(password).ToBytesFromHexStr();
            var iv = buffer.Take(16).ToArray();
            var source = buffer.Skip(16).ToArray();
            var engine = new AesEngine();
            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(blockCipher); // PKCS5/7 padding
            var keyParam = new KeyParameter(key);
            var keyParamWithIV = new ParametersWithIV(keyParam, iv);

            // Decrypt
            cipher.Init(false, keyParamWithIV);
            var decrypted = new byte[cipher.GetOutputSize(source.Length)];
            var length = cipher.ProcessBytes(source, decrypted, 0);
            cipher.DoFinal(decrypted, length);

            return decrypted;
        }
        /// <summary>
        /// 暗号化
        /// </summary>
        /// <param name="source"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public byte[] EncryptHex(byte[] source, string password)
        {
            var result = this.EncryptStringToBytes_Aes_BouncyCastle(source, password);
            return result;
        }
        /// <summary>
        /// 複合化
        /// </summary>
        /// <param name="source"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public byte[] DecryptHex(byte[] source, string password)
        {
            var result = this.DecryptStringFromBytes_Aes_BouncyCastle(source, password);
            return result;
        }

    }
}
