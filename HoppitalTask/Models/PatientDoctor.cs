using System;
using System.Collections.Generic;

namespace HoppitalTask.Models
{
    public partial class PatientDoctor
    {
        public int DocId { get; set; }
        public int PatientId { get; set; }

        public virtual Doctor Doc { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
