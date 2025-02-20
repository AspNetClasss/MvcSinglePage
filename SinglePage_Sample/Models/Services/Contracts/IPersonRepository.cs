using SinglePage_Sample.Models.DomainModels.PersonAggregates;

namespace SinglePage_Sample.Models.Services.Contracts
{
    public interface IPersonRepository : IRepository<Person, IEnumerable<Person>>
    {

    }
}
