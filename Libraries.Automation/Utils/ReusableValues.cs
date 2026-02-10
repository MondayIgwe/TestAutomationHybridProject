namespace Libraries.Automation.Utils
{
    public static class ReusableValues
    {
        public const string BaseApiUrl = "https://simple-books-api.click";
        public const int RandomGenerator = 10;
        private static readonly Random _random = new();

        public static int GenerateRandomNumber(int min = 0, int max = int.MaxValue)
        {
            return _random.Next(min, max);
        }

        public static int GenerateRandomNumber(int max)
        {
            return _random.Next(max);
        }

        public static string GenerateRandomNumericString(int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be positive", nameof(length));
            
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = (char)('0' + _random.Next(10));
            }
            return new string(result);
        }
    }
}
