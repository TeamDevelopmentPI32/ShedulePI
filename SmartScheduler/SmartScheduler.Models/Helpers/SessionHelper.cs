using SmartScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SmartScheduler.Models.Helpers
{
    public static class SessionHelper
    {
        private static Dictionary<string, int> Sessions = new Dictionary<string, int>();

        public static string CreateSession(int userId)
        {
            var token = CreateToken(userId);
            Sessions.Add(token, userId);
            return token;
        }

        public static string CreateSession(User user)
        {
            return CreateSession(user.UserId);
        }

        private static string CreateToken(int userId)
        {
            return CalculateMD5Hash($"{userId}:{DateTime.UtcNow}");
        }

        public static int GetSession(string token)
        {
            return Sessions.ContainsKey(token) ? Sessions[token] : -1;
        }

        private static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static void ClearAllSessions()
        {
            Sessions.Clear();
        }

        public static bool ClearSession(string token)
        {
            if (!Sessions.ContainsKey(token)) return false;

            Sessions.Remove(token);
            return true;
        }
    }
}
