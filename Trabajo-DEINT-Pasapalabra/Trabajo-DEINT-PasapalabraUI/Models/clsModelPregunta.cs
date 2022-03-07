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
        #region atributos
        private int estado;
        private bool animado;
        #endregion

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

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
