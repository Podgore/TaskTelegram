using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Common.DTO
{
    public class PersonDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }

    }
}
