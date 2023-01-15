using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace GestionaleHotel.Models
{
    public class Camera
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "N° camera")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Singola/Doppia")]
        public string Tipologia { get; set; }

        public static List<SelectListItem> GetTipologie()
        {
            List<SelectListItem> ListaTipologie= new List<SelectListItem>();
            SelectListItem Singola = new SelectListItem { Text = "Singola", Value = "Singola" };
            SelectListItem Doppia = new SelectListItem { Text = "Doppia", Value = "Doppia" };
            SelectListItem Tripla = new SelectListItem { Text = "Tripla", Value = "Tripla" };
            ListaTipologie.Add(Singola);
            ListaTipologie.Add(Doppia);
            ListaTipologie.Add(Tripla);

            return ListaTipologie;
        }
        public static List<SelectListItem> GetCamereDropdown()
        {
            SqlConnection con = Connessione.GetConnection();
            List<SelectListItem> ListaCamereDropdown= new List<SelectListItem>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Camere", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem ca = new SelectListItem();
                        ca.Text = "Stanza numero: " + reader["Numero"].ToString();
                        ca.Value = reader["IdCamera"].ToString();
                        ListaCamereDropdown.Add(ca);
                    }
                }
            }
            catch (Exception)
            {

               
            }
            finally { con.Close(); }
            return ListaCamereDropdown;
        }

        public static List<Camera> GetCamere()
        {
            SqlConnection con = Connessione.GetConnection();
            List<Camera> ListaCamere = new List<Camera>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Camere", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Camera cam = new Camera();
                        cam.Id = Convert.ToInt32(reader["IdCamera"]);
                        cam.Numero = Convert.ToInt32(reader["numero"]);
                        cam.Descrizione = reader["descrizione"].ToString();
                        cam.Tipologia = reader["tipologia"].ToString();
                        
                        ListaCamere.Add(cam);
                    }
                }
            }
            catch (Exception)
            {
                con.Close();

            }
            con.Close();
            return ListaCamere;
        }

        public static void CreateCamera(Camera ca) 
        {
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Insert into Camere values (@Numero, @Descrizione, @Tipologia)", con);

                command.Parameters.AddWithValue("@Numero", ca.Numero);
                command.Parameters.AddWithValue("@Descrizione", ca.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", ca.Tipologia);
              
                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
        }

        public static Camera GetSingolCamera(int id)
        {
            SqlConnection con = Connessione.GetConnection();
            Camera ca= new Camera();
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Camere where IdCamera = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ca.Id = Convert.ToInt32(reader["IdCamera"]);
                        ca.Numero = Convert.ToInt32(reader["numero"]);
                        ca.Descrizione = reader["descrizione"].ToString();
                        ca.Tipologia = reader["tipologia"].ToString();
                       
                    }
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return ca;

        }

        public static void ModificaCamera(Camera ca, int id)
        {
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Update Camere Set Numero = @Numero, Descrizione = @Descrizione, Tipologia = @Tipologia where IdCamera = @ID", con);

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Numero", ca.Numero);
                command.Parameters.AddWithValue("@Descrizione", ca.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", ca.Tipologia);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
        }

        public static void DeleteCamera(int id)
        {
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Delete from Camere where IdCamera = @Id", con);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

               
            }
            finally { con.Close(); }
        }
    }
}