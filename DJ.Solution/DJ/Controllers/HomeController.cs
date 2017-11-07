using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DJ.Models;
using MySql.Data.MySqlClient;

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
          try
          {
              DateTime start = Convert.ToDateTime(Request.Form["event-start"]);
              DateTime end = Convert.ToDateTime(Request.Form["event-start"]);
              // Convert datetimes to mysql format.
              Event newEvent = new Event(start, end, Request.Form["event-name"], Request.Form["venue-name"], Request.Form["venue-address"]);
              newEvent.Save();
              return RedirectToAction("Events");
          }
          catch (MySqlException ex)
          {
              return RedirectToAction("Events");
          }
      }

      [HttpGet("/events/past")]
      public ActionResult EventsPast()
      {
          List<Event> pastEvents = Event.GetAllByDate(false);
          return View("Events", pastEvents);
      }

      [HttpGet("/events/upcoming")]
      public ActionResult EventsUpcoming()
      {
          List<Event> upcomingEvents = Event.GetAllByDate();
          return View("Events", upcomingEvents);
      }

    }
}
