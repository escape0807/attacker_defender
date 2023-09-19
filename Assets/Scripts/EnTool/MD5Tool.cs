using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Globalization;
using System.Web;

public class MD5Tool
{
    #region 密钥
    //private static string key = "abcd1234";                                   //密钥(长度必须8位以上)   
    #endregion

    #region MD5加密
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="source">加密内容</param>
    /// <param name="keyVal">密钥</param>
    /// <param name="ivVal">初始向量</param>
    /// <returns></returns>
    /// 
    public static string EncryptString(string sKey, string pToEncrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }
    #endregion

    #region MD5解密
    /// <summary>
    /// MD5 解密
    /// </summary>
    public static string DecryptString(string sKey, string pToDecrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }

        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        StringBuilder ret = new StringBuilder();

        return System.Text.Encoding.Default.GetString(ms.ToArray());
    }

    #endregion
}
