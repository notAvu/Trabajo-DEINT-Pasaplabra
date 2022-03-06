using System;
using System.Collections.Generic;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Gestora
{
    public class clsGestoraPartidaDAL : clsUtilidadDMLDAL
    {

        public static int insertarPartidaDAL(clsPartida partida)
        {
            int filasAfectadas;
            instanciarConexion();
            anhiadirParametros(partida);
            filasAfectadas = ejecutarSentenciaDML("Insert into Partidas values(@Nickname,@aciertos,@fallos,@tiempo)");
            MiConexion.closeConnection();
            return filasAfectadas;
        }

        public static void anhiadirParametros(clsPartida partida)
        {
            MiComando.Parameters.Add("@Nickname", System.Data.SqlDbType.VarChar).Value = partida.Nick;
            MiComando.Parameters.Add("@aciertos", System.Data.SqlDbType.Int).Value = partida.TotalAcertadas;
            MiComando.Parameters.Add("@fallos", System.Data.SqlDbType.Int).Value = partida.TotalFalladas;
            MiComando.Parameters.Add("@tiempo", System.Data.SqlDbType.Time).Value = partida.Tiempo;
        }

    }
}
