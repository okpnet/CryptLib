using System;

namespace CryptLib
{
    /// <summary>
    /// 暗号化クラス
    /// </summary>
    public static class Crypter
    {
        const int passMaxlen = 4;
        const int sourceLen = 1;
        /// <summary>
        /// 暗号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static byte[] GetEncryptByte(byte[] source, string pass)
        {
            var inst = new Crypt();
            try
            {
                return inst.EncryptHex(source, pass);
            }
            catch (Exception ex)
            {
                throw new CryptLibException("Encrypt was not done.", ex);
            }
        }
        /// <summary>
        /// 復号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static byte[] GetDecryptByte(byte[] source, string pass)
        {
            var inst = new Crypt();
            var result = string.Empty;
            try
            {
                return inst.DecryptHex(source, pass);
            }
            catch (Exception ex)
            {
                throw new CryptLibException("Decrypt was not done.", ex);
            }
        }
        /// <summary>
        /// 暗号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static string GetEncryptStr(string source,string pass)
        {
            var inst= new Crypt();
            var result = string.Empty;
            try
            {
                result = inst.EncryptHex(source.ToUtf8Byte(), pass).ToBase64Str();
            }catch(Exception ex)
            {
                throw new CryptLibException("Encrypt was not done.", ex);
            }
            return result;
        }
        /// <summary>
        /// 復号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static string GetDecryptStr(string source, string pass)
        {
            var inst = new Crypt();
            var result = string.Empty;
            try
            {
                result = inst.DecryptHex(source.ToBase64Byte(), pass).ToUtf8Str();
            }
            catch (Exception ex)
            {
                throw new CryptLibException("Decrypt was not done.", ex);
            }
            return result.TrimEnd('\0');
        }
        /// <summary>
        /// 暗号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static string GetEncryptStrHex(string source, string pass)
        {
            var inst = new Crypt();
            var result = string.Empty;
            try
            {
                result = inst.EncryptHex(source.ToUtf8Byte(), pass).ToHexStr();
            }
            catch (Exception ex)
            {
                throw new CryptLibException("Encrypt was not done.", ex);
            }
            return result;
        }
        /// <summary>
        /// 復号化Staticメソッド
        /// </summary>
        /// <returns></returns>
        public static string GetDecryptStrHex(string source, string pass)
        {
            var inst = new Crypt();
            var result = string.Empty;
            try
            {
                result = inst.DecryptHex(source.ToBytesFromHexStr(), pass).ToUtf8Str();
            }
            catch (Exception ex)
            {
                throw new CryptLibException("Decrypt was not done.", ex);
            }
            return result.TrimEnd('\0');
        }

        //public static void ErrorCheckThrowException(string pass,string source)
        //{
        //    if (passMaxlen > pass.Length || 16 > source.Length)
        //        throw new FastCryptException($"Invalid pass or source string length.:source length({source.Length}).pass length({pass.Length})");
        //}
    }
}
