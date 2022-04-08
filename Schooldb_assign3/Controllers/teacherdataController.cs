using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Schooldb_assign3.Models;

namespace Schooldb_assign3.Controllers
{
    public class teacherdataController : ApiController
    {

        // The db context class which allows to access our mysql db.
        private SchoolDbContext School = new SchoolDbContext();

        //This controller ll access the teachers table of our school db.
        /// <summary>
        /// This will return a list of teachers name from the db
        /// </summary>
        /// <param name="teacherInfo">optional key or teacherInfo for name</param>
        /// <example> Get api/teacherdata/listteacher/xyz</example>
        /// <example>GET api/teacherdata/listteacher</example>
        /// <returns>
        /// A list of teachers (first and lasst name and id of teaxhers)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteacher/{teacherInfo?}")]
        //? in the key means this is optional so that app will not break
        public List<teachers> ListTeachers(string teacherInfo = null)
        {
            //creating connection
            MySqlConnection Conn = School.AccessDatabase();

            //opeming the connection between the server and db
            Conn.Open();

            //new command for our db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query ti select from
             // cmd.CommandText = "SELECT * FROM teachers";

            //we can also store sql query to a variable so that we can modify it easily 
            string sqlQuery = "SELECT * FROM teachers";


            if( teacherInfo != null)
            {
                //  sqlQuery = sqlQuery + " WHERE lower(teacherfname)=lower('"+teacherInfo+"')";
                sqlQuery = sqlQuery + " WHERE lower(teacherfname)=lower(@key)";
                cmd.Parameters.AddWithValue("@key", teacherInfo);
                cmd.Prepare();
            } else if( teacherInfo == " " )
            {
                sqlQuery = "select * from teachers where teacherid="+1;
            }

            cmd.CommandText = sqlQuery;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //this will create a list of teachers name
            List<teachers> Teachers = new List<teachers> { };

            //it will loop through each row 
            while (ResultSet.Read())
            {
                // Theteacher is a variable of type teachers obj
                teachers Theteachers = new teachers();
                Theteachers.teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                Theteachers.teacherfname = ResultSet["teacherfname"].ToString();
                Theteachers.teacherlname = ResultSet["teacherlname"].ToString();


                //it will add the teachers name in the empty list
                Teachers.Add(Theteachers);
            }

            //connection stopped
            Conn.Close();

            //return the teachers list
            return Teachers;
        }

        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]
        public teachers Findteacher(int teacherid)
        {
            //creating connection
            MySqlConnection Conn = School.AccessDatabase();

            //opening the connection between the server and db
            Conn.Open();

            //command for db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query to select from and gettingteacher data using id
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid="+ teacherid;

            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            teachers selectedteacher = new teachers();
           
            while (ResultSet.Read())
            {
                //looping tjrough the result
                selectedteacher.teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                selectedteacher.teacherfname = ResultSet["teacherfname"].ToString();
                selectedteacher.teacherlname = ResultSet["teacherlname"].ToString();
                selectedteacher.salary = ResultSet["salary"].ToString();

            }

            //closing connection
            Conn.Close();

            //return the selected teacher data
            return selectedteacher;

        }
        /// <summary>
        /// Adding new teachers to db
        /// <paramref name="NewTeacher"/> New Teachers info that I want to add
        /// </summary>
        public string AddTeacher(teachers NewTeacher)
        {

            //creating connection
            MySqlConnection Conn = School.AccessDatabase();

            //opeming the connection between the server and db
            Conn.Open();

            //new command for our db
            MySqlCommand cmd = Conn.CreateCommand();
            
                string sqlQuery = "insert into teachers (teacherfname, teacherlname, salary) values (@teacherfname, @teacherlname, @salary)"; //these are keys

                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
                cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
                cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            
            Conn.Close();

            return "Success";
            
        }

        /// <summary>
        /// Here we are deleting the teacher from our db
        /// </summary>
        /// <param name="teacherid">Primary key of teacher to get teacher</param>
        public void DeleteTeacher(int teacherid)
        {

            //creating connection
            MySqlConnection Conn = School.AccessDatabase();

            //opeming the connection between the server and db
            Conn.Open();

            //new command for our db
            MySqlCommand cmd = Conn.CreateCommand();

            // sql query

            string SqlQuery = "delete from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", teacherid);
            cmd.CommandText = SqlQuery;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

       

    }
    }
