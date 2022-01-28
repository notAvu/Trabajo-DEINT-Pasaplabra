using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsGamePageVM
    {
        #region atributos

        #endregion
        #region propiedades publicas
        public List<clsPregunta> ListadoPreguntas{ get; set; }
        #endregion
        #region constructores
        public clsGamePageVM()
        {
            //ListadoPreguntas = clsListadosPreguntaBL.CargarListadoPreguntaBL();
            //TODO BINDEAR EL LISTADO CON ITEMS SOURCE DEL LIST BOX
        }
        #endregion

    }
}
