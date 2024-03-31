using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI2.Models;

namespace WebAPI2.Controllers
{
    public class ContactsController : ApiController
    {
        // GET api/values
        public IEnumerable<Contacts> Get()
        {


            List<Contacts> contacts = new List<Contacts> ();
            //read all students from database
            string query = "Select * FROM Contacts";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection (connectionString))
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter (query,connection )) 
                {
                    DataTable dataTable = new DataTable ();
                    adapter.Fill(dataTable);

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
          
                        {
                            contacts.Add(new Contacts
                            {
                                ID = (int)row["ID"],
                                FirstName = (string)row["FirstName"],
                                LastName = (string)row["LastName"],
                                Email = (string)row["Email"],
                                Phone = (string)row["Phone"]
                            });
                        }
                    }
                }
            }
            return contacts;
        }

        // GET api/values/5
        public Contacts Get(int ID)
        {
            Contacts contacts = new Contacts();

            string query = "Select from Contacts where ID=" + ID + ";";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable != null)
                    {

                        contacts = new Contacts
                        {
                            ID = (int)dataTable.Rows[0]["ID"],
                            FirstName = (string)dataTable.Rows[0]["FirstName"],
                            LastName = (string)dataTable.Rows[0]["LastName"],
                            Email = (string)dataTable.Rows[0]["Email"],
                            Phone = (string)dataTable.Rows[0]["Phone"]
                        };

                    }
                }
            }
            return contacts;
        }
        // POST api/values
        public IHttpActionResult Post([FromBody] Contacts cont)
        {

            string query = "Insert into Students(FirstName, LastName, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone);";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", cont.FirstName);
                    command.Parameters.AddWithValue("@LastName", cont.LastName);
                    command.Parameters.AddWithValue("@Email", cont.Email);
                    command.Parameters.AddWithValue("@Phone", cont.Phone);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                 
              }
                return Ok();
           }

        // PUT api/values/5
        public IHttpActionResult Put( [FromBody] Contacts cont)
        {
            string query = "Update Contactss set FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone where ID=@ID;";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", cont.ID);
                    command.Parameters.AddWithValue("@FirstName", cont.FirstName);
                    command.Parameters.AddWithValue("@LastName", cont.LastName);
                    command.Parameters.AddWithValue("@Email", cont.Email);
                    command.Parameters.AddWithValue("@Phone", cont.Phone);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return Ok();
        }

        // DELETE api/values/5
        public void Delete(int ID)
        {
            string query = "Delete Students where ID="+ID+";";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
          
        }
    }

    public class Contacts
    {
        public int ID { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Email { get; internal set; }
        public string Phone { get; internal set; }
    }
}
