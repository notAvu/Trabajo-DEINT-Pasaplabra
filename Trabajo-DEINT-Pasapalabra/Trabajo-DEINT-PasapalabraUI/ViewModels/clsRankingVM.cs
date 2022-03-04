using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsRankingVM
    {
        #region propiedades privadas
        private List<clsPartida> listaPartidas;
        #endregion

        #region constructor por defecto
        public clsRankingVM()
        {
            listaPartidas = clsListadosPartidaBL.CargarListadoPartidaBL();
        }
        #endregion

        #region parametros
        public List<clsPartida> ListaPartidas { get => listaPartidas; set => listaPartidas = value; }
        #endregion
    }
}
