using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
    }
}
