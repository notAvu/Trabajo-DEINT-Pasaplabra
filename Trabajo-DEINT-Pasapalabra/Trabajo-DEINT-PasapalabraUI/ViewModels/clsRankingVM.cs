using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsRankingVM : clsVMBase
    {
        List<clsPartida> ListaPartidas { get; set; }
        public clsRankingVM()
        {
            ListaPartidas = clsListadosPartidaBL.CargarListadoOrdenadoBL();
        }
    }
}
