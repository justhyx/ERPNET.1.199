﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace ERPPlugIn.Class
{
    public class ClassDES
    {
        public static string SetKey(string text, string type)
        {
            if (type == "md5")
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToLower();
            }
            else
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "sha1").ToLower();
            }
        }
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="text">要加密的文本</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="text">解密文本</param>
        /// <param name="sKey">解密密钥</param>
        /// <returns></returns>
        public static string Decrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

    }
}