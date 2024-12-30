using SinglePage_Sample.ApplicationServices.Dtos.PersonDtos;

namespace SinglePage_Sample.ApplicationServices.Contracts
{
    public interface IPersonService:IService<PostPersonServiceDto,GetPersonServiceDto,GetAllPersonServiceDto,PutPersonServiceDto,DeletePersonServiceDto>
    {
    }
}
