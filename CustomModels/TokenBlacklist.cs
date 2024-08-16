public static class TokenBlacklist
{
    private static readonly HashSet<string> _blacklist = new HashSet<string>();

    public static void AddToBlacklist(string token)
    {
        lock (_blacklist)
        {
            _blacklist.Add(token);
        }
    }

    public static bool IsTokenBlacklisted(string token)
    {
        lock (_blacklist)
        {
            return _blacklist.Contains(token);
        }
    }
}
