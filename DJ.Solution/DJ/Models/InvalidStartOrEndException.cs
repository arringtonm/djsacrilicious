using System;
using MySql.Data.MySqlClient;
using DJ;

namespace DJ.Models
{
  public class InvalidStartOrEndException: Exception
  {
    public InvalidStartOrEndException()
   {
   }

   public InvalidStartOrEndException(string message)
       : base(message)
   {
   }

   public InvalidStartOrEndException(string message, Exception inner)
       : base(message, inner)
   {
   }
  }
}
