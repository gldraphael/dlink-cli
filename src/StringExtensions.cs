namespace Dlink.Cli
{
    public static class StringExtensions
    {
        public static bool Is(this string s1, string s2)
        {
            s1 = s1.ToLowerInvariant();
            s2 = s2.ToLowerInvariant();

            return s1 == s2 || s1 == $"--{s2}";
        }
    }
}
