using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/location")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class LocationController : ControllerBase
    {
        #region Declared

        readonly ILocationRepository _locationRepository;
        #endregion

        #region Initialization
        public LocationController(ILocationRepository locationRepository)
        {

            _locationRepository = locationRepository;
        }
        #endregion

        [HttpGet("countries")]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _locationRepository.GetAllContries());

        }

        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces(Guid countryId)
        {
            return Ok(await _locationRepository.GetProvinces(countryId));

        }

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts(Guid countryId, int provinceId)
        {
            return Ok(await _locationRepository.GetDistricts(countryId, provinceId));

        }

        [HttpGet("wards")]
        public async Task<IActionResult> GetWards(Guid countryId, int provinceId, int districtId)
        {
            return Ok(await _locationRepository.GetWards(countryId, provinceId, districtId));

        }

    }
}
