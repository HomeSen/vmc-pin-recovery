using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace VodafoneDecryptor
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine();

            string fileName = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), @"Vodafone\Vodafone Mobile Broadband\UserData\MobileBroadbandProfile.xml");
            if (args.Length > 0)
                fileName = args[0];

            if (File.Exists(fileName) == false)
                Console.WriteLine("[-] Can't find Vodafone Mobile Broadband UserData.");
            else
                ReadUserData(fileName);

            Console.WriteLine();
            Console.Write("[*] Done! ");
            Console.ReadKey();
        }

        private static void ReadUserData(string fileName)
        {
            try
            {
                Console.WriteLine("[*] Reading UserData file...");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNodeList nodes = xmlDoc.SelectNodes("/MobileConnectProfile/Wans/Wan");

                if (nodes == null)
                {
                    Console.WriteLine("[-] No saved WAN profiles found!");
                    return;
                }
                else if (nodes.Count == 0)
                {
                    Console.WriteLine("[-] No saved WAN profiles found!");
                    return;
                }


                foreach (XmlNode node in nodes)
                {
                    string pinCode = node.SelectSingleNode("PinCode").InnerText;
                    if (pinCode.Trim().Length == 0)
                    {
                        Console.WriteLine("[*] Found WAN profile, but no saved PIN! Proceeding with next profile...");
                        continue;
                    }

                    string decrypted = Decrypt(pinCode);
                    Console.WriteLine("[+] PIN: " + decrypted);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static string Encrypt(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] numArray = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in numArray)
                stringBuilder.Append(num.ToString() + ";");
            
            return ((object)stringBuilder).ToString();
        }

        private static string Decrypt(string encrypted)
        {
            string[] strArray = encrypted.Split(new char[1] { ';' });
            byte[] encryptedData = new byte[strArray.Length];
            int result = 0;
            int index = 0;
            foreach (string s1 in strArray)
            {
                if (int.TryParse(s1, out result))
                    encryptedData[index] = (byte)result;
                index++;
            }
            byte[] bytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(bytes);
        }
    }

}
