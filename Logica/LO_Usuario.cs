using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NetRodhe2.Models;
using System.Data.SqlClient;
using System.Data;

namespace NetRodhe2.Logica
{
    public class LO_Usuario
    {
        public Usuarios EncontrarUsuario ( string usuario, string contraseña)
        {
            Usuarios objeto = new Usuarios();
            using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FIVRKJB ; Initial Catalog=NetRodhe2; Integrated Security=true"))
            {
                string query = "SELECT nombres, correo, contraseña, usuario,id_rol FROM tbl_usuarios WHERE usuario = @pusuario AND contraseña = @pcontraseña";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pusuario", usuario);
                cmd.Parameters.AddWithValue("@pcontraseña", contraseña);
                cmd.CommandType = CommandType.Text;


                conexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objeto = new Usuarios()
                        {
                            Nombres = dr["nombres"].ToString(),
                            Correo = dr["correo"].ToString(),
                            Contraseña = dr["contraseña"].ToString(),
                            Usuario = dr["usuario"].ToString(),
                            IdRol = (Rol)dr["id_rol"],
                        };
                    }
                }
            }
                
                return objeto;


        }

    }
}