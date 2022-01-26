using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraUI.Models;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    class clsGameVM : clsVMBase
    {
        #region propiedades privadas
        List<clsModelPregunta> listadoPreguntas;
        clsModelPregunta preguntaSeleccionada;//mejor indice seleccionado?
        int aciertos;
        int fallos;
        string respuestaJugador;
        DateTime tiempoRestante;//TODO TIEMPO EN SEGUNDOS O CON MINUTERO?
        #endregion
        #region constructor
        public clsGameVM()
        {
            listadoPreguntas = new List<clsModelPregunta>();
            preguntaSeleccionada = listadoPreguntas[0];
            aciertos = 0;
            fallos = 0;
            tiempoRestante = new DateTime();
            respuestaJugador = "";
        }
        #endregion
        #region propiedades publicas
        public string RespuestaJugador { get => respuestaJugador; set => respuestaJugador = value; }
        public DateTime TiempoRestante { get => tiempoRestante; set => tiempoRestante = value; }
        public int Aciertos { get => aciertos; set => aciertos = value; }
        public int Fallos { get => fallos; set => fallos = value; }
        internal clsModelPregunta PreguntaSeleccionada { get => preguntaSeleccionada; set => preguntaSeleccionada = value; }
        internal List<clsModelPregunta> ListadoPreguntas { get => listadoPreguntas; set => listadoPreguntas = value; }
        #endregion
    }
}
