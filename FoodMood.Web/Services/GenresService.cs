using System;
using System.Collections.Generic;
using FoodMood.Web.Models.Domains;
using System.Data.SqlClient;
using System.Data;
using FoodMood.Web.Models.Requests;

namespace FoodMood.Web.Services
{
    public static class Helpers
    {
        public static string AsString(this IDataReader reader, int ordinal, bool trim = false)
        {
            try
            {
                if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
                {
                    if (trim)
                        return reader.GetString(ordinal).Trim();
                    else
                        return reader.GetString(ordinal);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }



    public class GenresService : BaseService
    {
        public static List<Genre> SelectAll()
        {
            List<Genre> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Genres_SelectAll",
                inputParamMapper: null,
                map: delegate (IDataReader r, short set)
                {
                    int ordinal = 0;
                    switch (set)
                    {
                        case 0:
                            Genre g = new Genre();
                            g.Id = r.GetInt32(ordinal++);
                            g.Name = r.AsString(ordinal++);

                            if (list == null)
                            {
                                list = new List<Genre>();
                            }
                            list.Add(g);
                            break;
                    }
                });
            return list;
        }

        public static Genre SelectById(int id)
        {
            Genre g = new Genre();
            DataProvider.ExecuteCmd(GetConnection, "dbo.Genres_SelectById",
                inputParamMapper: delegate (SqlParameterCollection pc)
                {
                    pc.AddWithValue("@Id", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    int ordinal = 0;
                    g.Id = reader.GetInt32(ordinal++);
                    g.Name = reader.GetString(ordinal++);
                }
                );
            return g;
        }

        public static int Insert(GenreAddRequest model)
        {
            int id = 0;
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Genres_Insert";

                SqlParameterCollection pc = cmd.Parameters;
                pc.AddWithValue("@Name", model.Name);
                SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                pc.Add(p);

                cmd.ExecuteNonQuery();
                Int32.TryParse(pc["@Id"].Value.ToString(), out id);
            }
            return id;
        }


        public static void Update(GenreUpdateRequest model)
        {
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Genres_Update";

                SqlParameterCollection pc = cmd.Parameters;
                pc.AddWithValue("@Id", model.Id);
                pc.AddWithValue("@Name", model.Name);

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
                cmd.CommandText = "dbo.Genres_Delete";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }


        #region before abstraction 

        //public static Genre SelectById(int id)
        //{
        //    Genre g = new Genre();

        //    string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "dbo.Genres_SelectById";

        //        cmd.Parameters.AddWithValue("@Id", id);

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while(dr.Read())
        //        {
        //            g.Id = dr.GetInt32(dr.GetOrdinal("Id"));
        //            g.Name = dr["Name"].ToString();
        //        }   
        //    }
        //    return g;
        //}

        #endregion
    }

}