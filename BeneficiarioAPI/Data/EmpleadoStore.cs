using BeneficiarioAPI.Models;
using Microsoft.Data.SqlClient;

//using System.Data.SqlClient;
using System.Data;

namespace BeneficiarioAPI.Data
{
    public static class EmpleadoStore
    {
        // SeleccionaEmpleado
        public static Empleado SeleccionaEmpleado(int id)
        {
            Empleado empleado = new();
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SeleccionaEmpleado", cn);
                cmd.Parameters.AddWithValue("@idEmpleado", id);
                // cmd.Parameters.Add("@idEmpleado", SqlDbType.VarChar).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // Id, Nombre, Apellidos, FechaNacimiento, NumeroEmpleado, CURP, SSN, Telefono, Nacionalidad
                    empleado.Id = dr.GetInt32(0);
                    empleado.Nombre = dr.GetString(1);
                    empleado.Apellidos = dr.GetString(2);
                    empleado.FechaNacimiento = dr.GetDateTime(3);
                    empleado.NumeroEmpleado = dr.GetInt32(4);
                    empleado.CURP = dr.GetString(5);
                    empleado.SSN = dr.GetString(6);
                    empleado.Telefono = dr.GetString(7);
                    empleado.Nacionalidad = dr.GetString(8);
                }
                cn.Close();
            }
            return empleado;
        }


        public static IEnumerable<Empleado> EmpleadoLista()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("ListaEmpledos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empleados.Add(new Empleado()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellidos = dr.GetString(2),
                        FechaNacimiento = dr.GetDateTime(3),
                        NumeroEmpleado = dr.GetInt32(4),
                        CURP = dr.GetString(5),
                        SSN = dr.GetString(6),
                        Telefono = dr.GetString(7),
                        Nacionalidad = dr.GetString(8),
                    });
                }
                cn.Close();
            }
            return empleados;
        }

        public static bool EmpleadoBorrar(int id)
        {
            // List<Empleado> empleados = new List<Empleado>();

            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("BorraEmpledo", cn);
                    cmd.Parameters.Add("@idEmpleado", SqlDbType.VarChar).Value = id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally { cn.Close(); }
            }
            return true;
        }

        public static bool EmpleadoAgregar(Empleado reg)
        {
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("AgregaEmpleado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reg.Id = 0;
                    cmd.Parameters.Add(new SqlParameter("@iNombre", reg.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@iApellidos", reg.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@iFechaNacimiento", reg.FechaNacimiento.ToString("yyyy-MM-dd")));
                    cmd.Parameters.Add(new SqlParameter("@iNumeroEmpleado", reg.NumeroEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@iCURP ", reg.CURP));
                    cmd.Parameters.Add(new SqlParameter("@iSSN ", reg.SSN));
                    cmd.Parameters.Add(new SqlParameter("@iTelefono ", reg.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@iNacionalidad ", reg.Nacionalidad));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string r = ex.Message;
                    return false;
                }
                finally { cn.Close(); }
            }
            return true;
        }

        public static bool EmpleadoActualiza(Empleado reg)
        {
            using SqlConnection cn = new beneficiariosData().getConnection;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("ActualizaEmpledo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEmpleado", reg.Id));
                cmd.Parameters.Add(new SqlParameter("@iNombre", reg.Nombre));
                cmd.Parameters.Add(new SqlParameter("@iApellidos", reg.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@iFechaNacimiento", reg.FechaNacimiento.ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new SqlParameter("@iNumeroEmpleado", reg.NumeroEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCURP ", reg.CURP));
                cmd.Parameters.Add(new SqlParameter("@iSSN ", reg.SSN));
                cmd.Parameters.Add(new SqlParameter("@iTelefono ", reg.Telefono));
                cmd.Parameters.Add(new SqlParameter("@iNacionalidad ", reg.Nacionalidad));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cn.Close();
            }

            return true;
        }
    }

}
