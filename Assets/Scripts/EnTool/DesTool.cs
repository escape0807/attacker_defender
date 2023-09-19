using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Globalization;

public class DesTool
{
    #region 密钥
    //private static string key = "abcd1234";                                   //密钥(长度必须8位以上)   
    #endregion

    #region DES加密
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">加密内容</param>
    /// <param name="keyVal">密钥</param>
    /// <param name="ivVal">初始向量</param>
    /// <returns></returns>
    public static string EncryptString(string keyVal, string ivVal, string source)
    { 
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(source);
            var des = new DESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(keyVal.Length > 8 ? keyVal.Substring(0, 8) : keyVal), IV = Encoding.ASCII.GetBytes(ivVal.Length > 8 ? ivVal.Substring(0, 8) : ivVal) };
            var desencrypt = des.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        catch { return "转换出错！"; }
    }
    #endregion

    #region DES解密
    /// <summary>
    /// DES 解密
    /// </summary>
    public static string DecryptString(string keyVal, string ivVal, string source)
    {
        try
        {
            string[] sInput = source.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            var des = new DESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(keyVal.Length > 8 ? keyVal.Substring(0, 8) : keyVal), IV = Encoding.ASCII.GetBytes(ivVal.Length > 8 ? ivVal.Substring(0, 8) : ivVal) };
            var desencrypt = des.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
        catch { return "解密出错！"; }
    }

    #endregion
}
