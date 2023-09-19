using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Globalization;
using System.Web;
using static UnityEngine.UI.Image;

public class Base64Tool
{
    #region 密钥
    //private static string key = "abcd1234";                                   //密钥(长度必须8位以上)   
    #endregion

    #region 使用 给定密钥字符串 加密/解密string
    /// <summary>
    /// 使用给定密钥字符串加密string
    /// </summary>
    /// <param name="original">原始文字</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">字符编码方案</param>
    /// <returns>密文</returns>
    public static string Encrypt(string key, string original) 
    { 
        byte[] buff = System.Text.Encoding.Default.GetBytes(original); 
        byte[] kb = System.Text.Encoding.Default.GetBytes(key);
        return Convert.ToBase64String(Encrypt(buff,kb));     
    }
    /// <summary>
    /// 使用给定密钥字符串解密string
    /// </summary>
    /// <param name="original">密文</param>
    /// <param name="key">密钥</param>
    /// <returns>明文</returns>
    public static string Decrypt(string key, string original)
    {
        return Decrypt(original, key, System.Text.Encoding.Default);
    }

    /// <summary>
    /// 使用给定密钥字符串解密string,返回指定编码方式明文
    /// </summary>
    /// <param name="encrypted">密文</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">字符编码方案</param>
    /// <returns>明文</returns>
    public static string Decrypt(string encrypted, string key, Encoding encoding)
    {
        byte[] buff = Convert.FromBase64String(encrypted);
        byte[] kb = System.Text.Encoding.Default.GetBytes(key);
        return encoding.GetString(Decrypt(buff, kb));
    }

    public static byte[] Encrypt(byte[] original, byte[] key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.Key = MakeMD5(key);
        des.Mode = CipherMode.ECB;

        return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
    }

    /// <summary>
    /// 使用给定密钥解密数据
    /// </summary>
    /// <param name="encrypted">密文</param>
    /// <param name="key">密钥</param>
    /// <returns>明文</returns>
    public static byte[] Decrypt(byte[] encrypted, byte[] key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.Key = MakeMD5(key);
        des.Mode = CipherMode.ECB;

        return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
    }

    public static byte[] MakeMD5(byte[] original)
    {
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyhash = hashmd5.ComputeHash(original);
        hashmd5 = null;
        return keyhash;
    }
    #endregion
}
