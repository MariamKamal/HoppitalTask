using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using HoppitalTask.Models;
using HoppitalTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HoppitalTask.Controllers
{
    public class LoginController : Controller
    {
        HospitalDBContext hospitalDBContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public LoginController(HospitalDBContext dBContext, IWebHostEnvironment hostEnvironment)
        {
            hospitalDBContext = dBContext;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult index(string email, string pass)
        {


            var Patientuser = hospitalDBContext.Patient.Where(p => p.Email == email).Where(p => p.Password == pass).FirstOrDefault();
            var doctoruser = hospitalDBContext.Doctor.Where(p => p.Email == email).FirstOrDefault();

            if (Patientuser != null)
            {
                return RedirectToAction("PatiientHome", new RouteValueDictionary(
                 new { controller = "Patient", action = "PatiientHome", Id = Patientuser.PatientId }));}


            if (doctoruser != null)
            {
                return RedirectToAction("DoctorHome", new RouteValueDictionary(
                new { controller = "Doctor", action = "DoctorHome", Id = doctoruser.DocId }));
            }
            else
                return RedirectToAction("notFound", "Home");
        }


        [HttpGet]
        public IActionResult RegisterDoctor()
        {

            return View("R");
        }
        [HttpPost]
        public IActionResult RegisterDoctor(DoctorViewModel doctor)
        {
            string uniqueFileName = UploadedFile(doctor);
            if (ModelState.IsValid)
            {
                Doctor newdoctor = new Doctor
                {
                    DocName = doctor.DocName,
                    Phone = doctor.Phone,
                    Gender = doctor.Gender,
                    Photo = uniqueFileName,
                    Specialization = doctor.Specialization,
                    Email = doctor.Email,
                    Password = doctor.Password




                };
                hospitalDBContext.Doctor.Add(newdoctor);
                hospitalDBContext.SaveChanges();
                return RedirectToAction("DoctorHome", new RouteValueDictionary(
           new { controller = "Doctor", action = "DoctorHome", Id = newdoctor.DocId }));
            }
            else
                return RedirectToAction();

        }
        public IActionResult RegisterPatient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterPatient(PatientViewModel patient) 
        {
            string uniqueFileName = UploadedFile(patient);
            if (ModelState.IsValid)
            {
                Patient newPatient = new Patient
                {
                    PartientName = patient.PartientName,
                    Phone = patient.Phone,
                    Gender = patient.Gender,
                    Photo = uniqueFileName,
                      BloodGroub= patient.BloodGroub,
                    Email = patient.Email,
                    Password = patient.Password




                };
                hospitalDBContext.Patient.Add(newPatient);
                hospitalDBContext.SaveChanges();

                return RedirectToAction("PatiientHome", new RouteValueDictionary(
           new { controller = "Patient", action = "PatiientHome", Id = newPatient.PatientId }));
            }
            else
                return RedirectToAction();


        }



        private string UploadedFile(viewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }






    }
}