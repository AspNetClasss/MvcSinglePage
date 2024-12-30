using Microsoft.AspNetCore.Mvc;

using SinglePage_Sample.ApplicationServices.Contracts;
using SinglePage_Sample.ApplicationServices.Dtos.PersonDtos;

namespace SinglePage.Sample01.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        #region [- ctor -]
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        #endregion

        #region [- Index() -]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region [- GetAll() -]
        public async Task<IActionResult> GetAll()
        {
            Guard_PersonService();
            var getAllResponse = await _personService.GetAll();
            var response = getAllResponse.Value.GetPersonServiceDtos;
            return Json(response);
        }
        #endregion

        #region [- Get() -]
        public async Task<IActionResult> Get(GetPersonServiceDto dto)
        {
            Guard_PersonService();
            var getResponse = await _personService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return Json("NotFound");
            }
            return Json(response);
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostPersonServiceDto dto)
        {
            Guard_PersonService();
            var postedDto = new GetPersonServiceDto() { Email = dto.Email };
            var getResponse = await _personService.Get(postedDto);

            if (ModelState.IsValid && getResponse.Value is null)
            {
                var postResponse = await _personService.Post(dto);
                return postResponse.IsSuccessful ? Ok() : BadRequest();
            }
            else if (ModelState.IsValid && getResponse.Value is not null)
            {
                return Conflict(dto);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
        #region [- Put() -]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutPersonServiceDto dto)
        {
            Guard_PersonService();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getResponse = await _personService.Get(new GetPersonServiceDto { Email = dto.Email });

            if (getResponse.Value is null)
            {
                return NotFound($"Person with email {dto.Email} not found.");
            }

            var putResponse = await _personService.Put(dto);

            return putResponse.IsSuccessful ? Ok() : BadRequest("Failed to update the person.");
        }
        #endregion
        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePersonServiceDto dto)
        {
           
            Guard_PersonService();

           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the person exists
            var getResponse = await _personService.Get(new GetPersonServiceDto { Id = dto.Id });

            if (getResponse.Value is null)
            {
                return NotFound($"Person with ID {dto.Id} not found.");
            }

            // Attempt to delete the person
            var deleteResponse = await _personService.Delete(dto);

            // Return appropriate response based on the delete operation's result
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion



        #region [- PersonServiceGuard() -]
        private ObjectResult Guard_PersonService()
        {
            if (_personService is null)
            {
                return Problem($" {nameof(_personService)} is null.");
            }

            return null;
        }
        #endregion
    }
}
