using apiWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace apiWeb.DataConnection
{
    public class ListarDocentes
    {
        public IEnumerable<RegistroDocentes> Listar()
        {
            List<RegistroDocentes> listarDocentes = new List<RegistroDocentes>();
            using (SqlConnection Conexion = new Conexion().getcn)
            {
                Conexion.Open();
                SqlCommand sqlCommand = new SqlCommand("ListarDocentes", Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    listarDocentes.Add(new RegistroDocentes()
                    {
                        Id = dataReader.GetInt32(0),
                        PrimerNombre = dataReader.GetString(1),
                        SegundoNombre = dataReader.GetString(2),
                        PrimerApellido = dataReader.GetString(3),
                        SegundoApellido = dataReader.GetString(4),
                        Correo = dataReader.GetString(5),
                        Telefono = dataReader.GetString(6),
                        FechaNacimiento = dataReader.GetDateTime(7),
                    });
                }
            }
            return listarDocentes;
        }
        public string InsertarDocentes(RegistroDocentes registarDocentes)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarDocente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", registarDocentes.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", registarDocentes.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", registarDocentes.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", registarDocentes.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", registarDocentes.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Correo", registarDocentes.Correo));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", registarDocentes.FechaNacimiento));                    
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Registrada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }
        public string ActualizarDocente(RegistroDocentes actualizarDocentes)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ActualizarDocente1", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", actualizarDocentes.Id));
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", actualizarDocentes.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", actualizarDocentes.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", actualizarDocentes.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", actualizarDocentes.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", actualizarDocentes.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Correo", actualizarDocentes.Correo));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", actualizarDocentes.FechaNacimiento));
                    
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Actualizada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string EliminarDocente(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EliminarProfesor", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@id_profesor", id));
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

