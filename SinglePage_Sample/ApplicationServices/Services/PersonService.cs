
using SinglePage_Sample.ApplicationServices.Contracts;
using SinglePage_Sample.ApplicationServices.Dtos.PersonDtos;
using SinglePage_Sample.Frameworks.ResponseFrameworks;
using SinglePage_Sample.Frameworks.ResponseFrameworks.Contracts;
using SinglePage_Sample.Models.DomainModels.PersonAggregates;
using SinglePage_Sample.Models.DomainModels.Services.Contracts;
using System.Net;

namespace SinglePage.Sample01.ApplicationServices.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        #region [- ctor -]
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IResponse<GetAllPersonServiceDto>> GetAll()
        {
            var selectAllResponse = await _personRepository.SelectAll();

            if (selectAllResponse is null)
            {
                return new Response<GetAllPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllPersonDto = new GetAllPersonServiceDto() { GetPersonServiceDtos = new List<GetPersonServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var personDto = new GetPersonServiceDto()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email
                };
                getAllPersonDto.GetPersonServiceDtos.Add(personDto);
            }

            var response = new Response<GetAllPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllPersonDto);
            return response;
        }
        #endregion

        #region [- Get() -]
        public async Task<IResponse<GetPersonServiceDto>> Get(GetPersonServiceDto dto)
        {
            var person = new Person()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            var selectResponse = await _personRepository.Select(person);

            if (selectResponse is null)
            {
                return new Response<GetPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectResponse.IsSuccessful)
            {
                return new Response<GetPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            var getPersonServiceDto = new GetPersonServiceDto()
            {
                Id = selectResponse.Value.Id,
                FirstName = selectResponse.Value.FirstName,
                LastName = selectResponse.Value.LastName,
                Email = selectResponse.Value.Email
            };
            var response = new Response<GetPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getPersonServiceDto);
            return response;
        }
        #endregion

        #region [- Post() -]
        public async Task<IResponse<PostPersonServiceDto>> Post(PostPersonServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var postedPerson = new Person()
            {
                Id = new Guid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            var insertedResponse = await _personRepository.Insert(postedPerson);

            if (!insertedResponse.IsSuccessful)
            {
                return new Response<PostPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
        #endregion

      

      

        public async Task<IResponse<PutPersonServiceDto>> Put(PutPersonServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var putedPerson = new Person()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,

            };
            var insertedResponse = await _personRepository.Update(putedPerson);

            if (!insertedResponse.IsSuccessful)
            {
                return new Response<PutPersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<PutPersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }

        public async Task<IResponse<DeletePersonServiceDto>> Delete(DeletePersonServiceDto dto)
        {

            if (dto is null)
            {
                return new Response<DeletePersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var personToDelete = new Person()
            {
                Id = dto.Id
            };

            var deleteResponse = await _personRepository.Delete(personToDelete);

            if (!deleteResponse.IsSuccessful)
            {
                return new Response<DeletePersonServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            var response = new Response<DeletePersonServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }
    }
}


