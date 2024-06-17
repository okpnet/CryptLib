using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Text;

namespace CryptLib
{
    /// <summary>
    /// ハッシュクラス
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// MD5ハッシュ
        /// </summary>
        /// <param name="value">変更元</param>
        /// <param name="encoding">エンコード</param>
        /// <returns>ハッシュ文字列</returns>
        public static string GetHashMD5(string value, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            var enc = encoding ?? Encoding.UTF8;
            var buffer = enc.GetBytes(value);
            var provider = new MD5Digest();//MD5.Create();
            var result = new byte[provider.GetDigestSize()];
            provider.BlockUpdate(buffer, 0, buffer.Length);
            provider.DoFinal(result,0) ;// md5.ComputeHash(buffer);
            return BitConverter.ToString(result).Replace("-", ""); ;
        }
        /// <summary>
        /// SHA256ハッシュ
        /// </summary>
        /// <param name="value">変更元</param>
        /// <param name="encoding">エンコード</param>
        /// <returns>ハッシュ文字列</returns>
        public static string GetHashSHA256(string value, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            var enc = encoding ?? Encoding.UTF8;
            var buffer = enc.GetBytes(value);
            var provider = new Sha256Digest();
            var result = new byte[provider.GetDigestSize()];
            provider.BlockUpdate(buffer, 0, buffer.Length);
            provider.DoFinal(result, 0);
            return BitConverter.ToString(result).Replace("-", ""); ;
        }
        /// <summary>
        /// SHA512ハッシュ
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetHashSHA512(string value, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            var enc = encoding ?? Encoding.UTF8;
            var buffer = enc.GetBytes(value);
            var provider = new Sha512Digest();
            var result = new byte[provider.GetDigestSize()];
            provider.BlockUpdate(buffer, 0, buffer.Length);
            provider.DoFinal(result, 0);
            return BitConverter.ToString(result).Replace("-", ""); ;
        }
        /// <summary>
        /// 比較
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="source">ハッシュ文字列</param>
        /// <returns>ハッシュ化したvalueとsourceが一致すればTrue</returns>
        public static bool CompareHashMD5(string value,string source)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(source)) return false;
            var val=Hash.GetHashMD5(value);
            return val == source ? true : false;
        }
        /// <summary>
        /// 比較
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="source">ハッシュ文字列</param>
        /// <returns>ハッシュ化したvalueとsourceが一致すればTrue</returns>
        public static bool CompareHashSHA256(string value, string source)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(source)) return false;
            var val = Hash.GetHashSHA256(value);
            return val == source ? true : false;
        }
        /// <summary>
        /// 比較
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="source">ハッシュ文字列</param>
        /// <returns>ハッシュ化したvalueとsourceが一致すればTrue</returns>
        public static bool CompareHashSHA512(string value, string source)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(source)) return false;
            var val = Hash.GetHashSHA512(value);
            return val == source ? true : false;
        }
    }
}
