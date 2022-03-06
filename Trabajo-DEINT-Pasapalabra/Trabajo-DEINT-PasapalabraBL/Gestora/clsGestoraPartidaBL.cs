using Trabajo_DEINT_PasapalabraDAL.Gestora;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraBL.Gestora
{
    public class clsGestoraPartidaBL
    {
        public static int insertarPartidaBL(clsPartida oPartida)
        {
            return clsGestoraPartidaDAL.insertarPartidaDAL(oPartida);
        }
    }
}
