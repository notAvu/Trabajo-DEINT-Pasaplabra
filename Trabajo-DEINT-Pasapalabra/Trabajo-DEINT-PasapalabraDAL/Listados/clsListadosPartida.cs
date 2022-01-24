using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPartida
    {
        public static List<clsPartida> CargarListadoPartidaDAL()
        {
            clsUtilidadBaseDAL.instanciarConexion();
            List<clsPartida> listadoPartidas = new List<clsPartida>();
            clsUtilidadSelectDAL.ejecutarSelect("SELECT*FROM Partida");
            while (clsUtilidadSelectDAL.MiLector.HasRows)
            {
                clsUtilidadSelectDAL.MiLector.Read();

                listadoPartidas.Add(getPartida(clsUtilidadSelectDAL.MiLector));
            }
            clsUtilidadSelectDAL.cerrarFlujos();
            return listadoPartidas;
        }



        private static clsPartida getPartida(SqlDataReader reader)
        {
            clsPartida oPartida;
            oPartida = new clsPartida();
            oPartida.Id = (int)reader["ID"];
            if (reader["Nickname"] != DBNull.Value) { oPartida.Nick = (string)reader["Nickname"]; }
            oPartida.TotalAcertadas = (int)reader["aciertos"];
            oPartida.TotalFalladas = (int)reader["fallos"];
            oPartida.Tiempo = (DateTime)reader["tiempo"];
            return oPartida;
        }
    }
}


