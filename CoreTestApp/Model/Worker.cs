using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Model
{
    public class Worker: IdentityUser
    {
        
        [Required]
        public string Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        //public WorkerRole Role { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
