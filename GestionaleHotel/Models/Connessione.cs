using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionaleHotel.Models
{
    public class Connessione
    {
        public static SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["HotelDB"].ToString();
            SqlConnection con = new SqlConnection(conString);
            return con;
        }

        public static SqlCommand GetCommand(string commandtext, SqlConnection con) 
        {
            SqlCommand command = new SqlCommand(commandtext, con);           
            return command;
        }
    }
}