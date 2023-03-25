using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository.Services
{
    public class ComplaintServicesBusinessLayer : IComplaintBusinessLayercs
    {
        private readonly IComplaintDataAccessLayer _services;
        public ComplaintServicesBusinessLayer(IComplaintDataAccessLayer services)
        {
            _services = services;
        }

        public Task<ComplientBox> AddComplient(ComplientBox comp)
        {
            return _services.AddComplient(comp);
        }

        public Task<List<ComplientBox>> GetAllComplients()
        {
            return _services.GetAllComlient();
        }

        public Task<IEnumerable<ComplientBox>> GetAllEmployees()
        {
            return _services.GetAllEmployees();
        }

        public Task<ComplientBox> GetByComplientId(string Complientid)
        {
            return _services.GetByComplientId(Complientid);
        }

        public Task<List<ComplaintsOfEmployee>> GetRecords()
        {
            return _services.GetRecords();
        }

        public Task<Dictionary<string, int>> NumberOfComplaintsRaised(string employyeID)
        {
            return _services.NumberOfComplaintsRaised(employyeID);
        }

        public Task<List<ComplientBox>> RequestedByEmployee(string employyeID)
        {
            return _services.RequestedByEmployee(employyeID);
        }

        public Task<string> Resolution(string complientid)
        { 
            return _services.Resolution(complientid);
        }
    }
}
