using System;
using System.Collections.Generic;
using FoodMood.Web.Models.Domains;
using System.Data.SqlClient;
using System.Data;

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