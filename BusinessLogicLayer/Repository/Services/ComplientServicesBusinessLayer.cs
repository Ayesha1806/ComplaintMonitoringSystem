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
    public class ComplientServicesBusinessLayer : IComplientBusinessLayercs
    {
        private readonly IComplientDataAccessLayer _services;
        public ComplientServicesBusinessLayer(IComplientDataAccessLayer services)
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

        public Task<ComplientBox> GetByComplientId(int Complientid)
        {
            return _services.GetByComplientId(Complientid);
        }

        public Task<List<ComplientBox>> RequestedByEmployee(string employyeID)
        {
            return _services.RequestedByEmployee(employyeID);
        }
    }
}
