namespace Projekt.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string? Name { get; private set; } = null!;
        public int? Size { get; private set; } = null!;
        protected Room(string? name, int? size)
        {
            Name = name;
            Size = size;
        }
        protected Room()
        {
            Name = "Default Room";
        }
        public void ChangeName(string name)
        {
            Utilities.Validator.ValidateString(name, nameof(name));
            Name = name;
        }
        public void ChangeSize(int size)
        {
            ValidateSize(size);
            Size = size;
        }
        private void ValidateSize(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Room size must be positive.");
            }
        }
    }
}
