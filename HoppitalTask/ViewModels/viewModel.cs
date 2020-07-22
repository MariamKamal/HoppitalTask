using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoppitalTask.ViewModels
{
    public interface viewModel
    {
        public IFormFile Photo { get; set; }

    }
}
