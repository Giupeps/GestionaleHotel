using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Models
{
    public class Pernottamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = " Tipo di pernottamento ")]
        public string Descrizione { get; set; }

        public static List<Pernottamento> GetPernottamento()
        {
            SqlConnection con = Connessione.GetConnection();
            List<Pernottamento> ListaPernottamenti = new List<Pernottamento>();
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Pernottamento", con);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pernottamento pe = new Pernottamento();
                        pe.Id = Convert.ToInt32(reader["Id"]);
                        pe.Descrizione = reader["Descrizione"].ToString();
                        ListaPernottamenti.Add(pe);
                    }
                }

                command.ExecuteReader();
            }
            catch (Exception)
            {


            }
            finally { con.Close(); }
            return ListaPernottamenti;
        }

        public static List<SelectListItem> GetPernottamentiDropdown()
        {
            SqlConnection con = Connessione.GetConnection();
            List<SelectListItem> ListaPernottamentiDropdown = new List<SelectListItem>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Pernottamento", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem per = new SelectListItem();
                        per.Text = reader["Descrizione"].ToString();
                        per.Value = reader["Id"].ToString();
                        ListaPernottamentiDropdown.Add(per);
                    }
                }
            }
            catch (Exception)
            {


            }
            finally { con.Close(); }
            return ListaPernottamentiDropdown;

        }

    }
}