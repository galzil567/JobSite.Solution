using JobSite.BusinessLogic.Services;
using JobSite.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobSite.WebAPI.Controllers
{
    public class JobsController : ApiController
    {
        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<JobDto> jobs = await JobService.GetJobsAsync();
                if (jobs == null || jobs.Count== 0)
                {
                    return NotFound();
                }
                return Ok(jobs);
            }
            catch (Exception error)
            {
                return InternalServerError(error);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                JobDto jobDto = await JobService.GetJobAsync(id);
                if (jobDto == null)
                {
                    return NotFound();
                }
                return Ok(jobDto);
            }
            catch (Exception error)
            {
                return InternalServerError(error);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] AddJobDto addJobDto)
        {
            if (addJobDto == null)
            {
                return BadRequest("Job data is required.");
            }
            try
            { 
                int newJobId = await JobService.AddJobAsync(addJobDto);
                return Ok(newJobId);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody] UpdateJobDto updateJobDto)
        {
            if (updateJobDto == null)
            {
                return BadRequest("Job data is required.");
            }

            try
            {
                bool updated = await JobService.UpdateJobAsync(id, updateJobDto);
                if (!updated)
                {
                    return NotFound();
                }
                return Ok("Job updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            bool success = await JobService.DeleteJobAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}