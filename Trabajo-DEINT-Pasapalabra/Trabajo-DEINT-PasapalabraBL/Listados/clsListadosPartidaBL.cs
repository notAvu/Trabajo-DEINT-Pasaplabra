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
        public static List<clsPartida> CargarListadoPartidaBL()
        {
            return clsListadosPartidaDAL.CargarListadoPartidaDAL();
        }
    }
}


