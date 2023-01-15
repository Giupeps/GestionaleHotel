using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionaleHotel.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Password { get; set; }

        public string Ruolo { get; set; }

        public static bool AuthenticateUser(string username, string password) 
        { 
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * From Users where Username = @username and [Password] = @password", con);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else 
                { 
                    return false; 
                }
            }
            catch (Exception)
            {
                return false;
                
            }
            finally { con.Close(); }
        }

    }
}