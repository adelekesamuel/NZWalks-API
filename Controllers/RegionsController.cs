using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;
using NZWalksAPI.Models.DTO;
using AutoMapper;
using System.Runtime.InteropServices;
using System.Xml.Linq;

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

        //get all region endpoint
        [HttpGet]
        public async Task<IActionResult >GetAllRegionsAsync()
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

            var regions = await regionRepository.GetAllAsync();

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

        //Get region by uuid endpoint
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
           var DomainRegion = await  regionRepository.GetAsync(id);

            if (DomainRegion == null)
            {
                return NotFound("This region does not exist");
            }

           var regionDTO = mapper.Map<Models.DTO.Region>(DomainRegion);

            return Ok(regionDTO);
        }

        //Add region endpoint
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Requst(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //Pass details to Repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region form the database
            var region = await regionRepository.DeleteAsync(id);

            //if null return not found
            if (region == null)
            {
                return NotFound();
            }

            //convert response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return ok response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            //update region using repository
            await regionRepository.UpdateAsync(id, region);

            //if null then notfound
            if (region == null)
            {
                return NotFound(); 
            }

            //Convert domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return Ok response
            return Ok();

        }
    }
}