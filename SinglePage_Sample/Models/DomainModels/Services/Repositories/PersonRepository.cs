using Microsoft.EntityFrameworkCore;

using System.Net;

using SinglePage_Sample.Frameworks.ResponseFrameworks.Contracts;
using SinglePage_Sample.Models.DomainModels.Services.Contracts;
using SinglePage_Sample.Models.DomainModels.PersonAggregates;
using SinglePage_Sample.Frameworks.ResponseFrameworks;
using SinglePage_Sample.Migrations;
using Person = SinglePage_Sample.Models.DomainModels.PersonAggregates.Person;

namespace SinglePage.Sample01.Models.Services.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ProjectDbContext _projectDbContext;

        #region [- ctor -]
        public PersonRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }
        #endregion

        #region [- Insert() -]
        public async Task<IResponse<Person>> Insert(Person model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _projectDbContext.AddAsync(model);
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- SelectAll() -]
        public async Task<IResponse<IEnumerable<Person>>> SelectAll()
        {
            try
            {
                var persons = await _projectDbContext.Person.AsNoTracking().ToListAsync();
                return persons is null ?
                    new Response<IEnumerable<Person>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Person>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, persons);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [- Select() -]
        public async Task<IResponse<Person>> Select(Person person)
        {
            try
            {
                var responseValue = new Person();
                if (person.Id.ToString() != "")
                {
                    //responseValue = await _projectDbContext.Person.FindAsync(person.Email);
                    responseValue = await _projectDbContext.Person.Where(c => c.Email == person.Email).SingleOrDefaultAsync();
                }
                else
                {
                    responseValue = await _projectDbContext.Person.FindAsync(person.Id);
                }
                return responseValue is null ?
                     new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public async Task<IResponse<Person>> Update(Person model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                var person = _projectDbContext.Update(model);
                await _projectDbContext.AddAsync(model);
                var respons = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return respons;



            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public  async Task<IResponse<Person>> Delete(Person model)
        {
            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                var p = _projectDbContext.Remove(model);

                await _projectDbContext.SaveChangesAsync();
                var respons = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return respons;

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
           
        }
    }
}


