namespace IVRDemo.Models;

public static class DtmfKeys
{
    public const string One = "1";
    public const string Two = "2";
    public const string Three = "3";
    public const string Back = "9";

    public static bool IsValid(string? key) => key is One or Two or Three;
}
