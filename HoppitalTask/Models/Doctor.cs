using System;
using System.Collections.Generic;

namespace HoppitalTask.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Clinic = new HashSet<Clinic>();
            PatientDoctor = new HashSet<PatientDoctor>();
        }

        public int DocId { get; set; }
        public string DocName { get; set; }
        public string Specialization { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Clinic> Clinic { get; set; }
        public virtual ICollection<PatientDoctor> PatientDoctor { get; set; }
    }
}
