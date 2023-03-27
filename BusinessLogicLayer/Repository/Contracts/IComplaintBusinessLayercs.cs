using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository.Contracts
{
    public interface IComplaintBusinessLayercs
    {
        Task<ComplientBox> AddComplient(ComplientBox comp);
        Task<List<ComplientBox>> RequestedByEmployee(string employyeID);
        Task<List<ComplientBox>> GetAllComplients();
        Task<ComplientBox> GetByComplientId(string Complientid);
        Task<IEnumerable<ComplientBox>> GetAllEmployees();

    }
}
