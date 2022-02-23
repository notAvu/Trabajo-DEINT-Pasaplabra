using System;
using System.Collections.Generic;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Gestora
{
    public class clsGestoraPartida
    {

        public static int insertarPartida(clsPartida partida)
        {
            int filasAfectadas;
            clsUtilidadBaseDAL.instanciarConexion();
            anhiadirParametros(partida);
            filasAfectadas = clsUtilidadDMLDAL.ejecutarSentenciaDML("Insert into Partidas values(@Nickname,@aciertos,@fallos,@tiempo)");
            clsUtilidadDMLDAL.MiConexion.closeConnection();
            return filasAfectadas;
        }

        public static void anhiadirParametros(clsPartida partida)
        {
            clsUtilidadDMLDAL.MiComando.Parameters.Add("@Nickname", System.Data.SqlDbType.VarChar).Value = partida.Nick;
            clsUtilidadDMLDAL.MiComando.Parameters.Add("@aciertos", System.Data.SqlDbType.Int).Value = partida.TotalAcertadas;
            clsUtilidadDMLDAL.MiComando.Parameters.Add("@fallos", System.Data.SqlDbType.Int).Value = partida.TotalFalladas;
            clsUtilidadDMLDAL.MiComando.Parameters.Add("@tiempo", System.Data.SqlDbType.Time).Value = partida.Tiempo;
        }

    }
}
