﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class _3DesTool
{/*
    private const string sKey = "qJzGEh6hESZDVJeCnFPGuxzaiB7NLQM5"; //32位
    private const string sIV = "andyliu1234=";
    */
    //构造一个对称算法
  //  public SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
    #region 加密解密函数
    /// <summary>
    /// 字符串的加密
    /// </summary>
    /// <param name="Value">要加密的字符串</param>
    /// <param name="sKey">密钥，必须32位</param>
    /// <param name="sIV">向量，必须是12个字符</param>
    /// <returns>加密后的字符串</returns>
    public static string EncryptString(string sKey, string sIV, string Value)
    {
        SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
        try
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            //指定加密的运算模式
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //获取或设置加密算法的填充模式
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
            byt = Encoding.UTF8.GetBytes(Value);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ("Error in Encrypting " + ex.Message);
        }
    }
    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="Value">加密后的字符串</param>
    /// <param name="sKey">密钥，必须32位</param>
    /// <param name="sIV">向量，必须是12个字符</param>
    /// <returns>解密后的字符串</returns>
    public static string DecryptString(string sKey, string sIV, string Value)
    {
        SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
        try
        {
            ICryptoTransform ct;//加密转换运算
            MemoryStream ms;//内存流
            CryptoStream cs;//数据流连接到数据加密转换的流
            byte[] byt;
            //将3DES的密钥转换成byte
            mCSP.Key = Convert.FromBase64String(sKey);
            //将3DES的向量转换成byte
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//创建对称解密对象
            byt = Convert.FromBase64String(Value);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ("Error in Decrypting " + ex.Message);
        }
    }
    #endregion
}


