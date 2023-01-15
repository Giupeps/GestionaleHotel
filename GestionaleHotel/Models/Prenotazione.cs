using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace GestionaleHotel.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data prenotazione")]
        public DateTime DataPrenotazione { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inizio prenotazione")]
        public DateTime InizioPrenotazione { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fine prenotazione")]
        public DateTime FinePrenotazione { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public int Anno { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public decimal Tariffa { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Seleziona Cliente")]
        public Clienti IdClienti { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Camera IdCamera { get; set; }

        public Pernottamento IdPernottamento { get; set; }

        public static List<Prenotazione> GetPrenotazioni()
        {
            SqlConnection con = Connessione.GetConnection();
            List<Prenotazione> ListaPrenotazioni = new List<Prenotazione>();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select IdPrenotazione, Numero, DataPrenotazione, InizioPrenotazione, FinePrenotazione, Tariffa, Nome, Cognome, pernottamento.Descrizione from Prenotazioni inner join Camere on prenotazioni.idcamera = camere.idcamera inner join Clienti on prenotazioni.idclienti = clienti.idcliente inner join Pernottamento on prenotazioni.idpernottamento = pernottamento.id", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione pr = new Prenotazione();
                        pr.Id = Convert.ToInt32(reader["IdPrenotazione"]);
                        pr.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        pr.InizioPrenotazione = Convert.ToDateTime(reader["InizioPrenotazione"]);
                        pr.FinePrenotazione = Convert.ToDateTime(reader["FinePrenotazione"]);
                        pr.Tariffa = Convert.ToDecimal(reader["Tariffa"]);

                        Camera ca = new Camera();
                        pr.IdCamera = ca;
                        ca.Numero = Convert.ToInt32(reader["Numero"]);

                        Clienti c = new Clienti();
                        pr.IdClienti = c;
                        c.Nome = reader["Nome"].ToString();
                        c.Cognome = reader["Cognome"].ToString();

                        Pernottamento per = new Pernottamento();
                        pr.IdPernottamento = per;
                        per.Descrizione = reader["Descrizione"].ToString();

                        ListaPrenotazioni.Add(pr);
                    }
                }
            }
            catch (Exception)
            {

              
            }
            finally { con.Close(); }
            return ListaPrenotazioni;
        }
        
        public static void CreatePrenotazione(Prenotazione pr)
        {
            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Insert into Prenotazioni values (@DataPrenotazione, @InizioPrenotazione, @FinePrenotazione, @Anno, @Tariffa, @IdCliente, @IdCamera, @IdPernottamento)", con);

                command.Parameters.AddWithValue("DataPrenotazione", pr.DataPrenotazione);
                command.Parameters.AddWithValue("@InizioPrenotazione", pr.InizioPrenotazione);
                command.Parameters.AddWithValue("@FinePrenotazione", pr.FinePrenotazione);
                command.Parameters.AddWithValue("@Anno", pr.InizioPrenotazione.Year);
                command.Parameters.AddWithValue("@Tariffa", pr.Tariffa);
                command.Parameters.AddWithValue("@IdCliente", pr.IdClienti.ID);
                command.Parameters.AddWithValue("@IdCamera", pr.IdCamera.Id);
                command.Parameters.AddWithValue("@idPernottamento", pr.IdPernottamento.Id);

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

        public static Prenotazione GetPrenotazione(int id)
        {
            SqlConnection con = Connessione.GetConnection();
            Prenotazione pr = new Prenotazione();
            Camera ca = new Camera();
            Pernottamento per = new Pernottamento();
            pr.IdCamera= ca;
            pr.IdPernottamento = per;
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Select * from Prenotazioni where IdPrenotazione = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pr.Id = Convert.ToInt32(reader["IdCamera"]);
                        pr.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        pr.InizioPrenotazione = Convert.ToDateTime(reader["InizioPrenotazione"]);
                        pr.FinePrenotazione = Convert.ToDateTime(reader["FinePrenotazione"]);
                        pr.Tariffa = Convert.ToDecimal(reader["Tariffa"]);
                        pr.IdCamera.Id = Convert.ToInt32(reader["IdCamera"]);
                        pr.IdPernottamento.Id = Convert.ToInt32(reader["IdCamera"]);
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
            return pr;
        }

        public static void ModificaPrenotazione(Prenotazione pr, int id)
        {

            SqlConnection con = Connessione.GetConnection();

            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Update Prenotazioni Set dataprenotazione = @DataPrenotazione, inizioprenotazione = @InizioPrenotazione, fineprenotazione = @FinePrenotazione, Tariffa = @Tariffa, idcamera = @IdCamera, idpernottamento = @IdPernottamento where IdPrenotazione = @ID", con);

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@DataPrenotazione", pr.DataPrenotazione);
                command.Parameters.AddWithValue("@InizioPrenotazione", pr.InizioPrenotazione);
                command.Parameters.AddWithValue("@FinePrenotazione", pr.FinePrenotazione);
                command.Parameters.AddWithValue("@Tariffa", pr.Tariffa);
                command.Parameters.AddWithValue("@IdCamera", pr.IdCamera.Id);
                command.Parameters.AddWithValue("@IdPernottamento", pr.IdPernottamento.Id);

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

        public static void CancellaPrenotazione(int id)
        {
            SqlConnection con = Connessione.GetConnection();
            try
            {
                con.Open();
                SqlCommand command = Connessione.GetCommand("Delete from Prenotazioni where idprenotazione = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            
            }
            finally { con.Close(); }
        }
    }
}