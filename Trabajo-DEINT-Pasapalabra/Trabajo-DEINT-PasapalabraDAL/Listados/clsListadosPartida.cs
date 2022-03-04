using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadSelectDAL;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadBaseDAL;
using Trabajo_DEINT_PasapalabraEntities;


namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPartida
    {
        public static List<clsPartida> CargarListadoPartidaDAL()
        {
            instanciarConexion();
            List<clsPartida> listadoPartidas = new List<clsPartida>();
            ejecutarSelect("SELECT * FROM Partidas ORDER BY puntuacion DESC, tiempo");
            if (MiLector.HasRows)
            {
                while (MiLector.Read())
                {
                    listadoPartidas.Add(getPartida(MiLector));
                }
            }
            
            cerrarFlujos();
            return listadoPartidas;
        }



        private static clsPartida getPartida(SqlDataReader reader)
        {
            clsPartida oPartida = new clsPartida();
            oPartida.Id = (int)reader["ID"];
            if (reader["Nickname"] != DBNull.Value) { oPartida.Nick = (string)reader["Nickname"]; }
            oPartida.TotalAcertadas = (int)reader["aciertos"];
            oPartida.TotalFalladas = (int)reader["fallos"];
            oPartida.Tiempo = (TimeSpan)reader["tiempo"];
            oPartida.Puntuacion = (int)reader["puntuacion"];
            return oPartida;
        }
    }
}


