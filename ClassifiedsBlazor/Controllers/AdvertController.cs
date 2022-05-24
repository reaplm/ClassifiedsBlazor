using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private IAdvertRepo _advertRepo;

        public AdvertController(IAdvertRepo advertRepo)
        {
            _advertRepo = advertRepo;
        }
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                return Ok(await _advertRepo.FindAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving objects from the database");
            }
        }
        [HttpGet]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                return Ok(await _advertRepo.FindById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving object from the database");
            }
        }
    }
}
