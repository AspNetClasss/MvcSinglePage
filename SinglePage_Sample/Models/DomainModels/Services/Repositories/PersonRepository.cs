using Microsoft.EntityFrameworkCore;
using SinglePage.Sample01.Models;
using SinglePage_Sample.Frameworks.ResponseFrameworks;
using SinglePage_Sample.Frameworks.ResponseFrameworks.Contracts;
using SinglePage_Sample.Models.DomainModels.PersonAggregates;
using SinglePage_Sample.Models.DomainModels.Services.Contracts;
using System.Net;

namespace SinglePage_Sample.Models.DomainModels.Services.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ProjectDbContext _projectDbContext;

        public PersonRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }
        //vorodish person ke esmesh model dadim 
        #region [-Insert()-]
        public async Task<IResponse<Person>> Insert(Person model)
        {

            try
            {
                if (model is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _projectDbContext.AddAsync(model);//serach
                var respons = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, model);
                return respons;
            }
            catch (Exception)
            {
                throw;
            }


        }
        #endregion
        #region [-SelectAll()-]
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
        public async Task<IResponse<Person>> Select (Person person)
        {
            try
            {
                var responseValue=new Person(); 
                if (person.Id.ToString() != "") //Ask
                {
                    responseValue = await _projectDbContext.Person.Where(c=>c.Email==person.Email).SingleOrDefaultAsync(); //Ask
                }
                else
                {
                    responseValue = await _projectDbContext.Person.FindAsync(person.Id);
                }
                return responseValue is null ?
                    new Response<Person>(false,HttpStatusCode.UnprocessableContent,ResponseMessages.NullInput, null) :
                    new Response<Person>(true,HttpStatusCode.OK,ResponseMessages.SuccessfullOperation, person);
            }
            catch (Exception)
            {
                throw; 
            }     
        }


    }
}

