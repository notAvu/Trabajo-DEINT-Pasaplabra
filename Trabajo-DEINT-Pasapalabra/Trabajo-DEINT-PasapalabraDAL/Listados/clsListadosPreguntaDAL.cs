using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadBaseDAL;
using static Trabajo_DEINT_PasapalabraDAL.Utilidades.clsUtilidadSelectDAL;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraDAL.Utilidades;
using System.Data;

namespace Trabajo_DEINT_PasapalabraDAL.Listados
{
    public class clsListadosPreguntaDAL
    {
        /// <summary>
        /// Carga un listado de preguntas contenida en la Base de Datos.
        /// </summary>
        /// <returns>Devuelve una Lista de objetos de tipo clsPregunta</returns>
        public static List<clsPregunta> CargarListadoPreguntaDAL()
        {
            List<clsPregunta> listadoPregunta = new List<clsPregunta>();
            instanciarConexion();

            SqlDataAdapter da = new SqlDataAdapter();//TODO MODURALIZAR
            da.SelectCommand = new SqlCommand("PalabrasJugada", MiConexion.Conexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            da.Fill(ds, "tabla_preguntas");

            DataTable dt = ds.Tables["tabla_preguntas"];//TODO DUDA SI SON FLUJOS Y HAY QUE CERRARLOS, YO CREO QUE NO, GERMAN

            foreach (DataRow row in dt.Rows)
            {
                char letra = Convert.ToChar((String)row["letra"]);
                clsPregunta oPregunta = new clsPregunta((int)row["IdPreguntas"],
                                            (string)row["enunciado"],
                                            (string)row["respuesta"],
                                            letra);
                listadoPregunta.Add(oPregunta);
            }
            MiConexion.Conexion.Close();
            return listadoPregunta;
        }


        /// <summary>
        /// Metodo privado que lee los datos provenientes de la Base de Datos y los introduce en un objeto de tipo clsPregunta.
        /// </summary>
        /// <returns>Devuelve un objeto de tipo clsPregunta</returns>
        private static clsPregunta getPregunta()
        {
            clsPregunta oPregunta;
            char letra = Convert.ToChar((string)MiLector["letra"]);
            oPregunta = new clsPregunta((int)MiLector["idPreguntas"],
                                        (string)MiLector["enunciado"],
                                        (string)MiLector["respuesta"],
                                        letra);
            return oPregunta;
        }

    }
}
