using System.Net;

namespace SinglePage_Sample.Frameworks.ResponseFrameworks.Contracts
{
    public interface IResponse<T>
    {
         bool IsSuccessful { get; set; }
         HttpStatusCode  Status { get; set; }
        string? Message { get; set; }
        T? Value { get; set; }

    }
}
