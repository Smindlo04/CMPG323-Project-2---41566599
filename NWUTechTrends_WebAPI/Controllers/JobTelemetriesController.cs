using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWUTechTrends_WebAPI.Models;

namespace NWUTechTrends_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class JobTelemetriesController : ControllerBase
    {
        private readonly NwutechTrendsMdfContext _context;

        public JobTelemetriesController(NwutechTrendsMdfContext context)
        {
            _context = context;
        }


        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public int ID { get; set; }
        public string ProccesID { get; set; }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetAllJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetryByID(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost] 
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            var existingJobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (existingJobTelemetry == null)
            {
                return NotFound();
            }

            existingJobTelemetry.ProccesId = jobTelemetry.ProccesId;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }

        public class SavingsDto
        {
            public Guid ClientID { get; set; }
            public Guid ProjectId { get; set; }
            public decimal TotalTimeSaved { get; set; }
            public decimal TotalCostSaved { get; set; }
        }

        private decimal CalculateCost(int totalTimeInSeconds)
        {
            const decimal costPerSecond = 0.5m;
            return totalTimeInSeconds * costPerSecond;
        }

        [HttpGet("projectId")]
        public async Task<IActionResult> GetSavingsPrId(Guid projectId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries.Where(jt => jt.ProjectId == projectId && jt.EntryDate >= startDate && jt.EntryDate <= endDate).ToListAsync();

            var totalTimeSaved = telemetryData.Sum(jt => jt.HumanTime ?? 0); 
            var totalCostSaved = CalculateCost(totalTimeSaved); 

            var savings = new SavingsDto
            {
                ProjectId = projectId,
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(savings);
        }



        [HttpGet("clientID")]
        public async Task<IActionResult> GetSavingsClId(Guid clientId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries.Where(jt => ClientId == clientId && jt.EntryDate >= startDate && jt.EntryDate <= endDate).ToListAsync();

            var totalTimeSaved = telemetryData.Sum(jt => jt.HumanTime ?? 0); 
            var totalCostSaved = CalculateCost(totalTimeSaved); 

            var savings = new SavingsDto
            {
                //ClientId = clientId,
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(savings);
        }


    }



    

}
