using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetRodhe2.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace NetRodhe2.Logica
{
    public class DispositivosLogica
    {
        private static DispositivosLogica instancia = null;

        public DispositivosLogica()
        {

        }

        public static DispositivosLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DispositivosLogica();
                }

                return instancia;
            }
        }

        //crear una tabla para insertar errores de todo tipo
        //id erorr, id del usuario, mensaje del error, fecha, seccion del programa


        public List<Dispositivos> Listar()
        {
            List<Dispositivos> rptListaDispositivo = new List<Dispositivos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select d.id_dispositivos,d.nombre_dispositivos ,d.mac_add,");
                sb.AppendLine("a.abrev_area,");
                sb.AppendLine("v.no_vlan,");
                sb.AppendLine("d.ip_add,");
                sb.AppendLine("p.nombre,");
                sb.AppendLine("d.admin_wireless_router,");
                sb.AppendLine("d.passwordrouter,");
                sb.AppendLine("d.nombre_en_telefono");
                sb.AppendLine("from tbl_dispositivos d");
                sb.AppendLine("inner join tbl_area a on a.id_area = d.id_area");
                sb.AppendLine("inner join tbl_vlans v on v.id_vlan = d.id_vlan");
                sb.AppendLine("inner join tbl_personal p on p.id_personal = d.id_personal");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaDispositivo.Add(new Dispositivos()
                        {
                            id_dispositivos = Convert.ToInt32(dr["id_dispositivos"]),
                            nombre_dispositivos = dr["nombre_dispositivos"].ToString(),
                            mac_add = dr["mac_add"].ToString(),
                            IArea = new Area() {  abrev_area = dr["abrev_area"].ToString() },
                            IVlan = new Vlan() { no_vlan = Convert.ToInt32(dr["no_vlan"].ToString()) },
                            ip_add = dr["ip_add"].ToString(),
                            IPersonal = new Personal() { nombre = dr["nombre"].ToString() },
                            admin_wireless_router = dr["admin_wireless_router"].ToString(),
                            passwordrouter= dr["passwordrouter"].ToString(),
                            nombre_en_telefono= dr["nombre_en_telefono"].ToString(),
                            
                        });
                    }
                    dr.Close();
                    return rptListaDispositivo;

                }
                catch (Exception ex)
                {
                    rptListaDispositivo = null;
                    return rptListaDispositivo;
                }
                
            }
        }
        public int Registrar(Dispositivos objeto)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AgregarDispositivo", oConexion);
                    cmd.Parameters.AddWithValue("nombre_dispositivos", objeto.nombre_dispositivos);
                    cmd.Parameters.AddWithValue("mac_add", objeto.mac_add);
                    cmd.Parameters.AddWithValue("id_area", objeto.IArea.id_area);
                    cmd.Parameters.AddWithValue("id_vlan", objeto.IVlan.id_vlan);
                    cmd.Parameters.AddWithValue("ip_add", objeto.ip_add);
                    cmd.Parameters.AddWithValue("id_personal", objeto.IPersonal.id_personal);
                    cmd.Parameters.AddWithValue("admin_wireless_router", objeto.admin_wireless_router);
                    cmd.Parameters.AddWithValue("passwordrouter", objeto.passwordrouter);
                    cmd.Parameters.AddWithValue("nombre_en_telefono", objeto.nombre_en_telefono);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }
        public bool Modificar(Dispositivos objeto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarDispositivo", oConexion);
                    cmd.Parameters.AddWithValue("id_dispositivos", objeto.id_dispositivos);
                    cmd.Parameters.AddWithValue("nombre_dispositivos", objeto.nombre_dispositivos);
                    cmd.Parameters.AddWithValue("mac_add", objeto.mac_add);
                    cmd.Parameters.AddWithValue("IArea", objeto.IArea.id_area);
                    cmd.Parameters.AddWithValue("IVlan", objeto.IVlan.id_vlan);
                    cmd.Parameters.AddWithValue("ip_add", objeto.ip_add);
                    cmd.Parameters.AddWithValue("IPersonal", objeto.IPersonal.id_personal);
                    cmd.Parameters.AddWithValue("admin_wireless_router", objeto.admin_wireless_router);
                    cmd.Parameters.AddWithValue("passwordrouter", objeto.passwordrouter);
                    cmd.Parameters.AddWithValue("nombre_en_telefono", objeto.nombre_en_telefono);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from tbl_dispositivos where id_dispositivos = @id_dispositivos", oConexion);
                    cmd.Parameters.AddWithValue("@id_dispositivos", id);
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