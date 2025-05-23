namespace Projekt.Models
{
    public class Room
    {
        public int ID { get; set; }
        private string _Name;
        public string Name { get { return _Name; } }
        private int _Size;
        public int Size { get { return _Size; } }
        public Room(string name, int size)
        {
            _Name = name;
            _Size = size;
        }
        public void ChangeName(string name)
        {
            _Name = name;
        }
        public void ChangeSize(int size)
        {
            if (size > 0)
            {
                _Size = size;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Room size must be positive.");
            }
        }
    }
}
