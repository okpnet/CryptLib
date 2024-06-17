// See https://aka.ms/new-console-template for more information
using CryptLib;

Console.WriteLine("Hello, World!");
var md5 = "HashTest".CreatePassword(HashType.MD5);
var sha256 = "HashTest".CreatePassword();
var sha513 = "HashTest".CreatePassword(HashType.SHA512);
System.Diagnostics.Debug.Assert(sha513.ComparePassword("HashTest"));
System.Diagnostics.Debug.Assert(sha513.ComparePassword("hashTest"));