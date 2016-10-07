using FoodMood.Web.Models.Domains;
using FoodMood.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodMood.Web.Services
{
    public class OptionsService
    {
        public static List<Option> SelectAll()
        {
            List<Option> list = new List<Option>();
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Options_SelectAll";

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Option o = new Option();

                    o.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    o.Name = dr["name"].ToString();
                    int GenreIdIndex = dr.GetOrdinal("GenreId");
                    if(dr.IsDBNull(GenreIdIndex))
                    {
                        o.Genre = null;
                    }
                    else
                    {
                        o.Genre = new Genre();
                        o.Genre.Id = dr.GetInt32(dr.GetOrdinal("GenreId"));
                        o.Genre.Name = dr.GetString(dr.GetOrdinal("GenreName"));
                    }
                    list.Add(o);
                }
            }
            return list;
        }




        public static Option SelectById(int id)
        {
            Option o = new Option();
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Options_SelectById";

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    o.Name = dr["Name"].ToString();
                  
                }
            }
            return o;
        }

        public static int Insert(OptionAddRequest model)
        {
            int id = 0;
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Options_Insert";

                SqlParameterCollection pc = cmd.Parameters;
                pc.AddWithValue("@Name", model.Name);
                pc.AddWithValue("@GenreId", model.GenreId);

                SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                pc.Add(p);

                cmd.ExecuteNonQuery();

                Int32.TryParse(pc["@Id"].Value.ToString(), out id);
            }
            return id;
        }

        public static void Update(OptionUpdateRequest model)
        {
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Options_Update";

                SqlParameterCollection pc = cmd.Parameters;
                pc.AddWithValue("@Id", model.Id);
                pc.AddWithValue("@Name", model.Name);
                pc.AddWithValue("@GenreId", model.GenreId);

                cmd.ExecuteNonQuery();
            }
        }


        public static void Delete(int id)
        {
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Options_Delete";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

    }
}