using System;
using System.Text;
using System.Security.Cryptography;

namespace UtilityCollection.Encrypt
{
    /// <summary> 
    /// RSA加密解密及RSA签名和验证
    /// </summary> 
    public class RsaCryption
    {
        private const string PrivateKey = "<RSAKeyValue><Modulus>rhERrmQ3/vLXVy8qAg2C519mheswMSCDWnAnmVSwTQsjRgnUk9tn5yJu6P75Kg9yIFbCJlbjqpGsuqYFtJ4VrAVy/MgFDMNOvnXFftffVn0ynknexKxGlwUn9qWmahct3xEsoAjb3mKUBpUUxnLOuHaZjK72NVF+jEjX/t/zhQ0=</Modulus><Exponent>AQAB</Exponent><P>1eFeXvjb28g+kJfopQHB6aleg1rC+nuOTeMydxez5xqsAO0KSNHGmv7N80yxkecWQ5NS52PEcriSDJPZ1f0FZw==</P><Q>0FiFf7aU8JirhnfasOimQP3a0RgHreGAr0EdTFPB5u5xvEJ6bIBo7L1ahixH3NlPGbIkXTUTqiLGBfJ99WbFaw==</Q><DP>l7eTsvkDNMe6IeWwYQR7Ip5Dbhg/AWIOExAcZ0CIHGLeKpX7WqZ8JMylGXaI67+qGmtyPrOV0e89ovBqcRJX9w==</DP><DQ>IUC/re6aPvxfBAtFIE9BmcXqkszfDOWdAFvILVKA9DbCeGWz3HVySba/KAMRRTJ56YQBQc8i4FjEelaFvBE3GQ==</DQ><InverseQ>pO/9UdJV8tCtjD2KTR+h5VblvAG2Vcza1w4Zh8ghbl1H2+n6W2XjBu7UqFsUiNKcVao/44vgTsSh9Hfh9FLRWg==</InverseQ><D>GX9ml6UWjsIDyUGfZa2U/096NSO+a3PXyeej5VICgUagZCIMgZwiHDlvBbJTzVV14kbTKcqQjuvH4Y9wRoThp5NLSdrkL3R8Yh99f3oJ5t8wjBlnN7ha/RIvdsKs4BvWxnsHGHmkMXjdJivlJqLxdjq/lmN+SajsPzm1vEPGygE=</D></RSAKeyValue>";

        private const string PublicKey = "<RSAKeyValue><Modulus>rhERrmQ3/vLXVy8qAg2C519mheswMSCDWnAnmVSwTQsjRgnUk9tn5yJu6P75Kg9yIFbCJlbjqpGsuqYFtJ4VrAVy/MgFDMNOvnXFftffVn0ynknexKxGlwUn9qWmahct3xEsoAjb3mKUBpUUxnLOuHaZjK72NVF+jEjX/t/zhQ0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        #region RSA 加密解密

        #region RSA 的密钥产生

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RsaKey(out string xmlKeys, out string xmlPublicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region RSA的加密函数
        //############################################################################## 
        //RSA 方式加密 
        //说明KEY必须是XML的行式,返回的是字符串 
        //在有一点需要说明！！该加密方式有 长度 限制的！！ 
        //############################################################################## 

        /// <summary>
        /// RSA的加密函数 使用内部Key
        /// </summary>
        /// <param name="encryptString">需要加密的字符串</param>
        /// <returns></returns>
        public static string RsaEncrypt(string encryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PublicKey);
            byte[] plainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
            byte[] cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            string result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }

        //RSA的加密函数  string
        public string RsaEncrypt(string xmlPublicKey, string encryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            byte[] plainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
            byte[] cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            string result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }
        //RSA的加密函数 byte[]
        public string RsaEncrypt(string xmlPublicKey, byte[] encryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            byte[] cypherTextBArray = rsa.Encrypt(encryptString, false);
            string result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }
        #endregion

        #region RSA的解密函数

        /// <summary>
        /// RSA的加密函数 使用内部Key
        /// </summary>
        /// <param name="decryptString">需要解密的字符串</param>
        /// <returns></returns>
        public static string RsaDecrypt(string decryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);
            byte[] plainTextBArray = Convert.FromBase64String(decryptString);
            byte[] dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            string result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;

        }

        //RSA的解密函数  string
        public string RsaDecrypt(string xmlPrivateKey, string decryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            byte[] plainTextBArray = Convert.FromBase64String(decryptString);
            byte[] dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            string result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;

        }

        //RSA的解密函数  byte
        public string RsaDecrypt(string xmlPrivateKey, byte[] decryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            byte[] dypherTextBArray = rsa.Decrypt(decryptString, false);
            string result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;

        }
        #endregion

        #endregion

        #region RSA数字签名

        #region 获取Hash描述表
        //获取Hash描述表 
        public bool GetHash(string source, ref byte[] hashData)
        {
            //从字符串中取得Hash描述 
            HashAlgorithm md5 = HashAlgorithm.Create("MD5");
            byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(source);
            if (md5 == null)
            {
                return false;
            }
            hashData = md5.ComputeHash(buffer);
            return true;
        }

        //获取Hash描述表 
        public bool GetHash(string source, ref string hashData)
        {
            //从字符串中取得Hash描述 
            HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(source);
            if (md5 == null)
            {
                return false;
            }
            byte[] hashDataTemp = md5.ComputeHash(buffer);
            hashData = Convert.ToBase64String(hashDataTemp);
            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] hashData)
        {
            if (hashData == null) throw new ArgumentNullException("hashData");

            //从文件中取得Hash描述 
            HashAlgorithm md5 = HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //从文件中取得Hash描述 
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(HashData);

            return true;

        }
        #endregion

        #region RSA签名
        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] EncryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            byte[] HashbyteSignature;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] HashbyteSignature;
            byte[] EncryptedSignatureData;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }
        #endregion

        #region RSA 签名验证

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {

            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;
            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion


        #endregion

    }
}
