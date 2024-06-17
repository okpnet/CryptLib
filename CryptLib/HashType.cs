using System;
using System.Collections.Generic;
using System.Text;

namespace CryptLib
{
    /// <summary>
    /// ハッシュのタイプ
    /// </summary>
    public enum HashType
    {
        MD5,SHA256,SHA512
    }
    /// <summary>
    /// HashType拡張
    /// </summary>
    public static class HashTypeExt
    {
        public static string ToString(this HashType type)
        {
            return type switch
            {
                HashType.MD5 => "MD5",
                HashType.SHA256 => "SHA256",
                HashType.SHA512 => "SHA512",
                _ => throw new NotImplementedException($"{type.ToString()} HashType is not implement in 'ToString' method.")
            };
        }
    }
}
