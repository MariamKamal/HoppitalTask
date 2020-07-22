using System;
using System.Collections.Generic;

namespace HoppitalTask.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int? DocId { get; set; }
        public int? PatientId { get; set; }
        public string Date { get; set; }
        public string Clinic { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
