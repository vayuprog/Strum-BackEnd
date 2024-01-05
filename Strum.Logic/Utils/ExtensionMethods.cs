using System.Globalization;

namespace Strum.Logic.Utils;

public static class ExtensionMethods
{
    public static string GetEuroFormat(this DateTime dt)
    {
        return $"{dt:dd/MM/yyyy HH:mm:ss}";
    }
}