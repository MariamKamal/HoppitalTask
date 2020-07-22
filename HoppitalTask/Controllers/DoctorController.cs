using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoppitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HoppitalTask.Controllers
{
    public class DoctorController : Controller
    { HospitalDBContext HospitalDBContext;
        public DoctorController(HospitalDBContext hospitalDBContext)
        {
            HospitalDBContext = hospitalDBContext;
        }
        public IActionResult DoctorHome(int Id)
        {
            var doc = HospitalDBContext.Doctor.SingleOrDefault(s => s.DocId == Id);
            
            return View(doc);
        }

        public IActionResult Delete(int? Id)
        {
            var doc = HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == Id);

            if (Id != null && doc != null)
            {
                HospitalDBContext.Doctor.Remove(doc);
                HospitalDBContext.SaveChanges();
                return RedirectToAction("notFound", "Home");
            }
            return RedirectToAction();

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View(HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == Id));
        }
        [HttpPost]
        public IActionResult Edit(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                Doctor newdoc = HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == doc.DocId);
                newdoc.DocName = doc.DocName;
                newdoc.Specialization = doc.Specialization;
                newdoc.Gender = doc.Gender;
                newdoc.Phone = doc.Phone;
                newdoc.Email = doc.Email;
                HospitalDBContext.SaveChanges();
                return RedirectToAction("DoctorHome", new RouteValueDictionary(
                     new { controller = "Doctor", action = "DoctorHome", Id = doc.DocId }));

            }
            return RedirectToAction();

    }
    }
}