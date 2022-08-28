using System;
using  NetRodhe2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;

namespace NetRodhe2.Logica
{
    public class AreaLogica
    {
        private static AreaLogica instancia = null;

        public AreaLogica()
        {

        }
        public static AreaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new AreaLogica();
                }

                return instancia;
            }
        }
        public bool Registrar(Area objetoAr)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarArea", oConexion);
                    cmd.Parameters.AddWithValue("abrev_area", objetoAr.abrev_area);
                    cmd.Parameters.AddWithValue("descripcion", objetoAr.descripcion);
                    cmd.Parameters.AddWithValue("id_edificios", objetoAr.IEdificios.id_edificios);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Area objetoAr)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarArea", oConexion);
                    cmd.Parameters.AddWithValue("id_area", objetoAr.id_area);
                    cmd.Parameters.AddWithValue("descripcion", objetoAr.descripcion);
                    cmd.Parameters.AddWithValue("id_edificios", objetoAr.IEdificios.id_edificios);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
        public List<Area> Listar()
        {
            List<Area> Lista = new List<Area>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select a.id_area, a.abrev_area, a.descripcion,");
                sb.AppendLine("e.id_edificios, e.nombre_edificios");
                sb.AppendLine("from tbl_area a");
                sb.AppendLine("inner join tbl_edificios e on e.id_edificios = a.id_edificios");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Lista.Add(new Area()
                        {
                            id_area = Convert.ToInt32(dr["id_area"].ToString()),
                            abrev_area = dr["abrev_area"].ToString(),
                            descripcion = dr["descripcion"].ToString(),
                            IEdificios = new Edificios() { id_edificios = Convert.ToInt32(dr["id_edificios"].ToString()), nombre_edificios = dr["nombre_edificios"].ToString() },
                        });
                    }
                    dr.Close();

                    return Lista;

                }
                catch (Exception ex)
                {
                    Lista = null;
                    return Lista;
                }
            }
        }
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from tbl_area where id_area = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

    }
}