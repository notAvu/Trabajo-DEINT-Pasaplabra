using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Trabajo_DEINT_PasapalabraDAL.Conexion
{
    public class clsMyConnection
    {
        #region propiedades publicas
        public String Server { get; set; }
        public String DataBase { get; set; }
        public String User { get; set; }
        public String Pass { get; set; }
        public SqlConnection Conexion { get; set; }
        #endregion

        #region constructores
        public ClsMyConnection()
        {
            Server = "servidor-german-sql.database.windows.net";
            DataBase = "PersonasDepartamentosDB";
            User = "gdebustamante";
            Pass = "#Mitesoro";
        }
        //Con parámetros por si quisiera cambiar las conexiones
        public ClsMyConnection(String server, String database, String user, String pass)
        {
            Server = server;
            DataBase = database;
            User = user;
            Pass = pass;
        }
        #endregion




        #region metodos publicos
        /// <summary>
        /// Método que abre una conexión con la base de datos
        /// </summary>
        /// <pre>Nada.</pre>
        /// <returns>Una conexión abierta con la base de datos</returns>
        public void getConnection()
        {
            try
            {
                Conexion = new SqlConnection();
                Conexion.ConnectionString = $"server={Server};database={DataBase};uid={User};pwd={Pass};";
                Conexion.Open();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Este metodo cierra una conexión con la Base de datos
        /// </summary>
        /// <post>La conexion es cerrada</post>
        /// <param name="connection">SqlConnection pr referencia. Conexion a cerrar
        /// </param>
        public void closeConnection()
        {
            try
            {
                Conexion.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }

}
