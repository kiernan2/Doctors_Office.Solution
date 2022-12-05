using Microsoft.AspNetCore.Mvc;
using Doctors_Office.Models;
using System.Collections.Generic;

namespace Doctors_Office.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}