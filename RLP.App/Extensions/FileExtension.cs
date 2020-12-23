using System.Linq;

namespace RLP.App.Extensions
{
    public static class FileExtension
    {
        public static bool? ExtensionIsValid(this string extension, string extensions)
        {
            if (string.IsNullOrEmpty(extension))
                return false;


            return extensions.Split(',')?.ToList().Exists(e => e.Equals(extension));
        }

        public static bool AutomaticReloadCache(this int skip, int maxNext)
        {
            return (skip <= 0 || skip + maxNext >= 90);
        }
    }
}
