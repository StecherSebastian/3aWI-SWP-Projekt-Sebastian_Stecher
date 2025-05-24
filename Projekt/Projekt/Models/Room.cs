using Projekt.Utilities;

namespace Projekt.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; private set; }
        public int Size { get; private set; }
        public Room(string name, int size)
        {
            Validator.ValidateString(name, nameof(name));
            Name = name;
            Size = size;
        }
        public void ChangeName(string name)
        {
            Validator.ValidateString(name, nameof(name));
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
