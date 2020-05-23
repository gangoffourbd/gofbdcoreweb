namespace Gofbd.TestConsole.Feature.Encoding
{
    using System;
    using System.Text;
    public static class Base64EncodingTest
    {
        public static string UnPwToBase64(string email, string password)
        {
            var usernamepassword = $"{email}:{password}";
            var bytes = Encoding.Unicode.GetBytes(usernamepassword);
            return Convert.ToBase64String(bytes);
        }

        public static string UnPwFromBase64(string value)
        {
            var bytes = Convert.FromBase64String(value);
            var unPw = Encoding.Unicode.GetString(bytes);
            var email = unPw.Split(':')[0];
            var password = unPw.Split(':')[1];
            return $"{email}:{password}";
        }
    }
}
