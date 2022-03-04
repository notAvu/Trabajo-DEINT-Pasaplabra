using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraEntities;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Trabajo_DEINT_PasapalabraUI.Models
{
    public class clsModelPregunta : clsPregunta, INotifyPropertyChanged
    {
        private int estado;
        private bool animado;
        #region propiedadesAutoimplementadas
        public int Estado
        {
            get => estado;
            set
            {
                estado = value;
                NotifyPropertyChanged("Estado");
            }

        }

        public bool Animado
        {
            get { return animado; }
            set
            {
                animado = value;
                NotifyPropertyChanged("Animado");
            }
        }

        #endregion
        #region constructores
        public clsModelPregunta() { }
        public clsModelPregunta(int estado, int id, string pregunta, string respuesta, char letra) :
            base(id, pregunta, respuesta, letra)
        {
            Estado = estado;
            Animado = false;
        }

        public clsModelPregunta(int estado, int id, string pregunta, string respuesta, char letra, bool animado) :
            base(id, pregunta, respuesta, letra)
        {
            Estado = estado;
            if (Animado)
            {
                Animado = false;
            }
            else
            {
                Animado = true;
            }
        }

        public clsModelPregunta(int estado, int id, string enunciado, string respuesta) : base(id, enunciado, respuesta)
        {
            Estado = estado;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region animaciones
        public void SelectedAnimation()
        {

        }
        #endregion
    }
}
