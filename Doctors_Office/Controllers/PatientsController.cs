using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Doctors_Office.Models;

namespace Doctors_Office.Controllers
{
  public class PatientsController : Controller
  {
    private readonly Doctors_OfficeContext _db;

    public PatientsController(Doctors_OfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patients.ToList());
    }

    public ActionResult Details(int id)
    {
      Patient thisPatient = _db.Patients
        .Include(patient => patient.JoinEntities)
          .ThenInclude(join => join.Doctor)
        .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    public ActionResult Create()
    {
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View();
    }

      [HttpPost]
    public ActionResult Create(Patient patient, int doctorId)
    {
      _db.Patients.Add(patient);
      _db.SaveChanges();
      if (doctorId != 0)
      {
        _db.DoctorsPatients.Add(new DoctorPatient() { DoctorId = doctorId, PatientId = patient.PatientId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult Edit(Patient patient, int doctorId)
    {
      bool duplicate = _db.DoctorsPatients.Any(join =>
      join.DoctorId == doctorId && join.PatientId == patient.PatientId);

      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsPatients.Add(new DoctorPatient() {DoctorId = doctorId, PatientId = patient.PatientId });
      }

      _db.Entry(patient).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatient(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      ViewBag.PatientId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult AddPatient(Patient patient, int doctorId)
    {
      bool duplicate = _db.DoctorsPatients.Any(join =>
      join.DoctorId == doctorId && join.PatientId == patient.PatientId);
    
      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsPatients.Add(new DoctorPatient() { DoctorId = doctorId, PatientId = patient.PatientId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteDoctor(int joinId)
    {
      DoctorPatient joinEntry = _db.DoctorsPatients.FirstOrDefault(entry => entry.DoctorPatientId == joinId);
      _db.DoctorsPatients.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int doctorId)
    {
      bool duplicate = _db.DoctorsPatients.Any(join =>
      join.DoctorId == doctorId && join.PatientId == patient.PatientId);
      
      if (doctorId != 0 && !duplicate)
      {
        _db.DoctorsPatients.Add(new DoctorPatient() { DoctorId = doctorId, PatientId = patient.PatientId});
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult AddDoctor(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisPatient);
    }
  }
}