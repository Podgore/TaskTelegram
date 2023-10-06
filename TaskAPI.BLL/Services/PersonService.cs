using AutoMapper;
using TaskAPI.Abstractions.EF;
using TaskAPI.Abstractions.Services;
using TaskAPI.Common.DTO;
using TaskAPI.Entities;

namespace TaskAPI.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepo<Person, string> _repository;

        private readonly IMapper _mapper;

        public PersonService(IRepo<Person, string> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PersonDTO> AddPerson(PersonDTO person)
        {
            Person entity = _mapper.Map<Person>(person);

            if (await _repository.Table.FindAsync(entity.Id) != null)
                throw new InvalidOperationException("Entity with such key already exists in database");

            await _repository.AddAsync(entity);

            return _mapper.Map<PersonDTO>(entity);
        }

        public async Task<bool> DeletePerson(string Name)
        {
            var person = await _repository.FindAsync(Name);
            return person != null && await _repository.DeleteAsync(person) > 0; throw new NotImplementedException();
        }


        public async Task<PersonDTO?> GetPersonByName(string Name)
        {
            Person? person = await _repository.FindAsync(Name);
            return person != null ? _mapper.Map<PersonDTO>(person) : null;
        }

        public List<PersonDTO> GetPersons()
        {
            return _mapper.Map<IEnumerable<PersonDTO>>(_repository.GetAll()).ToList();
        }

        public async Task<UpdatePersonDTO> UpdatePerson(string Name, UpdatePersonDTO person)
        {
            var entity = await _repository.FindAsync(Name) ?? throw new KeyNotFoundException($"Unable to find entity with such key {Name}");

            _mapper.Map(person, entity);

            await _repository.UpdateAsync(entity);

            return _mapper.Map<UpdatePersonDTO>(entity);
        }
    }
}
