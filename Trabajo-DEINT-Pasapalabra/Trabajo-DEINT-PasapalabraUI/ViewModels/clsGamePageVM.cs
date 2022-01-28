using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraUI.Models;
using Windows.UI.Xaml;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsGamePageVM:clsVMBase
    {
        #region propiedades privadas
        private List<clsModelPregunta> listadoPreguntas;
        private clsModelPregunta preguntaSeleccionada;
        private int aciertos;
        private int fallos;
        private int tiempoMax;
        private string respuestaJugador;
        private DispatcherTimer tiempo;
        private DelegateCommand checkRespuesta;
        private DelegateCommand saltarPregunta;
        #endregion
        #region constructor
        public clsGamePageVM()
        {
            cargarListadoPreguntas();
            preguntaSeleccionada = listadoPreguntas[0];
            aciertos = 0;
            fallos = 0;
            tiempoMax = 300;
            tiempo = new DispatcherTimer();
            tiempo.Interval = new TimeSpan(0, 0, 1);
            tiempo.Tick += (a, b) =>
            {
                TiempoMax--;
                NotifyPropertyChanged("TiempoMax");
            };
            respuestaJugador = "";
        }
        #endregion
        #region propiedades publicas
        public string RespuestaJugador { get => respuestaJugador; set => respuestaJugador = value; }
        public DispatcherTimer TiempoRestante { get => tiempo; set => tiempo = value; }
        public int Aciertos { get => aciertos; set => aciertos = value; }
        public int Fallos { get => fallos; set => fallos = value; }
        public int TiempoMax { get => tiempoMax; set => tiempoMax = value; }
        public DelegateCommand CheckRespuesta { get => new DelegateCommand(CheckRespuesta_Execute, CheckRespuesta_CanExecute); }

        private bool CheckRespuesta_CanExecute()
        {
            return preguntaSeleccionada != null && !string.IsNullOrEmpty(respuestaJugador);
        }

        private void CheckRespuesta_Execute()
        {
            PreguntaSeleccionada.Estado = respuestaJugador == PreguntaSeleccionada.Respuesta ? 1 : -1;//TODO preguntarle a fernando lo del bool?=null como estado por defecto
        }

        public clsModelPregunta PreguntaSeleccionada { get => preguntaSeleccionada; set => preguntaSeleccionada = value; }
        public List<clsModelPregunta> ListadoPreguntas { get => listadoPreguntas; set => listadoPreguntas = value; }
        #endregion
        #region metodos auxiliares
        private void cargarListadoPreguntas()
        {
            clsListadosPreguntaBL.CargarListadoPreguntaBL().ForEach(pregunta => listadoPreguntas.Add(new clsModelPregunta(0, pregunta.Id, pregunta.Enunciado, pregunta.Respuesta)));
        }
        #endregion

    }
}
