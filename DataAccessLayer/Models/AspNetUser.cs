﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            ComplaintsOfEmployees = new HashSet<ComplaintsOfEmployee>();
            Roles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [NotMapped]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [NotMapped]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        [NotMapped]
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        [NotMapped]
        public virtual ICollection<ComplaintsOfEmployee> ComplaintsOfEmployees { get; set; }
        [NotMapped]
        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
