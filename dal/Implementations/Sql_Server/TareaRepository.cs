using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;

namespace dal.Implementations.Sql_Server
{
    public class TareaRepository
    {
        private string _connectionStrings = ConfigurationManager.ConnectionStrings["ConnSqlServer"].ConnectionString;

        public List<Tarea> ObtenerTodasTareas()
        {
            List<Tarea> tareas = new List<Tarea>();

            using(SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, titulo, descripcion, completada, fecha_creacion " +
                        "FROM tarea";

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Tarea tarea = new Tarea(
                                Convert.ToInt32(reader["id"]),
                                reader["titulo"].ToString(),
                                reader["descripcion"].ToString(),
                                Convert.ToBoolean(reader["completada"]),
                                Convert.ToDateTime(reader["fecha_creacion"])
                             );

                            tareas.Add(tarea);
                        }
                    }
                }
            }
            return tareas;
        }
        public Tarea ObtenerPorId(int id)
        {
            using(SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, titulo, descripcion, completada, fecha_creacion " +
                        "FROM tarea " +
                        "WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Tarea(
                                Convert.ToInt32(reader["id"]),
                                reader["titulo"].ToString(),
                                reader["descripcion"].ToString(),
                                Convert.ToBoolean(reader["completada"]),
                                Convert.ToDateTime(reader["fecha_creacion"])
                                );
                        }
                    }
                }
                return null;
            }
        }

        /*
            Agregar(Tarea tarea)
            Listar()
            BuscarPorId(int id)
            Modificar(Tarea tarea)
            Eliminar(int id)
         */
    }
}
