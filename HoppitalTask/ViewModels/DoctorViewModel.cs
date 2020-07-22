using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoppitalTask.ViewModels
{
    public class DoctorViewModel:viewModel
    {
     

        public int DocId { get; set; }
        public string DocName { get; set; }
        public string Specialization { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public IFormFile Photo { get; set; }
        public string Password { get; set; }

      
    }
}
