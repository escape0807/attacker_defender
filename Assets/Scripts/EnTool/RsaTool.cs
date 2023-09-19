using System;
using System.Text;
using System.Security.Cryptography;

public class RsaTool
{

    public static string Rsa(string PublicRsaKey, string source)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(PublicRsaKey);
        var cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(source), true);
        return Convert.ToBase64String(cipherbytes);
    }

    /// <summary>
    /// RSA解密
    /// </summary>
    public static string UnRsa(string PrivateRsaKey, string source)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(PrivateRsaKey);
        var cipherbytes = rsa.Decrypt(Convert.FromBase64String(source), true);
        return Encoding.UTF8.GetString(cipherbytes);
    }
}
