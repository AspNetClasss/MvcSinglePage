namespace SinglePage_Sample.ApplicationServices.Dtos.PersonDtos
{
    public class PutPersonServiceDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        //public string NationalCode { get; set; }
    }
}
