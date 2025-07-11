using System;

namespace KP_Mitsura
{
    internal static class Crypt
    {
        public static Boolean Check_Correct(String str)
        {
            Int32 nCorrect = 0;
            if(str == null)
            {
                nCorrect++;
            }
            if (str != null)
            {
                if (str.Length < 5)
                {
                    nCorrect++;
                }
                if (nCorrect == 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!((str[i] >= '0' && str[i] <= '9') ||
                              (str[i] >= 'A' && str[i] <= 'Z') ||
                              (str[i] >= 'a' && str[i] <= 'z') ||
                              (str[i] >= 'А' && str[i] <= 'Я') ||
                              (str[i] >= 'а' && str[i] <= 'я') ||
                              (str[i] == 'Ё' || str[i] == 'ё') || 
                              (str[i] == ' '))) nCorrect++;
                    }
                }
                if (nCorrect == 0)
                {
                    for (int i = 2; i < str.Length; i++)
                    {
                        if ((str[i].Equals(str[i - 1])) && (str[i].Equals(str[i - 2]))) nCorrect++;
                    }
                }
            }
            if (nCorrect == 0) return false;
            else return true;
        }
        public static String Base64Encrypt(String str)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decrypt(String str)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
