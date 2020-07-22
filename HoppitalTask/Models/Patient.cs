using System;
using System.Collections.Generic;

namespace HoppitalTask.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
            PatientDoctor = new HashSet<PatientDoctor>();
        }

        public int PatientId { get; set; }
        public string PartientName { get; set; }
        public string BloodGroub { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? DocId { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<PatientDoctor> PatientDoctor { get; set; }
    }
}
