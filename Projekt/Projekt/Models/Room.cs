using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Room
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Length(2, 50, ErrorMessage = "Name has to be at least 3 Characters long up to a maximum of 50.")]
        public string Name { get; private set; }
        [Required(ErrorMessage = "Size is required.")]
        [Range(20, 100, ErrorMessage = "Room size has to be between 20 and 100.")]
        public int Size { get; private set; }
        public Room(string name, int size)
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
