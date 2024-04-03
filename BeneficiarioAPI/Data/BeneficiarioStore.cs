using BeneficiarioAPI.Models;
using Microsoft.Data.SqlClient;

//using System.Data.SqlClient;
using System.Data;

namespace BeneficiarioAPI.Data
{
    public class BeneficiarioStore
    {
        public static Beneficiario SeleccionaBeneficiario(int id)
        {
            Beneficiario beneficiario = new();
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SeleccionaBeneficiario", cn);
                cmd.Parameters.AddWithValue("@idBeneficiario", id);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    beneficiario.Id = dr.GetInt32(0);
                    beneficiario.Nombre = dr.GetString(1);
                    beneficiario.Apellidos = dr.GetString(2);
                    beneficiario.FechaNacimiento = dr.GetDateTime(3);
                    beneficiario.CURP = dr.GetString(4);
                    beneficiario.SSN = dr.GetString(5);
                    beneficiario.Telefono = dr.GetString(6);
                    beneficiario.Nacionalidad = dr.GetString(7);
                    beneficiario.Porcentaje = dr.GetInt32(8);
                    beneficiario.IdEmpleado = dr.GetInt32(9);
                }
                cn.Close();
            }
            return beneficiario;
        }

        public static IEnumerable<Beneficiario> BeneficiarioLista(int id)
        {
            List<Beneficiario> Beneficiarios = new List<Beneficiario>();
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("ListaBeneficiarios", cn);
                cmd.Parameters.Add("@idEmpleado", SqlDbType.VarChar).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Beneficiarios.Add(new Beneficiario()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellidos = dr.GetString(2),
                        FechaNacimiento = dr.GetDateTime(3),
                        CURP = dr.GetString(4),
                        SSN = dr.GetString(5),
                        Telefono = dr.GetString(6),
                        Nacionalidad = dr.GetString(7),
                        Porcentaje = dr.GetInt32(8),
                        IdEmpleado = dr.GetInt32(9)
                    }); ;
                }
                cn.Close();
            }
            return Beneficiarios;
        }

        public static bool BeneficiarioBorrar(int id)
        {
            using SqlConnection cn = new beneficiariosData().getConnection;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("BorraBeneficiario", cn);
                cmd.Parameters.Add("@idBeneficiario", SqlDbType.VarChar).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { cn.Close(); }

            return true;
        }

        public static bool BeneficiarioAgregar(Beneficiario reg)
        {
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("AgregaBeneficiario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reg.Id = 0;
                    cmd.Parameters.Add(new SqlParameter("@iNombre", reg.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@iApellidos", reg.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@iFechaNacimiento", reg.FechaNacimiento.ToString("yyyy-MM-dd")));
                    cmd.Parameters.Add(new SqlParameter("@iCURP ", reg.CURP));
                    cmd.Parameters.Add(new SqlParameter("@iSSN ", reg.SSN));
                    cmd.Parameters.Add(new SqlParameter("@iTelefono ", reg.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@iNacionalidad ", reg.Nacionalidad));
                    cmd.Parameters.Add(new SqlParameter("@iPorcentaje", reg.Porcentaje));
                    cmd.Parameters.Add(new SqlParameter("@iIdEmpleado", reg.IdEmpleado));
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

        public static bool BeneficiarioActualiza(Beneficiario reg)
        {
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ActualizaBeneficiario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idBeneficiario", reg.Id));
                    cmd.Parameters.Add(new SqlParameter("@iNombre", reg.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@iApellidos", reg.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@iFechaNacimiento", reg.FechaNacimiento.ToString("yyyy-MM-dd")));
                    cmd.Parameters.Add(new SqlParameter("@iCURP ", reg.CURP));
                    cmd.Parameters.Add(new SqlParameter("@iSSN ", reg.SSN));
                    cmd.Parameters.Add(new SqlParameter("@iTelefono ", reg.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@iNacionalidad ", reg.Nacionalidad));
                    cmd.Parameters.Add(new SqlParameter("@iPorcentaje", reg.Porcentaje));
                    cmd.Parameters.Add(new SqlParameter("@iIdEmpleado", reg.IdEmpleado));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { return false; }
                finally { cn.Close(); }
            }
            return true;
        }

        // TotalPorcentaje, recibe como parámetro el id del Empleado
        // para retornar el total de porcentaje de los beneficiarios
        public static int TotalPorcentajeMenosBeneficiario(int idE, int idB)
        {
            int Total = 0;
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("PorcentajesMenosBeneficiario", cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idE);
                cmd.Parameters.AddWithValue("@idBeneficiario", idB);
                // cmd.Parameters.Add("@idBeneficiario", SqlDbType.VarChar).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;

                IDbDataParameter totalParameter = cmd.CreateParameter();
                totalParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(totalParameter);

                cmd.ExecuteNonQuery();

                Total = Convert.ToInt32(totalParameter.Value);

                cn.Close();
            }
            return Total;
        }

        // TotalPorcentaje, recibe como parámetro el id del Empleado
        // para retornar el total de porcentaje de los beneficiarios
        public static int TotalPorcentaje(int id)
        {
            int Total = 0;
            using (SqlConnection cn = new beneficiariosData().getConnection)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("PorcentajeTotal", cn);
                cmd.Parameters.AddWithValue("@idEmpleado", id);

                cmd.CommandType = CommandType.StoredProcedure;

                IDbDataParameter totalParameter = cmd.CreateParameter();
                totalParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(totalParameter);

                cmd.ExecuteNonQuery();

                Total = Convert.ToInt32(totalParameter.Value);

                cn.Close();
            }
            return Total;
        }
    }

}
