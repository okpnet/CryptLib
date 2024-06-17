using System.Text;

namespace CryptLib
{
    /// <summary>
    /// 文字列拡張クラス
    /// </summary>
    public static class StringExt
    {
        /// <summary>
        /// Split拡張
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(this string value, int count)
        {
            if (count == 0 || count >= value.Length) yield break;
            var splitcount = value.Length / count;
            for (int i = 0; i < splitcount; i++)
            {
                yield return value.Substring(i * count, count);
            }
        }
        /// <summary>
        /// バイト配列を16進数文字列に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexStr(this IEnumerable<byte> value)
        {
            if (value is null || !value.Any()) return string.Empty;
            return BitConverter.ToString(value.ToArray()).Replace("-", "");
        }
        /// <summary>
        /// バイト配列を16進数文字列に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] value)
        {
            if (value is null || 0 >= value.Length) return string.Empty;
            return BitConverter.ToString(value).Replace("-", "");
        }
        /// <summary>
        /// バイト配列をBase64に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64Str(this IEnumerable<byte> value)
        {
            if (value is null || !value.Any()) return string.Empty;
            try
            {
                return Convert.ToBase64String(value.ToArray());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        /// バイト配列をBase64に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64Str(this byte[] value)
        {
            if (value is null || 0 >= value.Length) return string.Empty;
            try
            {
                return Convert.ToBase64String(value);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        public static byte[] ToBase64Byte(this string value)
        {
            if (value is null || 0 >= value.Length) return new byte[0];
            try
            {
                return Convert.FromBase64String(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }
		/// <summary>
		/// Utf8文字列16進数をバイト配列に変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte[] ToBytesFromHexStr(this string value)
        {
            try
            {
                var result = value.Split(2).Select(a => Convert.ToByte(a, 16)).ToArray();
                return result;
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }
		/// <summary>
		/// バイト配列をUtf8文字列16進数に変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToHexStrFromBytes(this byte[] value)
		{
			try
			{
				return Encoding.UTF8.GetString(value);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return string.Empty;
			}
		}
		/// <summary>
		/// Byte配列をUtf8文字列に変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToUtf8Str(this byte[] value)
        {
            try
            {
                return Encoding.UTF8.GetString(value);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        /// 文字列をByte配列へ変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToUtf8Byte(this string value)
        {
            try
            {
                return Encoding.UTF8.GetBytes(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }
        /// <summary>
        /// パスワード変換
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashType"></param>
        /// <returns></returns>
        public static string CreatePassword(this string password,HashType hashType = HashType.SHA256)
        {
            var result= hashType switch
            {
                HashType.MD5 => Hash.GetHashMD5(password),
                HashType.SHA512 => Hash.GetHashSHA512(password),
                _ => Hash.GetHashSHA256(password)
            };
            //System.Diagnostics.Debug.WriteLine($"TYPE:{hashType.ToString()} SOURCE:{password} LEN:{result.Length}");
            return result;
        }
        /// <summary>
        /// パスワードのコンペア
        /// </summary>
        /// <param name="hashSource"></param>
        /// <param name="compareHashSource"></param>
        /// <returns></returns>
        /// <exception cref="CryptLibException"></exception>
        public static bool ComparePassword(this string hashSource, string compareHashSource)
        {
            return hashSource.Length switch
            {
                32 => Hash.CompareHashMD5(compareHashSource, hashSource),
                64 => Hash.CompareHashSHA256(compareHashSource, hashSource),
                128 => Hash.CompareHashSHA512(compareHashSource, hashSource),
                _ => throw new CryptLibException("It source must be either MD5, SHA256, or SHA512.")
            };
        }
    }
}
