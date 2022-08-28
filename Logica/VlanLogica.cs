using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using NetRodhe2.Models;

namespace NetRodhe2.Logica
{
    public class VlanLogica
    {
        private static VlanLogica instancia = null;

        public VlanLogica()
        {

        }

        public static VlanLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VlanLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Vlan objetoVlan)
        {
            bool respuesta = true;
            using (SqlConnection objetoConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarVlan", objetoConexion);
                    cmd.Parameters.AddWithValue("no_vlan", objetoVlan.no_vlan);
                    cmd.Parameters.AddWithValue("nombre_vlan", objetoVlan.nombre_vlan);
                    cmd.Parameters.AddWithValue("ip_subred", objetoVlan.ip_subred);
                    cmd.Parameters.AddWithValue("gw_vlan", objetoVlan.gw_vlan);
                    cmd.Parameters.AddWithValue("sub_net_mask", objetoVlan.sub_net_mask);
                    cmd.Parameters.AddWithValue("ip_minima", objetoVlan.ip_minima);
                    cmd.Parameters.AddWithValue("ip_maxima", objetoVlan.ip_maxima);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    objetoConexion.Open();

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

        public bool Modificar(Vlan objetoVlan)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarVlan", oConexion);
                    cmd.Parameters.AddWithValue("id_vlan", objetoVlan.id_vlan);
                    cmd.Parameters.AddWithValue("no_vlan", objetoVlan.no_vlan);
                    cmd.Parameters.AddWithValue("nombre_vlan", objetoVlan.nombre_vlan);
                    cmd.Parameters.AddWithValue("ip_subred", objetoVlan.ip_subred);
                    cmd.Parameters.AddWithValue("gw_vlan", objetoVlan.gw_vlan);
                    cmd.Parameters.AddWithValue("sub_net_mask", objetoVlan.sub_net_mask);
                    cmd.Parameters.AddWithValue("ip_minima", objetoVlan.ip_minima);
                    cmd.Parameters.AddWithValue("ip_maxima", objetoVlan.ip_maxima);
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

        public List<Vlan> Listar()
        {
            List<Vlan> Lista = new List<Vlan>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select id_vlan,no_vlan,nombre_vlan,ip_subred,gw_vlan,sub_net_mask,ip_minima,ip_maxima from tbl_vlans", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Vlan()
                            {
                                id_vlan = Convert.ToInt32(dr["id_vlan"]),
                                no_vlan = Convert.ToInt32(dr["no_vlan"]),
                                nombre_vlan = dr["nombre_vlan"].ToString(),
                                ip_subred = dr["ip_subred"].ToString(),
                                gw_vlan = dr["gw_vlan"].ToString(),
                                sub_net_mask = dr["sub_net_mask"].ToString(),
                                ip_minima = dr["ip_minima"].ToString(),
                                ip_maxima = dr["ip_maxima"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Vlan>();
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
                    SqlCommand cmd = new SqlCommand("delete from tbl_vlan where id_vlan = @id", oConexion);
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