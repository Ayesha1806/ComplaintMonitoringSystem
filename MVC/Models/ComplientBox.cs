using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public partial class ComplientBox
    {
        public int ComplientId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public int ComplientRaised { get; set; }
        public string Status { get; set; } = null!;
        public string Resolution { get; set; } = null!;
        public bool ActiveFlag { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
