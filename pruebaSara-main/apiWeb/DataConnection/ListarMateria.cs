using apiWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace apiWeb.DataConnection
{
    public class ListarMateria
    {
        public IEnumerable<RegistroMateria> Listar()
        {
            List<RegistroMateria> listarMaterias = new List<RegistroMateria>();
            using (SqlConnection Conexion = new Conexion().getcn)
            {
                Conexion.Open();
                SqlCommand sqlCommand = new SqlCommand("ListaMaterias", Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    listarMaterias.Add(new RegistroMateria()
                    {
                        IDMateria = dataReader.GetInt32(0),
                        Nombre_materia = dataReader.GetString(1),
                       
                    });
                }
            }
            return listarMaterias;
        }
        public string InsertarMateria(RegistroMateria registrarMateria)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarMateria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreMateria", registrarMateria.Nombre_materia));
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Registrada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }
        public string ActualizarMateria(RegistroMateria actualizarMateria)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ActualizarMateria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreMateria", actualizarMateria.Nombre_materia));
                    cmd.Parameters.Add(new SqlParameter("@Id", actualizarMateria.IDMateria));
                   
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Actualizada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string EliminarMateria(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EliminarMateria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Eliminada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }
    }
}

