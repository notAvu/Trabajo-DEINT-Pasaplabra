using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Listados;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraBL.Listados
{
    public class clsListadosPartidaBL
    {
        /// <summary>
        /// Comunica la DAL con la UI para cargar un listado de partidas contenida en la Base de Datos
        /// </summary>
        /// <returns>Devuelve el listado de clsPartida</returns>
        public static List<clsPartida> CargarListadoPartidaBL()
        {
            return clsListadosPartidaDAL.CargarListadoPartidaDAL();
        }
    }
}


