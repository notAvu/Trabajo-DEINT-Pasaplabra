using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraEntities;

namespace Trabajo_DEINT_PasapalabraUI.Models
{
    class clsModelPregunta : clsPregunta
    {
        #region propiedadesAutoimplementadas
        public int Estado { get; set; }

        #endregion
        #region constructores
<<<<<<< HEAD
        public clsModelPregunta(int estado, int id, string pregunta, string respuesta, char letra) :
            base(id, pregunta, respuesta,letra)
=======
        public clsModelPregunta(int estado,int id,string pregunta, string respuesta):base(id,pregunta,respuesta)
>>>>>>> 6172b6dd6902474b2791390465b0ccde7467fb66
        {
            Estado = estado;
        }
        #endregion
    }
}
