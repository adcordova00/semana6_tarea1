using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.config;
using System.Data;
using System.Data.SqlClient;

namespace Login.Models
{
    class UsuariosModel
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }

        public UsuariosModel() { }

        public static UsuariosModel Insertar(UsuariosModel usuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO usuario (nombre_usuario, password, roles) " +
                                   "OUTPUT INSERTED.ID, INSERTED.nombre_usuario, INSERTED.password, INSERTED.roles " +
                                   "VALUES (@NombreUsuario, @Password, @Roles)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        comando.Parameters.AddWithValue("@Password", usuario.Password);
                        comando.Parameters.AddWithValue("@Roles", usuario.Roles);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new UsuariosModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Roles = lector["roles"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar el usuario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el usuario.");
            }
            return null;
        }

        public static string Actualizar(UsuariosModel usuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE usuario SET nombre_usuario = @NombreUsuario, password = @Password, " +
                                   "roles = @Roles WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", usuario.ID.ToString());
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario.ToString());
                        comando.Parameters.AddWithValue("@Password", usuario.Password.ToString());
                        comando.Parameters.AddWithValue("@Roles", usuario.Roles.ToString());
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar el usuario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar el usuario.");
                return "Error";
            }
        }

        public static string Eliminar(int idUsuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM usuario WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idUsuario);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar el usuario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar el usuario.");
                return "Error";
            }
        }

        public static UsuariosModel ObtenerPorId(int idUsuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM usuario WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idUsuario);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new UsuariosModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Roles = lector["roles"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener el usuario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener el usuario.");
            }
            return null;
        }

        public static List<UsuariosModel> ObtenerTodos()
        {
            var usuarios = new List<UsuariosModel>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM usuario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                usuarios.Add(new UsuariosModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Roles = lector["roles"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de usuarios.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de usuarios.");
            }

            return usuarios;
        }
        public static UsuariosModel Autenticar(string nombreUsuario, string password)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    string consulta = "SELECT * FROM usuario WHERE nombre_usuario = @NombreUsuario AND password = @Password";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        comando.Parameters.AddWithValue("@Password", password);
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {

                                return new UsuariosModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Roles = lector["roles"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de SQL al autenticar el usuario: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar el usuario: " + ex.Message);
            }
        }
        public  UsuariosModel OtroAutenticar(string nombreUsuario, string password)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    string consulta = "SELECT * FROM usuario WHERE nombre_usuario ='" + nombreUsuario + "'";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                if (lector["password"].ToString() == password)
                                {
                                    return new UsuariosModel
                                    {
                                        ID = Convert.ToInt32(lector["ID"]),
                                        NombreUsuario = lector["nombre_usuario"].ToString(),
                                        Password = lector["password"].ToString(),
                                        Roles = lector["roles"].ToString()
                                    };
                                }
                                else {
                                    return null;
                                }

                               
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de SQL al autenticar el usuario: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar el usuario: " + ex.Message);
            }
        }

    }
}
