using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Model
{
    public class WorkerViewModel
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
