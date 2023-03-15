using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Contracts;
using GlobalEntity.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Services
{
    public class ComplientServicesDataLayer :IComplientDataAccessLayer
    {
        private readonly ComplaintMonitoringSystemContext _context;
        private readonly ILogger<ComplaintMonitoringSystemContext> _logger;
        public ComplientServicesDataLayer(ComplaintMonitoringSystemContext context,ILogger<ComplaintMonitoringSystemContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ComplientBox> AddComplient(ComplientBox comp)
        {
            try
            {
                _logger.LogInformation("Admin Register");
                _logger.LogDebug(comp.EmployeeId, comp.Issue, comp.ComplientRaised, comp.Status);
                int count = NumberOfComplients(comp.EmployeeId);
                ComplientBox complient = new ComplientBox()
                {
                    ComplientId = 1001,
                    EmployeeId = comp.EmployeeId,
                    Issue = comp.Issue,
                    Status = "Submited",
                    ComplientRaised = count,
                    Resolution="UnderProcessing",
                    ActiveFlag = true,
                    CreatedBy = "Employee",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedDate = null
                };
                _context.ComplientBoxes.Add(complient);
                _context.SaveChanges();
                return complient;
            }
            catch (BadRequest ex)
            {
                _logger.LogError("Server Error");
                throw new BadRequest("Server Error!!!");
            }
        }
        public int NumberOfComplients(string EmployeeID)
        {
            int count = 0;
            if (GetByEmployyeID(EmployeeID) == null)
            {
                return 1;
            }
            else
            {
                var data = _context.ComplientBoxes.ToList();
                foreach (var complient in data)
                {
                    if (complient.EmployeeId.Equals(EmployeeID))
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public ComplientBox GetByEmployyeID(string employyeID)
        {
            return _context.ComplientBoxes.FirstOrDefault(x => x.EmployeeId == employyeID);
        }

        public async Task<List<ComplientBox>> GetAllComlient()
        {
            return await _context.ComplientBoxes.ToListAsync();
                
        }

        public async Task<List<ComplientBox>> RequestedByEmployee(string employyeID)
        {
            try
            {
                string query = $"select * from Complient where EmployeeId='{employyeID}'";
                var obj = await _context.ComplientBoxes.Where(x => x.EmployeeId == employyeID).ToListAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new BadRequest("Something Went Wrong!!!");
            }
        }

        public async Task<ComplientBox> GetByComplientId(int Complientid)
        {
            try
            {
                if (Complientid > 0)
                {
                    return _context.ComplientBoxes.FirstOrDefault(x => x.ComplientId == Complientid);
                }
                return null;  
            }
            catch(Exception e)
            {
                throw new BadRequest("Server Error");
            }
        }
    }
}
