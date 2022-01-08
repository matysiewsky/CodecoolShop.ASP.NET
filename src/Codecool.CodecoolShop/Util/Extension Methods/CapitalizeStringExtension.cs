namespace Codecool.CodecoolShop.Util.Extension_Methods
{
    public static class CapitalizeStringExtension
    {
        public static string Capitalize(this string strToCapitalize) => (string.IsNullOrEmpty(strToCapitalize))
            ? string.Empty
            : char.ToUpper(strToCapitalize[0]) + strToCapitalize[1..];

    }
}