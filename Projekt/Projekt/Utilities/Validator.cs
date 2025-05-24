namespace Projekt.Utilities
{
    public class Validator
    {
        public static void ValidateString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", paramName);
            }
        }
        public static void ValidateEnumValue<T>(T value, string varName) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentOutOfRangeException(varName, $"{value} is not a valid value for {typeof(T).Name}.");
            }
        }
    }
}
