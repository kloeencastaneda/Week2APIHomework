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
    public class StudentsController : ApiController
    {
        // GET api/values
        public IEnumerable<Student> Get()
        {


            List<Student> students = new List<Student> ();
            //read all students from database
            string query = "Select * FROM Students";
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
                            students.Add(new Student
                            {
                                ID = (int)row["ID"],
                                Name = (string)row["Name"],
                                Age = (int)row["Age"]
                            });
                        }
                    }
                }
            }
            return students;
        }

        // GET api/values/5
        public Student Get(int id)
        {
            Student student = new Student();

            string query = "Select from Students where ID=" + id + ";";
            string connectionString = @"Data Source=localhost, Initial Catalog = WebAPI; Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable != null)
                    {

                        student = new Student
                        {
                            ID = (int)dataTable.Rows[0]["ID"],
                            Name = (string)dataTable.Rows[0]["Name"],
                            Age = (int)dataTable.Rows[0]["Age"]
                        };

                    }
                }
            }
            return student;
        }
        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
