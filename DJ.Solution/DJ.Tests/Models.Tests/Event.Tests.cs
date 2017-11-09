using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DJ.Models;

namespace DJ.Models.Tests
{
    [TestClass]
    public class DJTests : IDisposable
    {

        public void Dispose()
        {
            Event.ClearAll();
        }

        public DJTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dj_s_test;";
        }

        [TestMethod]
        public void GetAll_GetEmptyDatabaseInitially_0()
        {
            int result = Event.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesEventToDatabase_List()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-11-16 23:00"), Convert.ToDateTime("2017-11-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event1.Save();

            List<Event> allEvents = Event.GetAll();
            List<Event> expectedEvents = new List<Event>{event1};
            CollectionAssert.AreEqual(allEvents, expectedEvents);
        }

        [TestMethod]
        public void Find_FindsEventInDatabase_Event()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-11-16 23:00"), Convert.ToDateTime("2017-11-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event1.Save();

            Event foundEvent = Event.Find(event1.GetId());

            Assert.AreEqual(event1, foundEvent);
        }

        [TestMethod]
        public void Update_UpdatesEventInDatabase_String()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-11-16 23:00"), Convert.ToDateTime("2017-11-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event1.Save();

            event1.SetStart(Convert.ToDateTime("2017-12-16 23:00"));
            event1.SetEnd(Convert.ToDateTime("2017-12-17 02:00"));
            event1.SetName("Macy's Winter Nights");
            event1.Update();

            Event updatedEvent = Event.Find(event1.GetId());
            Assert.AreEqual(updatedEvent.GetName(), event1.GetName());
            Assert.AreEqual(updatedEvent.GetStart(), event1.GetStart());
            Assert.AreEqual(updatedEvent.GetEnd(), event1.GetEnd());
        }

        [TestMethod]
        public void Delete_DeletesEventInDatabase_False()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-11-16 23:00"), Convert.ToDateTime("2017-11-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event1.Save();
            Event event2 = new Event(Convert.ToDateTime("2017-12-16 22:00"), Convert.ToDateTime("2017-12-17 01:00"), "Macy's Winter Nights", "Macy's", "123 Sesame St");
            event2.Save();

            event1.Delete();
            List<Event> result = Event.GetAll();
            List<Event> expectedList = new List<Event>{event2};
            CollectionAssert.AreEqual(result, expectedList);
        }

        [TestMethod]
        public void GetAllByDate_GetsListOfOnlyUpcomingEvents_List()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-04-16 23:00"), Convert.ToDateTime("2017-04-17 02:00"), "Macy's Spring Nights", "Macy's", "123 Sesame St");
            event1.Save();
            Event event2 = new Event(Convert.ToDateTime("2017-07-16 22:00"), Convert.ToDateTime("2017-07-17 01:00"), "Macy's Summer Nights", "Macy's", "123 Sesame St");
            event2.Save();
            Event event3 = new Event(Convert.ToDateTime("2017-10-16 23:00"), Convert.ToDateTime("2017-10-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event3.Save();
            Event event4 = new Event(Convert.ToDateTime("2017-12-16 22:00"), Convert.ToDateTime("2017-12-17 01:00"), "Macy's Winter Nights", "Macy's", "123 Sesame St");
            event4.Save();

            List<Event> result = Event.GetAllByDate();
            List<Event> expectedList = new List<Event>{event4};
            CollectionAssert.AreEqual(result, expectedList);

        }

        [TestMethod]
        public void GetAllByDate_GetsListOfOnlyPastEvents_List()
        {
            Event event1 = new Event(Convert.ToDateTime("2017-04-16 23:00"), Convert.ToDateTime("2017-04-17 02:00"), "Macy's Spring Nights", "Macy's", "123 Sesame St");
            event1.Save();
            Event event2 = new Event(Convert.ToDateTime("2017-07-16 22:00"), Convert.ToDateTime("2017-07-17 01:00"), "Macy's Summer Nights", "Macy's", "123 Sesame St");
            event2.Save();
            Event event3 = new Event(Convert.ToDateTime("2017-10-16 23:00"), Convert.ToDateTime("2017-10-17 02:00"), "Macy's Autumn Nights", "Macy's", "123 Sesame St");
            event3.Save();
            Event event4 = new Event(Convert.ToDateTime("2017-12-16 22:00"), Convert.ToDateTime("2017-12-17 01:00"), "Macy's Winter Nights", "Macy's", "123 Sesame St");
            event4.Save();

            List<Event> result = Event.GetAllByDate(false);
            List<Event> expectedList = new List<Event>{event1, event2, event3};
            CollectionAssert.AreEqual(result, expectedList);
        }

        [TestMethod]
        public void ValidateDates_ChecksIfDatesExistInDatabase_false()
        {
          Event event1 = new Event(Convert.ToDateTime("2017-04-16 22:00"), Convert.ToDateTime("2017-04-17 02:00"), "Macy's Spring Nights", "Macy's", "123 Sesame St");
          event1.Save();
          Event event2 = new Event(Convert.ToDateTime("2017-04-16 23:00"), Convert.ToDateTime("2017-04-17 03:00"), "A Conlicting Time", "Macy's", "123 Sesame St");
          bool isValid = event2.ValidateDates();

          Assert.AreEqual(false, isValid);

        }
    }
}
