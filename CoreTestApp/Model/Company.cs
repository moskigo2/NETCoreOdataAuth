using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Model
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Address { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [JsonIgnore]
        ICollection<Worker> Workers { get; set; }
    }
}
