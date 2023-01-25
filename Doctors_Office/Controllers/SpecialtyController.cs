using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Doctors_Office.Models;

namespace Doctors_Office.Controllers
{
  public class SpecialtiesController : Controller
  {
    private readonly Doctors_OfficeContext _db;

    public SpecialtiesController(Doctors_OfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Specialties.ToList());
    }

    public ActionResult Details(int id)
    {
      Specialty thisSpecialty = _db.Specialties
        .Include(specialty => specialty.JoinEntities)
          .ThenInclude(join => join.Doctor)
        .FirstOrDefault(specialty => specialty.SpecialtyId ==  id);
      return View(thisSpecialty);
    }

    public ActionResult Create()
    {
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Specialty specialty, int doctorId)
    {
      _db.Specialties.Add(specialty);
      _db.SaveChanges();
      if (doctorId != 0)
      {
        _db.DoctorsSpecialties.Add(new DoctorSpecialty() { DoctorId = doctorId, SpecialtyId = specialty.SpecialtyId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index"); 
    }

    public ActionResult Edit(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisSpecialty);
    }

    [HttpPost]
    public ActionResult Edit(Specialty specialty, int doctorId)
    {
      bool duplicate = _db.DoctorsSpecialties.Any(join =>
      join.DoctorId == doctorId && join.SpecialtyId == specialty.SpecialtyId);

      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsSpecialties.Add(new DoctorSpecialty() {DoctorId = doctorId, SpecialtyId = specialty.SpecialtyId });
      }

      _db.Entry(specialty).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult AddDoctor(Specialty specialty, int doctorId)
    {
      bool duplicate = _db.DoctorsSpecialties.Any(join => 
      join.DoctorId == doctorId && join.SpecialtyId == specialty.SpecialtyId);

      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsSpecialties.Add(new DoctorSpecialty() { DoctorId = doctorId, SpecialtyId = specialty.SpecialtyId });
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDoctor(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisSpecialty);
    }

    [HttpPost]
    public ActionResult CreateDoctor(Specialty specialty, int doctorId)
    {
      bool duplicate = _db.DoctorsSpecialties.Any(join =>
      join.DoctorId == doctorId && join.SpecialtyId == specialty.SpecialtyId);

      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsSpecialties.Add(new DoctorSpecialty() { DoctorId = doctorId, SpecialtyId = specialty.SpecialtyId });
        Doctor newDoctor = new Doctor() { DoctorId = doctorId };
        ViewBag.Doctor = newDoctor;
        _db.Doctors.Add(newDoctor);
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult CreateDoctor(int id)
    {
      ViewBag.Doctor = (_db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id));
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View();
    }
  }
}