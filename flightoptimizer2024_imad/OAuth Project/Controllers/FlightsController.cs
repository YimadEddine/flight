using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;
using System.Diagnostics;

namespace OAuth_Project.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightsService;

        public FlightsController(IFlightService flightsService)
        {
            this._flightsService = flightsService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            try
            {
                List<Flight> result = await _flightsService.GetAllAvailableFlights();
                if(result.Count == 0) { return NoContent(); }
                return Ok(result);
            }catch(Exception ex)
            {
                Debug.WriteLine("Flights:GetFights: ", ex.Message); 
                return StatusCode(500, "An error occurred while processing the request.");

            }    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight(int id)
    {
        try
        {
            Flight result = await _flightsService.GetFlightById(id);
                if(result== null) { return StatusCode(200, "No flight with Id " + id); }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Flights:GetFightByID: ", ex.Message);
            return StatusCode(500, "An error occurred while processing the request.");

        }
    }


    [HttpGet("test")]
        public IActionResult test()
        {

            if (!User.Identity.IsAuthenticated) return Ok(new { msg = "no auth" });
                return Ok(new { hehe = "xd" });
            
           
        }
    }
}

