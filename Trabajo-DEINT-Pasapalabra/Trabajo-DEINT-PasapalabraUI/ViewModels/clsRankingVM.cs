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
        public List<clsPartida> ListaPartidas { get; set; }
        public clsRankingVM()
        {
            ListaPartidas = clsListadosPartidaBL.CargarListadoPartidaBL();
        }
    }
}
