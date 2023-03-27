using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Contracts
{
    public interface IComplaintDataAccessLayer
    {
        Task<ComplientBox> AddComplient(ComplientBox comp);
        Task<List<ComplientBox>> RequestedByEmployee(string employyeID);
        Task<List<ComplientBox>> GetAllComlient();
        Task<ComplientBox> GetByComplientId(string Complientid);
        Task<IEnumerable<ComplientBox>> GetAllEmployees();
    }
}
