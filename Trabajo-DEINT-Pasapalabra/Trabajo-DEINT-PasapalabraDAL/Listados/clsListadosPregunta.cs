using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadBaseDAL;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadSelectDAL;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPregunta
    {
        public static List<clsPregunta> CargarListadoPreguntaDAL()
        {
            instanciarConexion();
            List<clsPregunta> listadoPregunta = new List<clsPregunta>();
            ejecutarSelect("SELECT TOP 20 * FROM Preguntas ORDER BY RAND()");
            while (MiLector.HasRows)   
            {
                MiLector.Read();

                listadoPregunta.Add(getPregunta(MiLector));
            }
            cerrarFlujos();
            return listadoPregunta;
        }



        private static clsPregunta getPregunta(SqlDataReader reader)
        {
            clsPregunta oPregunta;
            oPregunta = new clsPregunta();
            oPregunta.Id = (int)reader["ID"];
            oPregunta.Enunciado = (string)reader["Enunciado"];
            oPregunta.Respuesta = (string)reader["Respuesta"];
            return oPregunta;
        }

    }
}
