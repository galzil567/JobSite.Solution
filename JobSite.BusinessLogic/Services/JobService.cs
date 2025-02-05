using JobSite.Data.Entities;
using JobSite.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSite.BusinessLogic.Services
{
    public class JobService
    {
        public static async Task<List<JobDto>> GetJobsAsync()
        {           
            try
            {
                using (JobSiteDbContext db = new JobSiteDbContext())
                {
                    return await db.Jobs.Select(job => new JobDto
                    {
                        Id = job.Id,
                        Title = job.Title,
                        Type = job.Type,
                        Description = job.Description,
                        Location = job.Location,
                        Salary = job.Salary,
                        CompanyName = job.CompanyName,
                        CompanyDescription = job.CompanyDescription,
                        CompanyEmail = job.CompanyEmail,
                        CompanyPhone = job.CompanyPhone,
                    }).ToListAsync();
                };
            }
            catch (Exception error)
            {
                throw new Exception("Error retrieving jobs", error);
            }
        }

        public static async Task<JobDto> GetJobAsync(int id)
        {
            try
            {
                using (JobSiteDbContext db = new JobSiteDbContext())
                {
                    Job job = await db.Jobs.SingleOrDefaultAsync(j => j.Id == id);
                    JobDto jobDto = new JobDto()
                    {
                        Id = job.Id,
                        Title = job.Title,
                        Type = job.Type,
                        Description = job.Description,
                        Location = job.Location,
                        Salary = job.Salary,
                        CompanyName = job.CompanyName,
                        CompanyDescription = job.CompanyDescription,
                        CompanyEmail = job.CompanyEmail,
                        CompanyPhone = job.CompanyPhone,
                    };
                    return jobDto;
                };
            }
            catch (Exception error)
            {
                throw new Exception("Error retrieving job", error);
            }
        }

        public static async Task<int> AddJobAsync(AddJobDto addJobDto)
        {
            try
            {
                JobSiteDbContext db = new JobSiteDbContext();

                Job newJob = new Job()
                {
                    Title = addJobDto.title,
                    Type = addJobDto.type,
                    Description = addJobDto.description,
                    Location = addJobDto.location,
                    Salary = addJobDto.salary,
                    CompanyName = addJobDto.companyName,
                    CompanyDescription = addJobDto.companyDescription,
                    CompanyEmail = addJobDto.contactEmail,
                    CompanyPhone = addJobDto.contactPhone,
                };
                db.Jobs.Add(newJob);
                await db.SaveChangesAsync();
                return newJob.Id;
            }
            catch (DbUpdateException ex)
            {
                // אם מדובר בשגיאה במסד הנתונים 
                throw new Exception("Database error while adding job", ex);
            }
            catch (Exception ex)
            {
                // שגיאה כללית
                throw new Exception("An unexpected error occurred while adding job", ex);
            }
        }

        public static async Task<bool> DeleteJobAsync(int id)
        {
            try
            {
                JobSiteDbContext db = new JobSiteDbContext();
                Job job = await db.Jobs.SingleOrDefaultAsync(x => x.Id == id);
                if (job == null)
                {
                    return false;
                }
                db.Jobs.Remove(job);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting job", ex);
            }
        }

        public static async Task<bool> UpdateJobAsync(int id, UpdateJobDto updateJobDto)
        {
            JobSiteDbContext db = new JobSiteDbContext();

            Job existingJob = await db.Jobs.SingleOrDefaultAsync(x =>x.Id == id);

            if (existingJob == null)
            {
                return false;
            }

            existingJob.Title = updateJobDto.title;
            existingJob.Type = updateJobDto.type;
            existingJob.Description = updateJobDto.description;
            existingJob.Location = updateJobDto.location;
            existingJob.Salary = updateJobDto.salary;
            existingJob.CompanyDescription = updateJobDto.companyDescription;
            existingJob.CompanyName = updateJobDto.companyName;
            existingJob.CompanyPhone = updateJobDto.contactPhone;
            existingJob.CompanyEmail = updateJobDto.contactEmail;

            await db.SaveChangesAsync();
            return true;

        }
    }
}
