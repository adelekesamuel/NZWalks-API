using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;
using NZWalksAPI.Models.DTO;
using AutoMapper;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("regions")]
     
    public class RegionsController : Controller
    {
        private readonly iRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(iRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //*********This is to hardcode data to display

            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Victoria Island",
            //        Code = "VI",
            //        Area = 227755,
            //        Lat = -1.8822,
            //        Long = 299.88,
            //        Population = 5000000,
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Ikoyi",
            //        Code = "EKY",
            //        Area = 234567,
            //        Lat = -9.8976,
            //        Long = 199.89,
            //        Population = 2000000,
            //    }
            //};


            //******If data is to be fetched from the database using DTO standard.

            var regions = regionRepository.GetAll();

            //return DTO regions

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);

        }
    }
}