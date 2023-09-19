using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class SHATool 
{
    /// <summary>
    /// SHA1 加密 
    /// </summary>
    /// <param name="content">需要加密字符串</param>
    /// <param name="encode">指定加密编码</param>
    /// <param name="upperOrLower">大小写格式（大写：X2;小写:x2）默认小写</param> 
    public static string SHA1Encrypt(string content, string upperOrLower = "x2")
    {
        try
        {
            var buffer = System.Text.Encoding.Default.GetBytes(content);//用指定编码转为bytes数组
            var data = SHA1.Create().ComputeHash(buffer);
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString(upperOrLower));
            }

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "SHA1加密出错：" + ex.Message;
        }
    }

}
