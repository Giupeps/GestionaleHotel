using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleHotel.Models
{
    public class Clienti
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "C.F.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo deve contenere 16 caratteri")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Città")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Provincia")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Il campo deve contenere 2 caratteri")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Cellulare")]
        public string Cellulare { get; set; }

        public static List<SelectListItem> GetClientiDropdown()
        {
            SqlConnection con = Connessione.GetConnection();
            List<SelectListItem> ListaClientiDropdown = new List<SelectListItem>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Clienti", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem ca = new SelectListItem();
                        ca.Text = reader["Nome"].ToString() + " " + reader["Cognome"].ToString();
                        ca.Value = reader["IdCliente"].ToString();
                        ListaClientiDropdown.Add(ca);
                    }
                }
            }
            catch (Exception)
            {


            }
            finally { con.Close(); }
            return ListaClientiDropdown;
        }

       public static List<Clienti> Getclienti()
        {
            SqlConnection con = Connessione.GetConnection();
            List<Clienti> ListaClienti = new List<Clienti>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Clienti", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Clienti cliente = new Clienti();
                        cliente.ID = Convert.ToInt32(reader["IDCliente"]);
                        cliente.Nome = reader["Nome"].ToString();
                        cliente.Cognome = reader["Cognome"].ToString();
                        cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                        cliente.Citta = reader["Citta"].ToString();
                        cliente.Provincia = reader["Provincia"].ToString();
                        cliente.Email = reader["Email"].ToString();
                        if (reader["Telefono"] == DBNull.Value)
                        {
                            cliente.Telefono = null;
                        }
                        else
                        {
                            cliente.Telefono = reader["Telefono"].ToString();
                        }
                        cliente.Cellulare = reader["Cellulare"].ToString();
                        ListaClienti.Add(cliente);
                    }
                }
            }
            catch (Exception)
            {
                con.Close();

            }
            con.Close();
            return ListaClienti;
        }

        public static void Create(Clienti c)
        {
            SqlConnection con = Connessione.GetConnection();
            
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Insert into Clienti values (@Nome, @Cognome, @CodiceFiscale, @Citta, @Provincia, @Email, @Telefono, @Cellulare)", con);

                command.Parameters.AddWithValue("@Nome", c.Nome);
                command.Parameters.AddWithValue("@Cognome", c.Cognome);
                command.Parameters.AddWithValue("@CodiceFiscale", c.CodiceFiscale);
                command.Parameters.AddWithValue("@Citta", c.Citta);
                command.Parameters.AddWithValue("@Provincia", c.Provincia);
                command.Parameters.AddWithValue("@Email", c.Email);
                if (c.Telefono == null)
                {
                    command.Parameters.AddWithValue("@Telefono", "");
                }
                else
                {

                    command.Parameters.AddWithValue("@Telefono", c.Telefono);
                }
                command.Parameters.AddWithValue("@Cellulare", c.Cellulare);

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

        public static Clienti GetSingoloCliente(int id)
        {
            SqlConnection con = Connessione.GetConnection();
            Clienti cliente = new Clienti();
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Clienti where idcliente = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente.ID = Convert.ToInt32(reader["IDcliente"]);
                        cliente.Nome = reader["Nome"].ToString();
                        cliente.Cognome = reader["Cognome"].ToString();
                        cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                        cliente.Citta = reader["Citta"].ToString();
                        cliente.Provincia = reader["Provincia"].ToString();
                        cliente.Email = reader["Email"].ToString();
                        if (reader["Telefono"] == DBNull.Value)
                        {
                            cliente.Telefono = null;
                        }
                        else
                        {
                            cliente.Telefono = reader["Telefono"].ToString();
                        }
                        cliente.Cellulare = reader["Cellulare"].ToString();
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
            return cliente;
        } 

        public static void ModificaCliente(int id, Clienti c)
        {
            SqlConnection con = Connessione.GetConnection();
            
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Update Clienti Set Nome=@Nome, Cognome=@Cognome, CodiceFiscale=@CodiceFiscale, Citta=@Citta, Provincia=@Provincia, Email=@Email, Telefono=@Telefono, Cellulare=@Cellulare where idcliente = @ID", con);

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Nome", c.Nome);
                command.Parameters.AddWithValue("@Cognome", c.Cognome);
                command.Parameters.AddWithValue("@CodiceFiscale", c.CodiceFiscale);
                command.Parameters.AddWithValue("@Citta", c.Citta);
                command.Parameters.AddWithValue("@Provincia", c.Provincia);
                command.Parameters.AddWithValue("@Email", c.Email);
                if (c.Telefono == null)
                {
                    command.Parameters.AddWithValue("@Telefono", "");
                }
                else
                {

                    command.Parameters.AddWithValue("@Telefono", c.Telefono);
                }
                command.Parameters.AddWithValue("@Cellulare", c.Cellulare);

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

        public static void CancellaCliente(int id)
        {
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Delete from Clienti where idcliente = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
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
    }
}