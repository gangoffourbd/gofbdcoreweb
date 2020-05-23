using System;

namespace Gofbd.Core.Infrastructure.ExtensionMetthod
{
    public static class StringExtensionMethod
    {
        public static bool IsValidCredentialFormat(this string value)
        {
            //Format ~username~|~password~
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;
            if (!value.StartsWith("~")) return false;
            if (!value.EndsWith("~")) return false;
            var index = value.IndexOf('|');
            if (value[index - 1] != '~' || value[index + 1] != '~')
                return false;
            if (value.Split("~|~", StringSplitOptions.RemoveEmptyEntries).Length == 2)
                return true;
            return false;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}

