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
        public static void ValidateEnumValue<T>(T value, string paramName) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentOutOfRangeException(paramName, $"{value} is not a valid value for {typeof(T).Name}.");
            }
        }
        public static void ValidNumberOfSeats(int size, int seats)
        {
            const int ratio = 75;
            if (seats < 0 || seats > (size * ratio / 100))
            {
                throw new ArgumentOutOfRangeException(nameof(seats), $"Number of seats must be positive and less or equal to {ratio}% of the room size.");
            }
        }
    }
}
