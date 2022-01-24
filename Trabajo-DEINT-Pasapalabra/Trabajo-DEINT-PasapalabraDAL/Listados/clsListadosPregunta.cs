using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPregunta
    {
        public static List<clsPregunta> CargarListadoPreguntaDAL()
        {
            clsUtilidadBaseDAL.instanciarConexion();
            List<clsPregunta> listadoPregunta = new List<clsPregunta>();
            clsUtilidadSelectDAL.ejecutarSelect("SELECT TOP 20 FROM Pregunta ORDER BY RAND()");
            while (clsUtilidadSelectDAL.MiLector.HasRows)
            {
                clsUtilidadSelectDAL.MiLector.Read();

                listadoPregunta.Add(getPregunta(clsUtilidadSelectDAL.MiLector));
            }
            clsUtilidadSelectDAL.cerrarFlujos();
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
