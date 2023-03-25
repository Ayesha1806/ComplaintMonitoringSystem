using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class ComplaintsOfEmployee
    {
        public int Id { get; set; }
        public string LoginId { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string ComplaintId { get; set; } = null!;
        public int ComplaintCount { get; set; }
        public string Resolutation { get; set; } = null!;
        public bool ActiveFlag { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual AspNetUser Login { get; set; } = null!;
    }
}
