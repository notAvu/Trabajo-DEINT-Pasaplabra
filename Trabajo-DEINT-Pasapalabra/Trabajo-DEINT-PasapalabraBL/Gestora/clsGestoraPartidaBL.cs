using Trabajo_DEINT_PasapalabraDAL.Gestora;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraBL.Gestora
{
    public class clsGestoraPartidaBL
    {
        /// <summary>
        /// Inserta un objeto de tipo clsPartida en la Base de Datos
        /// </summary>
        /// <param name="oPartida"></param>
        /// <returns>Devuelve un entero con el número de filas afectadas</returns>
        public static int insertarPartidaBL(clsPartida oPartida)
        {
            return clsGestoraPartidaDAL.insertarPartidaDAL(oPartida);
        }
    }
}
