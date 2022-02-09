using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadBaseDAL;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadSelectDAL;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;

namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPregunta
    {
        public static List<clsPregunta> CargarListadoPreguntaDAL()
        {
            instanciarConexion();
            List<clsPregunta> listadoPregunta = new List<clsPregunta>();
            MiComando = new SqlCommand("PalabrasJugada", MiConexion.Conexion);
            ejecutarSelect("SELECT TOP 20 * FROM Preguntas ORDER BY RAND() GROUP BY letra");
            //TODO CAMBIAR INSTRUCCION
            while (MiLector.HasRows)   
            {
                MiLector.Read();
                listadoPregunta.Add(getPregunta());
            }
            cerrarFlujos();
            return listadoPregunta;
        }



        private static clsPregunta getPregunta()
        {
            clsPregunta oPregunta;
            char letra = Convert.ToChar((string)MiLector["letra"]);
            oPregunta = new clsPregunta((int)MiLector["idPreguntas"],
                                        (string)MiLector["enunciado"],
                                        (string)MiLector["respuesta"],
                                        letra);//TODO NO DEJABA CONVERSION EXPLICITA
            return oPregunta;
        }

    }
}
