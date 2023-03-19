﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Contracts
{
    public interface IComplientDataAccessLayer
    {
        Task<ComplientBox> AddComplient(ComplientBox comp);
        Task<List<ComplientBox>> RequestedByEmployee(string employyeID);
        Task<List<ComplientBox>> GetAllComlient();
        Task<Dictionary<string, int>> NumberOfComplaintsRaised(string employyeID);
        Task<ComplientBox> GetByComplientId(string Complientid);
        Task<IEnumerable<ComplientBox>> GetAllEmployees();
    }
}
