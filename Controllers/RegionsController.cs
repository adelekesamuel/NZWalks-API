using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("regions")]
     
    public class RegionsController : Controller
    {
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Victoria Island",
                    Code = "VI",
                    Area = 227755,
                    Lat = -1.8822,
                    Long = 299.88,
                    Population = 5000000,
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Ikoyi",
                    Code = "EKY",
                    Area = 234567,
                    Lat = -9.8976,
                    Long = 199.89,
                    Population = 2000000,
                }
            };

            return Ok(regions);
        }
    }
}