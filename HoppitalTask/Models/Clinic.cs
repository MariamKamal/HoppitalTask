using System;
using System.Collections.Generic;

namespace HoppitalTask.Models
{
    public partial class Clinic
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int? DocId { get; set; }

        public virtual Doctor Doc { get; set; }
    }
}
