using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoppitalTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoppitalTask.Controllers
{
    public class ClinicController : Controller
    {
        HospitalDBContext HospitalDBContext;
        public ClinicController(HospitalDBContext hospitalDBContext)
        {
            HospitalDBContext = hospitalDBContext;
        }
        // GET: Clinic
        public ActionResult Index()
        {
            return View(HospitalDBContext.Clinic.ToList());
        }

        // GET: Clinic/Details/5
        public ActionResult Details(int id)
        {
            return View(HospitalDBContext.Clinic.SingleOrDefault(i =>i.ClinicId==id));
        }

        // GET: Clinic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinic clinic)
        {
            try
            {
                HospitalDBContext.Clinic.Add(clinic);
                HospitalDBContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Clinic/Edit/5
        public ActionResult Edit(int id)
        {
            return View(HospitalDBContext.Clinic.SingleOrDefault(i=>i.ClinicId==id));
        }

        // POST: Clinic/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Clinic clinic)
        {
            try
            {
             Clinic newClinic=   HospitalDBContext.Clinic.SingleOrDefault(i => i.ClinicId == clinic.ClinicId);

                newClinic.ClinicName = clinic.ClinicName;
                newClinic.Location = clinic.Location;
                newClinic.Type = clinic.Type;
                HospitalDBContext.SaveChanges();

                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

     

        // POST: Clinic/Delete/5
       
      
        public ActionResult Delete(int id)
        {
          
                HospitalDBContext.Clinic.Remove(HospitalDBContext.Clinic.SingleOrDefault(i => i.ClinicId == id));
                // TODO: Add delete logic here
                HospitalDBContext.SaveChanges();

                return RedirectToAction(nameof(Index));
          
        }
    }
}