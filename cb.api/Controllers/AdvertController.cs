
using Amazon.CloudWatch;
using cb.api.Entities;
using cb.api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private IAdvertRepo _advertRepo;
        //readonly
        //private IAmazonCloudWatch _amazonCloudWatch;
        readonly
        private ILogger _logger;

        public AdvertController(IAdvertRepo advertRepo, ILogger<AdvertController> logger)
        {
            _advertRepo = advertRepo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                List<Advert> adverts = await _advertRepo.FindAll();

                //Write to cloudwatch
                onAdvertProcessed("Adverts fetched: " + adverts.Count, null);

                return Ok(adverts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                //Write to cloudwatch
                onAdvertProcessed("Error retrieving advert list from the database ( " + ex.Message + " )", ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving objects from the database");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                return Ok(await _advertRepo.FindById(id));
            }
            catch (Exception ex)
            {
                onAdvertProcessed("Error retrieving object from the database", null);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving object from the database");
            }
        }
        /// <summary>
        /// Write to cloudwatch
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
       private void onAdvertProcessed(string message, Exception? ex)
        {
            //Write to cloudwatch
            _logger.LogInformation(message, ex);
        }
    }
}
