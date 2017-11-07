using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DJ.Models;

namespace DJ.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/events")]
      public ActionResult Events()
      {
        List<Event> allEvents = Event.GetAll();
        return View(allEvents);
      }

      [HttpGet("/events/add")]
      public ActionResult EventForm()
      {
          return View();
      }

    }
}
