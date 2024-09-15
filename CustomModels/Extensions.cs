namespace ClownsCRMAPI.CustomModels
{
    public static class Extensions
    {
        // For nullable bool
        public static bool GetValueOrDefault(this bool? value, bool defaultValue = false)
        {
            return value ?? defaultValue;
        }

        // For nullable int
        public static int GetValueOrDefault(this int? value, int defaultValue = 0)
        {
            return value ?? defaultValue;
        }
        public static decimal GetValueOrDefault(this decimal? value, int defaultValue = 0)
        {
            return value ?? defaultValue;
        }

        // For nullable string
        public static string GetValueOrDefault(this string value, string defaultValue = "")
        {
            return value ?? defaultValue;
        }

        // For nullable DateTime
        public static DateTime GetValueOrDefault(this DateTime? value, DateTime defaultValue)
        {
            return value ?? defaultValue;
        }

        // Add more extension methods as needed
    }

}
