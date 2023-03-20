using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Contracts;
using GlobalEntity.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Services
{
    public class ComplaintServicesDataLayer :IComplaintDataAccessLayer
    {
        private readonly ComplaintMonitoringSystemContext _context;
        private readonly ILogger<ComplaintMonitoringSystemContext> _logger;
        public ComplaintServicesDataLayer(ComplaintMonitoringSystemContext context,ILogger<ComplaintMonitoringSystemContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ComplientBox> AddComplient(ComplientBox comp)
        {
            try
            {
                _logger.LogInformation("Complient Raised");
                _logger.LogDebug(comp.EmployeeId, comp.Issue, comp.ComplientRaised, comp.Status,comp.ComplientId,comp.CreatedBy,comp.CreatedDate,comp.ModifiedBy,comp.ModifiedDate);
                int count = NumberOfComplients(comp.EmployeeId);
                string guid = Guid.NewGuid().ToString();
                ComplientBox complient = new ComplientBox()
                {
                    ComplientId = guid,
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
            catch (Exception e)
            {
                _logger.LogError(e.Message,e.InnerException,e.StackTrace);
                throw new BadRequest("Server Error!!!");
            }
        }
        public int NumberOfComplients(string EmployeeID)
        {
            _logger.LogInformation("Count of Complients");
            _logger.LogDebug(EmployeeID);
            try
            {
                int count = 1;
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
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace, e.InnerException);
                throw new ServerError("Something Went Wrong!!!");
            }
           // return Convert.ToInt16($"SELECT Count(EmployeeId) as NumberOfComplaints FROM [dbo].[ComplientBox] WHERE EmployeeId='{EmployeeID}'");
            
        }
        public ComplientBox GetByEmployyeID(string employyeID)
        {
            _logger.LogInformation("Getting Data ById");
            _logger.LogDebug(employyeID);
            try
            {
                var data= _context.ComplientBoxes.FirstOrDefault(x => x.EmployeeId == employyeID);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e.InnerException, e.StackTrace);
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<List<ComplientBox>> GetAllComlient()
        {
            _logger.LogInformation("Getting List Of Records");

            try
            {
                var data= _context.ComplientBoxes.ToListAsync();
                if (data != null)
                {
                    return await data;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace, e.InnerException);
                throw new Exception(e.Message);
            }
                
        }

        public async Task<List<ComplientBox>> RequestedByEmployee(string employyeID)
        {
            _logger.LogInformation("By Employee Id Getting id");
            _logger.LogDebug(employyeID);
            try
            {
                string query = $"select * from Complient where EmployeeId='{employyeID}'";
                var obj = await _context.ComplientBoxes.Where(x => x.EmployeeId == employyeID).ToListAsync();
                if(obj != null)
                {
                    return obj;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace, e.InnerException);
                throw new BadRequest("Something Went Wrong!!!");
            }
        }

        public async Task<ComplientBox> GetByComplientId(string Complientid)
        {
            _logger.LogInformation("Complient Raised");
            try
            {
                if (Complientid!=null)
                {
                    return _context.ComplientBoxes.FirstOrDefault(x => x.ComplientId.Equals(Complientid));
                }
                return null;  
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e.InnerException, e.StackTrace);
                throw new BadRequest("Server Error");
            }
        }

        public async Task<Dictionary<string,int>> NumberOfComplaintsRaised(string employyeID)
        {
            var dictonary = new Dictionary<string, int>();
            var data = _context.ComplientBoxes.ToList();
           
                int count = 0;
                foreach (var complient in data)
                {
                    if (complient.EmployeeId.Equals(employyeID))
                    {
                        count++;
                    }
                }
                dictonary.Add(employyeID, count);
                count = 0;
            
            return dictonary;
            
        }

        public async Task<IEnumerable<ComplientBox>> GetAllEmployees()
        {
            try
            {
                List<ComplientBox> list = new List<ComplientBox>();
                var data = _context.ComplientBoxes.ToList();
               //return data.Union(data);
                //var terms = .Split(' ').ToList();
                foreach (ComplientBox u1 in data)
                {
                    bool duplicatefound = false;
                    foreach (ComplientBox u2 in list)
                        if (u1.EmployeeId == u2.EmployeeId)
                            duplicatefound = true;

                    if (!duplicatefound)
                        list.Add(u1);
                }
                return list.DistinctBy(i => i.EmployeeId).ToList();
            }
            catch (Exception e)
            {
                throw new BadRequest("Server Error!!!");
            }
        }
    }
}
