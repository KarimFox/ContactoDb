using System.Data.SqlClient;
using System.Data;
using ContactoDb.Datos;
using ContactoDb.Models;

namespace ContactoDb_ACL.Datos
{
    public class ContactosDatos
    {
        //Método para listar todos los contactos
        public List<ContactoModel> Listar()
        {
            // creo una lista vacia
            var oLista = new List<ContactoModel>();
            // creo una instancia de la clase conexion
            var cn = new Conexion();
            // utilizar using para establecer la cedena de conexion
            using (var conexion = new SqlConnection(cn.CadenaSql()))
            {
                // abrir la conexion
                conexion.Open();
                // comando a ejecutar
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                // decir el tipo de comando
                cmd.CommandType = CommandType.StoredProcedure;
                // leer el resultado de la ejecucion del procedimiento almacenado
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // una vez que se esten leyendo uno a uno, tambien los guardaremos
                        // en la lista
                        oLista.Add(new ContactoModel()
                        {
                            // se utilizan las propiedades de la clase
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        //Método para obtener un contacto
        public ContactoModel ObtenerContacto(int IdContacto)
        {
            // Creo un objeto vacío
            var oContacto = new ContactoModel();
            var cn = new Conexion();
            // Utilizar using para establecer la cadena de conexión
            using (var conexion = new SqlConnection(cn.CadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                // Enviando un parámetro al procedimiento almacenado
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asigno los valores al objeto oContacto
                        oContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        oContacto.Nombre = dr["Nombre"].ToString();
                        oContacto.Telefono = dr["Telefono"].ToString();
                        oContacto.Correo = dr["Correo"].ToString();
                        oContacto.Clave = dr["Clave"].ToString();
                    }
                }
            }
            return oContacto;
        }

        //Método para guardar
        public bool GuardarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utilizar using para establecer la cedena de conexion
                using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    //Enviando un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                    //si no ocurre un erro la variable rpta sera true
                    respuesta = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        //Método de editar es el mismo que el guardar pero añadiendo el parámetro del IdContacto
        public bool EditarContacto(ContactoModel model)
        {
            // Creo una variable boolean
            bool respuesta;
            try
            {
                // Utilizo using para establecer la cadena de conexión
                using (var conexion = new SqlConnection(new Conexion().CadenaSql()))
                {
                    // Abro la conexión
                    conexion.Open();
                    // Creo un SqlCommand para ejecutar el procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    // Envío los parámetros al procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    // Ejecuto el procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                // Si no ocurre un error, la variable respuesta será true
                respuesta = true;
            }
            catch (Exception e)
            {
                // Si ocurre un error, la variable respuesta será false
                respuesta = false;
            }
            // Devuelvo la variable respuesta
            return respuesta;
        }

        //Método de eliminar
        public bool EliminarContacto(ContactoModel contact)
        {
            bool response;

            try
            {
                var connection = new Conexion();

                using (var dbConnection = new SqlConnection(connection.CadenaSql()))
                {
                    dbConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", dbConnection);
                    cmd.Parameters.AddWithValue("IdContacto", contact.IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                response = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                response = false;
            }
            return response;
        }


    }
}