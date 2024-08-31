using System.Security.Cryptography;

namespace Melnikov.Blazor.Clean.Infrastructure.Utilities;

public static class Base32
{
    private const string Base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    public static string GenerateBase32()
    {
        return string.Create(32, 0, (buffer, _) =>
        {
            Span<byte> span = stackalloc byte[20];
            RandomNumberGenerator.Fill(span);
            var index = 0;
            var offset = 0;
            while (offset < span.Length)
            {
                byte a;
                byte b;
                byte c;
                byte d;
                byte e;
                byte f;
                byte g;
                byte h;
                var nextGroup = GetNextGroup(span, ref offset, out a, out b, out c, out d, out e, out f, out g, out h);

                buffer[index + 7] = nextGroup >= 8 ? Base32Chars[h] : '=';
                buffer[index + 6] = nextGroup >= 7 ? Base32Chars[g] : '=';
                buffer[index + 5] = nextGroup >= 6 ? Base32Chars[f] : '=';
                buffer[index + 4] = nextGroup >= 5 ? Base32Chars[e] : '=';
                buffer[index + 3] = nextGroup >= 4 ? Base32Chars[d] : '=';
                buffer[index + 2] = nextGroup >= 3 ? Base32Chars[c] : '=';
                buffer[index + 1] = nextGroup >= 2 ? Base32Chars[b] : '=';
                buffer[index] = nextGroup >= 1 ? Base32Chars[a] : '=';
                
                index += 8;
            }
        });
    }

    private static int GetNextGroup(Span<byte> input, ref int offset,
        out byte a, out byte b, out byte c, out byte d, out byte e, out byte f, out byte g, out byte h)
    {
        var nextGroup = (input.Length - offset) switch
        {
            1 => 2,
            2 => 4,
            3 => 5,
            4 => 7,
            _ => 8
        };

        var num1 = offset < input.Length ? input[offset++] : 0U;
        var num2 = offset < input.Length ? input[offset++] : 0U;
        var num3 = offset < input.Length ? input[offset++] : 0U;
        var num4 = offset < input.Length ? input[offset++] : 0U;
        var num5 = offset < input.Length ? input[offset++] : 0U;

        a = (byte)(num1 >> 3);
        b = (byte)((uint)(((int)num1 & 7) << 2) | num2 >> 6);
        c = (byte)(num2 >> 1 & 31U);
        d = (byte)((uint)(((int)num2 & 1) << 4) | num3 >> 4);
        e = (byte)((uint)(((int)num3 & 15) << 1) | num4 >> 7);
        f = (byte)(num4 >> 2 & 31U);
        g = (byte)((uint)(((int)num4 & 3) << 3) | num5 >> 5);
        h = (byte)(num5 & 31U);

        return nextGroup;
    }
}