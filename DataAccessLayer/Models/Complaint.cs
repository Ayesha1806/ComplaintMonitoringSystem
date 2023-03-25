using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Complaint
    {
        public int SlNo { get; set; }
        public string ComplaintId { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public string Resolutation { get; set; } = null!;
        public bool Activeflag { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
