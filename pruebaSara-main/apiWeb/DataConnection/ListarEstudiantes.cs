using apiWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace apiWeb.DataConnection
{
    public class ListarEstudiantes
    {
        public IEnumerable<RegistroEstudiantes> Listar()
        {
            List<RegistroEstudiantes> listarEstudiantes = new List<RegistroEstudiantes>();
            using (SqlConnection Conexion = new Conexion().getcn)
            {
                Conexion.Open();
                SqlCommand sqlCommand = new SqlCommand("ListaEstudiantes", Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    listarEstudiantes.Add(new RegistroEstudiantes()
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
            return listarEstudiantes;
        }
        public string InsertarEstudiante(RegistroEstudiantes registraEstudiante)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarEstudiante", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", registraEstudiante.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", registraEstudiante.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", registraEstudiante.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", registraEstudiante.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", registraEstudiante.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Correo", registraEstudiante.Correo));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", registraEstudiante.FechaNacimiento));                    
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Registrada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }
        public string ActualizarEstudainte(RegistroEstudiantes actualizarEstudiante)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ActualizarEstudiante", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PrimerNombre", actualizarEstudiante.PrimerNombre));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", actualizarEstudiante.SegundoNombre));
                    cmd.Parameters.Add(new SqlParameter("@PrimerApellido", actualizarEstudiante.PrimerApellido));
                    cmd.Parameters.Add(new SqlParameter("@SegundoApellido", actualizarEstudiante.SegundoApellido));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", actualizarEstudiante.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Correo", actualizarEstudiante.Correo));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", actualizarEstudiante.FechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@Id", actualizarEstudiante.Id));
                    cmd.ExecuteNonQuery();
                    mensaje = "Solicitud Actualizada";
                }
                catch (Exception ex) { mensaje = ex.Message; }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string EliminarEstudiante(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EliminarEstudainte", cn);
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

