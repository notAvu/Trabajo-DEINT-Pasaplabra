using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraUI.Models
{
    class clsModelPregunta : clsPartida
    {
        #region propiedadesAutoimplementadas
        public int Estado { get; set; }
        #endregion
        #region constructores
        public clsModelPregunta(int estado, int id, string nick, int totalAcertadas, int totalFalladas, DateTime tiempo
            ) :base(id,nick,totalAcertadas,totalFalladas,tiempo )
        {
            Estado = estado;
        }
        #endregion
    }
}
