using TaskAPI.Common.DTO;

namespace TaskAPI.Abstractions.Services
{
    public interface IPersonService
    {
        Task<PersonDTO> AddPerson(PersonDTO person);
        Task<bool> DeletePerson(string Name);
        List<PersonDTO> GetPersons();
        Task<PersonDTO?> GetPersonByName(string Name);
        Task<UpdatePersonDTO> UpdatePerson(string Name, UpdatePersonDTO dog);
    }
}
