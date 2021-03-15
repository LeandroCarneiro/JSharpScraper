using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetScraper.Repository;

namespace VetScraper.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VetClinicsController : ControllerBase
    {
        private readonly IVetClinicRepository repository;
        public VetClinicsController(IVetClinicRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClinics(int skip = 0, int limit = 50)
        {
            try
            {
                return Ok(await repository.ListClinics(skip, limit));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
