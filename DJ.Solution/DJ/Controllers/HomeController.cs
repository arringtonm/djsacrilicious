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

      List<Event> allEvents = Event.GetAllOneMonthBefore();
      return View(allEvents);
    }

    [HttpGet("/events/add")]
    public ActionResult EventForm()
    {
      Dictionary<string,object> model = new Dictionary<string, object>{};
      List<Event> allEvents = Event.GetAll();
      model.Add("all-events", allEvents);
      model.Add("error", 0);
      return View(model);
    }

    [HttpPost("/events/add")]
    public ActionResult EventAdd()
    {
      DateTime start = Convert.ToDateTime(Request.Form["event-start"]);
      DateTime end = Convert.ToDateTime(Request.Form["event-start"]);
      Event newEvent = new Event(start, end, Request.Form["event-name"], Request.Form["venue-name"], Request.Form["venue-address"]);
      try
      {
        //Checks that input start time and end time are valid.
        if (start.Subtract(end).TotalHours < 1)
        {
          throw new InvalidStartOrEndException();
        }
        newEvent.Save();
        return RedirectToAction("Events");
      }
      catch (InvalidStartOrEndException ex)
      {
        Dictionary<string,object> model = new Dictionary<string, object>{};
        List<Event> allEvents = Event.GetAll();
        model.Add("all-events", allEvents);
        model.Add("error", 1);
        return View("EventForm", model);
      }
      catch (MySqlException ex)
      {
        Dictionary<string,object> model = new Dictionary<string, object>{};
        List<Event> allEvents = Event.GetAll();
        model.Add("all-events", allEvents);
        model.Add("error", 2);
        return View("EventForm", model);
      }
    }

    // Go to page with events to edit.
    [HttpGet("/events/edit")]
    public ActionResult EventsEdit()
    {
      Dictionary<string,object> model = new Dictionary<string, object>{};
      List<Event> allEvents = Event.GetAll();
      model.Add("all-events", allEvents);
      // model.Add("selected-event", null);
      model.Add("error", 0);
      return View("EventEdit",model);
    }

    // // Go to page with events
    // [HttpGet("/events/edit/{id}")]
    // public ActionResult EventEdit(int id)
    // {
    //   Dictionary<string,object> model = new Dictionary<string, object>{};
    //   List<Event> allEvents = Event.GetAll();
    //   model.Add("all-events", allEvents);
    //   model.Add("selected-event", null);
    //   model.Add("error", 0);
    //   return View(model);
    // }

    // Go to page with events
    // [HttpGet("/events/edit/{id}")]
    // public ActionResult EventEdit(int id)
    // {
    //   Dictionary<string,object> model = new Dictionary<string, object>{};
    //   List<Event> allEvents = Event.GetAll();
    //   model.Add("all-events", allEvents);
    //   model.Add("error", 0);
    //   return View(model);
    // }

    [HttpPost("/events/edit/{id}")]
    public ActionResult EventEdit(int id)
    {
      DateTime start = Convert.ToDateTime(Request.Form["event-start"]);
      DateTime end = Convert.ToDateTime(Request.Form["event-start"]);
      Event selectedEvent = new Event(start, end, Request.Form["event-name"], Request.Form["venue-name"], Request.Form["venue-address"], id);
      try
      {
        //Checks that input start time and end time are valid.
        if (start.Subtract(end).TotalHours > 1)
        {
          throw new InvalidStartOrEndException();
        }
        selectedEvent.Update();
        return RedirectToAction("Events");
        // Maybe this should be a success page instead.
      }
      catch (InvalidStartOrEndException ex)
      {
        Dictionary<string,object> model = new Dictionary<string, object>{};
        List<Event> allEvents = Event.GetAll();
        model.Add("all-events", allEvents);
        model.Add("error", 1);
        return View("EventEdit", model);
      }
      catch (MySqlException ex)
      {
        Dictionary<string,object> model = new Dictionary<string, object>{};
        List<Event> allEvents = Event.GetAll();
        model.Add("all-events", allEvents);
        model.Add("error", 2);
        return View("EventEdit", model);
      }
    }

    // Get Past events.
    [HttpGet("/events/past")]
    public ActionResult EventsPast()
    {
      List<Event> pastEvents = Event.GetAllByDate(false);
      return View("Events", pastEvents);
    }

    // Get upcoming events.
    [HttpGet("/events/upcoming")]
    public ActionResult EventsUpcoming()
    {
      List<Event> upcomingEvents = Event.GetAllByDate();
      return View("Events", upcomingEvents);
    }

  }
}
