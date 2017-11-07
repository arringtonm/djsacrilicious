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

      [HttpPost("/events/add")]
      public ActionResult EventAdd()
      {
          DateTime start = Convert.ToDateTime(Request.Form["event-start"]);
          Console.WriteLine(start.GetType());
          DateTime end = Convert.ToDateTime(Request.Form["event-start"]);
          // Convert datetimes to mysql format.
          Event newEvent = new Event(start, end, Request.Form["event-name"], Request.Form["venue-name"], Request.Form["venue-address"]);
          newEvent.Save();
          Console.WriteLine("you got here: " + Event.GetAll().Count);
          return RedirectToAction("Events");
      }

    }
}
