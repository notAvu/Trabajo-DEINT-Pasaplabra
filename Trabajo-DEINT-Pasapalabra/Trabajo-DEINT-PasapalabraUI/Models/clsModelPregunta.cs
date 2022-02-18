using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraEntities;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Trabajo_DEINT_PasapalabraUI.Models
{
    public class clsModelPregunta : clsPregunta
    {
        private int estado;
        #region propiedadesAutoimplementadas
        public int Estado { get => estado; set { estado = value; EvaluarColor(); } }

        private void EvaluarColor()
        {
            if (estado == 1)
            {
                Color.Color = Colors.Green;
            }
            else if (estado == -1)
            {
                Color.Color = Colors.Red;
            }
        }

        public SolidColorBrush Color { get; set; }

        #endregion
        #region constructores
        public clsModelPregunta(int estado, int id, string pregunta, string respuesta, char letra) :
            base(id, pregunta, respuesta, letra)
        {
            Estado = estado;
            Color = new SolidColorBrush(Colors.Blue);
        }

        public clsModelPregunta(int estado, int id, string enunciado, string respuesta) : base(id, enunciado, respuesta)
        {
            Estado = estado;
            Color = new SolidColorBrush(Colors.Blue);
        }
        #endregion
        #region animaciones
        public void SelectedAnimation() {
            
        }
        #endregion
    }
}
