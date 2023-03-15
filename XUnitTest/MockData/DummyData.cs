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
            }
        };
        public async static Task<List<ComplientBox>> GetAllRecords()
        {
            return data;
        }
        public async static Task<List<ComplientBox>> NoContext()
        {
            return null;
        }
    }
}
