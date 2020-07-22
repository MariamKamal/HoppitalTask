using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoppitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HoppitalTask.Controllers
{
    public class PatientController : Controller
    {
        HospitalDBContext HospitalDBContext;
        public PatientController( HospitalDBContext con)
        {
            HospitalDBContext = con;
        }
        public IActionResult PatiientHome(int Id)
        {
            ViewData["userID"] = Id;

            var listOfDoctors = HospitalDBContext.Doctor.ToList();
            var patientData = HospitalDBContext.Patient.SingleOrDefault(p => p.PatientId == Id);
            var Appoinments = HospitalDBContext.Appointment.Where(p => p.PatientId == Id);
            List<Doctor> list=new List<Doctor>();
            foreach (var item in Appoinments)
            {
                var doc= HospitalDBContext.Doctor.Find(item.DocId);

                list.Add(doc); 
            }
            ViewBag.DoctorsData = listOfDoctors;
            ViewBag.Appoinments = Appoinments;
            ViewBag.list = list;


            return View(patientData);
        }


        [HttpGet]
        public IActionResult Appointment( int Id, int IDD)
        {
            Doctor doctor = HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == Id);
            Patient patient = HospitalDBContext.Patient.SingleOrDefault(i => i.PatientId == IDD);
            Appointment appointment = new Appointment()
            {
                PatientId = patient.PatientId,
                DocId = doctor.DocId
            
            };
            ViewData["docName"] = doctor.DocName;
            ViewData["specialization"] = doctor.Specialization;
            ViewData["patientName"] = patient.PartientName;

            return View(appointment);


        }
        [HttpPost]
        public IActionResult Appointment(Appointment appointment)
        {
            Appointment app = new Appointment
            {

               PatientId= appointment.PatientId,
               DocId=appointment.DocId,
               Date=appointment.Date,
               Clinic =appointment.Clinic
            };
            HospitalDBContext.Appointment.Add(app);
            HospitalDBContext.SaveChanges();

            return RedirectToAction("PatiientHome", new RouteValueDictionary(
                                 new { controller = "Patient", action = "PatiientHome", Id = app.PatientId }));

        }

        public IActionResult Delete(int? Id)
        {
            var patient = HospitalDBContext.Patient.SingleOrDefault(i => i.PatientId == Id);

            if (Id != null && patient != null)
            {
                HospitalDBContext.Patient.Remove(patient);
                HospitalDBContext.SaveChanges();
                return RedirectToAction("notFound", "Home");
            }
            return RedirectToAction();

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View(HospitalDBContext.Patient.SingleOrDefault(i => i.PatientId == Id));
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                Patient newPatient = HospitalDBContext.Patient.SingleOrDefault(i => i.PatientId == patient.PatientId);
                newPatient.PartientName = patient.PartientName;
                newPatient.BloodGroub = patient.BloodGroub;
                newPatient.Gender = patient.Gender;
                newPatient.Phone = patient.Phone;
                newPatient.Email = patient.Email;
                HospitalDBContext.SaveChanges();
                return RedirectToAction("PatiientHome", new RouteValueDictionary(
                     new { controller = "Patient", action = "PatiientHome", Id = patient.PatientId }));

            }
            return RedirectToAction();

        }

        public IActionResult  appDetails(int Id)
        {
            
            var app = HospitalDBContext.Appointment.SingleOrDefault(i => i.AppointmentId == Id);
            ViewData["DocName"] = HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == app.DocId).DocName; 
            ViewData["specialization"]= HospitalDBContext.Doctor.SingleOrDefault(i => i.DocId == app.DocId).Specialization;
            ViewData["patientName"] = HospitalDBContext.Patient.SingleOrDefault(i=>i.PatientId==app.PatientId).PartientName;
            return View(app);
        }


    }
}