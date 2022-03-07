using System;
using System.Collections.Generic;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Gestora
{
    public class clsGestoraPartidaDAL : clsUtilidadDMLDAL
    {
        /// <summary>
        /// Inserta una partida jugada nueva en la Base de Datos.
        /// Precondiciones: el objeto partida no debe ser null.
        /// </summary>
        /// <param name="partida">Recibe por parámtro un objeto de tipo clsPartida</param>
        /// <returns>Devuelve un entero con el número de filas afectadas</returns>
        public static int insertarPartidaDAL(clsPartida partida)
        {
            int filasAfectadas;
            instanciarConexion();
            anhiadirParametros(partida);
            filasAfectadas = ejecutarSentenciaDML("Insert into Partidas values(@Nickname,@aciertos,@fallos,@tiempo)");
            MiConexion.closeConnection();
            return filasAfectadas;
        }

        /// <summary>
        /// Metodo privado que añade los parámetros del objeto clsPartida a la consulta INSERT
        /// </summary>
        /// <param name="partida">un objeto de tipo clsPartida</param>
        private static void anhiadirParametros(clsPartida partida)
        {
            MiComando.Parameters.Add("@Nickname", System.Data.SqlDbType.VarChar).Value = partida.Nick;
            MiComando.Parameters.Add("@aciertos", System.Data.SqlDbType.Int).Value = partida.TotalAcertadas;
            MiComando.Parameters.Add("@fallos", System.Data.SqlDbType.Int).Value = partida.TotalFalladas;
            MiComando.Parameters.Add("@tiempo", System.Data.SqlDbType.Time).Value = partida.Tiempo;
        }

    }
}
