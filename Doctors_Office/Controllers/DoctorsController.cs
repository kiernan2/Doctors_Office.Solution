using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Doctors_Office.Models;

namespace Doctors_Office.Controllers
  {
    public class DoctorsController : Controller
    {
      private readonly Doctors_OfficeContext _db;

      public DoctorsController(Doctors_OfficeContext db)
      {
        _db = db;
      }

      public ActionResult Index()
      {
        //List<DoctorSpecialty> model = _db.DoctorsSpecialties.ToList();
        List<Doctor> model = _db.Doctors.ToList();
        return View(model);
        // return View(_db.Doctors.ToList());
      }

      public ActionResult Details(int id)
      {
        Doctor thisDoctor = _db.Doctors
          .Include(doctor => doctor.JoinEntities)
            .ThenInclude(join => join.Patient)
          .FirstOrDefault(doctor => doctor.DoctorId == id);
        return View(thisDoctor);
      }

      public ActionResult Create()
      {
        return View();
      }

      [HttpPost]
      public ActionResult Create(Doctor doctor)
      {
        ViewBag.Doctor = doctor;
        _db.Doctors.Add(doctor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Edit(int id)
      {
        Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
        return View(thisDoctor);
      }

      [HttpPost]
      public ActionResult Edit(Doctor doctor)
      {
        _db.Entry(doctor).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Delete(int id)
      {
        Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
        return View(thisDoctor);
      }

      [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
        _db.Doctors.Remove(thisDoctor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      [HttpPost]
      public ActionResult AddSpecialty(Specialty specialty, int doctorId)
      {
        bool duplicate = _db.DoctorsSpecialties.Any(join =>
        join.DoctorId == doctorId && join.SpecialtyId == specialty.SpecialtyId);

        if(doctorId != 0 && !duplicate)
        {
          _db.DoctorsSpecialties.Add(new DoctorSpecialty() { DoctorId = doctorId, SpecialtyId = specialty.SpecialtyId});
        }
        
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult AddSpecialty(int id)
      {
        Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
        ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "Name");
        return View(thisDoctor);
      }
    }
  }