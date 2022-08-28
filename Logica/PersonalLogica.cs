using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using NetRodhe2.Models;

namespace NetRodhe2.Logica
{
    public class PersonalLogica
    {
        private static PersonalLogica instancia = null;

        public PersonalLogica()
        {

        }

        public static PersonalLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PersonalLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Personal objetoPersonal)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarPersonal", oConexion);
                    cmd.Parameters.AddWithValue("nombre", objetoPersonal.nombre);
                    cmd.Parameters.AddWithValue("puesto", objetoPersonal.puesto);
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

        public bool Modificar(Personal objetoPersonal)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarPersonal", oConexion);
                    cmd.Parameters.AddWithValue("id_personal", objetoPersonal.id_personal);
                    cmd.Parameters.AddWithValue("nombre", objetoPersonal.nombre);
                    cmd.Parameters.AddWithValue("puesto", objetoPersonal.puesto);
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
        public List<Personal>Listar()
        {
            List<Personal> Lista = new List<Personal>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select id_personal,nombre,puesto from tbl_personal", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Personal()
                            {
                                id_personal = Convert.ToInt32(dr["id_personal"]),
                                nombre = dr["nombre"].ToString(),
                                puesto = dr["puesto"].ToString(),
                                
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Personal>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Personal where id_personal = @id", oConexion);
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