// DateTime currentDate = DateTime.Now;
// string currentDateinMySql = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
// string foundDate = rdr.GetDateTime(0).ToString();
// Convert.ToDateTime(Request.Form["date"])

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace DJ.Models
{
    public class Event
    {
        // Properties id, start date, end date, event name, venue name, venue address including getters and setters.
        private int _id;
        public int GetId() {return _id;}
        public void SetId(int id) {_id = id;}

        private DateTime _start;
        public DateTime GetStart() {return _start;}
        public void SetStart(DateTime start) {_start = start;}

        private DateTime _end;
        public DateTime GetEnd() {return _end;}
        public void SetEnd(DateTime end) {_end = end;}

        private string _eventName;
        public string GetName() {return _eventName;}
        public void SetName(string name) {_eventName = name;}

        private string _venueName;
        public string GetVenueName() {return _venueName;}
        public void SetVenueName(string venueName) { _venueName = venueName;}

        private string _venueAddress;
        public string GetVenueAddress() {return _venueAddress;}
        public void SetVenueAddress(string venueAddress) {_venueAddress = venueAddress;}

        //Event Constructor
        public Event(DateTime start, DateTime end, string eventName, string venueName, string venueAddress, int id = 0)
        {
            SetStart(start);
            SetEnd(end);
            SetName(eventName);
            SetVenueName(venueName);
            SetVenueAddress(venueAddress);
            SetId(id);
        }

        // To prevent overlap, validate dates.
        public bool ValidateDates()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT COUNT(*) FROM events WHERE @EndTime >= start_time AND end_time >= @StartTime;";
          cmd.Parameters.Add(new MySqlParameter("@EndTime", this.GetEnd()));
          cmd.Parameters.Add(new MySqlParameter("@StartTime", this.GetStart()));
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int overlappingDates = 0;
          while(rdr.Read())
          {
            overlappingDates = rdr.GetInt32(0);
          }
          Console.WriteLine(overlappingDates);
          if (overlappingDates > 0)
          {
            return false;
          }
          else
          {
            return true;
          }
        }

        // Get all events in database by current date, by default will return upcoming. If false, will return past events.
        public static List<Event> GetAllByDate(bool upcoming = true)
        {
            List<Event> upcomingEvents = new List<Event> {};
            DateTime currentDate = DateTime.Now;
            string currentDateInMySql = currentDate.ToString("yyyy-MM-dd HH:mm:ss"); // saves date to MySql format
            string dateOperator = ">=";
            if (upcoming != true)
            {
                dateOperator = "<";
            }
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM events WHERE start_time" + @dateOperator + "@CurrentDate;";
            cmd.Parameters.Add(new MySqlParameter("@CurrentDate", currentDateInMySql));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int eventId = rdr.GetInt32(0);
                DateTime start = rdr.GetDateTime(1);
                DateTime end = rdr.GetDateTime(2);
                string eventName = rdr.GetString(3);
                string venueName = rdr.GetString(4);
                string venueAddress = rdr.GetString(5);
                Event upcomingEvent = new Event(start, end, eventName, venueName, venueAddress, eventId);
                upcomingEvents.Add(upcomingEvent);
            }
            return upcomingEvents;
        }

        // Get all for views, showing upcoming and back 1 month.
        public static List<Event> GetAllOneMonthBefore()
        {
            List<Event> displayEvents = new List<Event> {};
            DateTime monthBefore = DateTime.Now.AddMonths(-1);
            string monthBeforeString = monthBefore.ToString("yyyy-MM-dd HH:mm:ss"); // saves date to MySql format
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM events WHERE start_time >= @MonthBefore;";
            cmd.Parameters.Add(new MySqlParameter("@MonthBefore", monthBeforeString));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int eventId = rdr.GetInt32(0);
                DateTime start = rdr.GetDateTime(1);
                DateTime end = rdr.GetDateTime(2);
                string eventName = rdr.GetString(3);
                string venueName = rdr.GetString(4);
                string venueAddress = rdr.GetString(5);
                Event displayEvent = new Event(start, end, eventName, venueName, venueAddress, eventId);
                displayEvents.Add(displayEvent);
            }
            return displayEvents;
        }

        // Delete an event.
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM events WHERE id = @EventId;";
            cmd.Parameters.Add(new MySqlParameter("@EventId", GetId()));
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        // Update event details
        public void Update()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE events SET start_time = @StartTime, end_time = @EndTime, event_name = @EventName, venue_name = @VenueName, venue_address = @VenueAddress WHERE id = @EventId;";
            cmd.Parameters.Add(new MySqlParameter("@StartTime", this.GetStart()));
            cmd.Parameters.Add(new MySqlParameter("@EndTime", this.GetEnd()));
            cmd.Parameters.Add(new MySqlParameter("@EventName", this.GetName()));
            cmd.Parameters.Add(new MySqlParameter("@VenueName", this.GetVenueName()));
            cmd.Parameters.Add(new MySqlParameter("@VenueAddress", this.GetVenueAddress()));
            cmd.Parameters.Add(new MySqlParameter("@EventId", this.GetId()));
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        // Retrieve a specific event in database by id
        public static Event Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM events WHERE id = @SearchId;";
            cmd.Parameters.Add(new MySqlParameter("@SearchId", searchId));
            int id = 0;
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            string name = "";
            string venueName = "";
            string venueAddress = "";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                date1 = rdr.GetDateTime(1);
                date2 = rdr.GetDateTime(2);
                name = rdr.GetString(3);
                venueName = rdr.GetString(4);
                venueAddress = rdr.GetString(5);
            }
            Event foundEvent = new Event(date1, date2, name, venueName, venueAddress, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundEvent;
            // return new Event(Convert.ToDateTime("2017-12-01"), Convert.ToDateTime("2017-12-01"), "Fake", "fake", "fake");
        }

        // Save event to database
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO events (start_time, end_time, event_name, venue_name, venue_address) VALUES (@StartTime, @EndTime, @EventName, @VenueName, @VenueAddress);";
            cmd.Parameters.Add(new MySqlParameter("@StartTime", this.GetStart().ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new MySqlParameter("@EndTime", this.GetEnd().ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new MySqlParameter("@EventName", this.GetName()));
            cmd.Parameters.Add(new MySqlParameter("@VenueName", this.GetVenueName()));
            cmd.Parameters.Add(new MySqlParameter("@VenueAddress", this.GetVenueAddress()));
            cmd.ExecuteNonQuery();
            this.SetId((int)cmd.LastInsertedId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        // Get every event from the database.
        public static List<Event> GetAll()
        {
            List<Event> allEvents = new List<Event> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM events ORDER BY start_time DESC, end_time DESC;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int eventId = rdr.GetInt32(0);
                DateTime eventStart = rdr.GetDateTime(1);
                DateTime eventEnd = rdr.GetDateTime(2);
                string eventName = rdr.GetString(3);
                string venueName = rdr.GetString(4);
                string venueAddress = rdr.GetString(5);
                Event newEvent = new Event(eventStart, eventEnd, eventName, venueName, venueAddress, eventId);
                allEvents.Add(newEvent);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allEvents;
        }

        // Define equality for testing.
        public override bool Equals(System.Object otherEvent)
        {
            if (!(otherEvent is Event))
            {
                return false;
            }
            {
                Event newEvent = (Event) otherEvent;
                bool idEquality = this.GetId() == newEvent.GetId();
                bool startEquality = this.GetStart() == newEvent.GetStart();
                bool endEquality = this.GetEnd() == newEvent.GetEnd();
                bool nameEquality = this.GetName() == newEvent.GetName();
                bool venueNameEquality = this.GetVenueName() == newEvent.GetVenueName();
                bool venueAddressEquality = this.GetVenueAddress() == newEvent.GetVenueAddress();
                return (idEquality && startEquality && endEquality && nameEquality && venueNameEquality && venueAddressEquality);
            }
        }

        // Define for testing.
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        // Clear entire event database.
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM events;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
