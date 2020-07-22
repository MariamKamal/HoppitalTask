using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoppitalTask.ViewModels
{
    public class PatientViewModel:viewModel
    {
        public string PartientName { get; set; }
        public string BloodGroub { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
    }
}
