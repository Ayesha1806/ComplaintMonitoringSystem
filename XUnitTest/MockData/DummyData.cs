using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.MockData
{
    public class DummyData
    {
       public static List<ComplientBox> data = new List<ComplientBox>()
        {
            new ComplientBox()
            {
                ComplientId=1001,
                ComplientRaised=1,
                Issue="desk is not clean",
                EmployeeId="MLI1135",
                Resolution="UnderProcessing",
                Status="Submited",
                ActiveFlag=true,
                CreatedBy="Ayesha",
                CreatedDate=DateTime.Now,
                ModifiedBy=null,
                ModifiedDate=null
            },
             new ComplientBox()
            {
                ComplientId=1002,
                ComplientRaised=5,
                Issue="desk is not clean",
                EmployeeId="MLI1135",
                Resolution="UnderProcessing",
                Status="Submited",
                ActiveFlag=true,
                CreatedBy="Ayesha",
                CreatedDate=DateTime.Now,
                ModifiedBy=null,
                ModifiedDate=null
            },
             new ComplientBox()
            {
                ComplientId=1002,
                ComplientRaised=5,
                Issue="desk is not clean",
                EmployeeId="MLI1135",
                Resolution="UnderProcessing",
                Status="Submited",
                ActiveFlag=true,
                CreatedBy="Ayesha",
                CreatedDate=DateTime.Now,
                ModifiedBy=null,
                ModifiedDate=null
            }
        };
        public static List<ComplientBox> GetComplaints()
        {
            return new List<ComplientBox>
            {
                new ComplientBox
                {
                    EmployeeId="MLI1135",
                    Issue="Issue with system",
                    ComplientId=1001,
                    ComplientRaised=1,
                    Resolution="Processing",
                    Status="sublited",
                    ActiveFlag=true,
                    CreatedBy="Employee",
                    CreatedDate=DateTime.Now,
                }
            };
        }
        public async static Task<List<ComplientBox>> GetAllRecords()
        {
            return data;
        }
        public async static Task<List<ComplientBox>> NoContext()
        {
            return null;
        }
        public async static Task<ComplientBox> GetComplaintById(int id)
        {
            //List<ComplientBox> comp =  GetComplaints();
            var Data= data.FirstOrDefault(x=>x.ComplientId==id);
            if (Data != null)
            {
                return Data;
            }
            return null;
        }
        public async static Task<List<ComplientBox>> GetByEmployeeId(string id)
        {
            var Data=data.Where(x=>x.EmployeeId==id);
            if (Data != null)
            {
                return Data.ToList();
            }
            return null;

        }
        public async static Task<ComplientBox> AddComplaint(ComplientBox comp)
        {
            return new ComplientBox
            {
                EmployeeId = "MLI1135",
                Issue = "Issue with laptop",
                Status = "Submited",
                ActiveFlag = true,
                ComplientId = 1002,
                ComplientRaised = 2,
                Resolution = "Processing",
                CreatedBy = "Employee",
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null
            };
        }
    }
}
