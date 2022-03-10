using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadSelectDAL;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadBaseDAL;
using Trabajo_DEINT_PasapalabraEntities;


namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPartidaDAL
    {
        /// <summary>
        /// Carga un listado de partidas contenida en la Base de Datos.
        /// </summary>
        /// <returns>Devuelve una Lista de objetos de tipo clsPartida</returns>
        public static List<clsPartida> CargarListadoPartidaDAL()
        {
            instanciarConexion();
            List<clsPartida> listadoPartidas = new List<clsPartida>();
            ejecutarSelect("SELECT * FROM Partidas ORDER BY(aciertos - fallos) desc, tiempo ASC");
            if (MiLector.HasRows) {
                while (MiLector.Read())
            {

                    listadoPartidas.Add(getPartida(MiLector));
                }
            }
            cerrarFlujos();
            return listadoPartidas;
        }


        /// <summary>
        /// Metodo privado que lee los datos provenientes de la Base de Datos y los introduce en un objeto de tipo clsPartida.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Devuelve un objeto de tipo clsPartida</returns>
        private static clsPartida getPartida(SqlDataReader reader)
        {
            clsPartida oPartida;
            oPartida = new clsPartida();
            oPartida.Id = (int)reader["ID"];
            if (reader["Nickname"] != DBNull.Value) { oPartida.Nick = (string)reader["Nickname"]; }
            oPartida.TotalAcertadas = (int)reader["aciertos"];
            oPartida.TotalFalladas = (int)reader["fallos"];
            oPartida.Puntuacion = (int)reader["puntuacion"];
            oPartida.Tiempo = (TimeSpan)reader["tiempo"];
            return oPartida;
        }
    }
}


