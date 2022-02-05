﻿using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraUI.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsGamePageVM : clsVMBase
    {
        #region propiedades privadas
        private List<clsModelPregunta> listadoPreguntas;
        private clsModelPregunta preguntaSeleccionada;
        private string txtBoxRespuestaJugador;
        private DispatcherTimer tiempo;
        private DelegateCommand checkRespuestaCommand;
        private DelegateCommand saltarPregunta;
        #endregion
        #region constructor por defecto
        public clsGamePageVM()
        {
            cargarListadoPreguntas();
            preguntaSeleccionada = listadoPreguntas[0];
            recargarPregunta(preguntaSeleccionada);
            TiempoMax = 300;
            NotifyPropertyChanged("TiempoMax");
            iniciarContador();
            TxtBoxRespuestaJugador = "";
        }

        #endregion
        #region propiedades publicas
        public string TxtBoxRespuestaJugador
        {
            get { return txtBoxRespuestaJugador; }
            set
            {
                txtBoxRespuestaJugador = value;
                if (checkRespuestaCommand != null)
                {
                    checkRespuestaCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string TxtBoxEnunciadoPregunta { get; set; }
        public string TxtBoxLetraPregunta { get; set; }
        public DispatcherTimer TiempoRestante { get => tiempo; set => tiempo = value; }
        public int Aciertos{ get; set; }
        public int Fallos { get; set; }
        public int PalabrasRestantes { get; set; }

        public int TiempoMax { get; set; }
        public DelegateCommand CheckRespuestaCommand
        {
            get
            {
                return checkRespuestaCommand = new DelegateCommand(CheckRespuestaCommand_Execute, CheckRespuesta_CanExecute);
            }
        }

        public clsModelPregunta PreguntaSeleccionada { get => preguntaSeleccionada; set => preguntaSeleccionada = value; }
        public List<clsModelPregunta> ListadoPreguntas { get => listadoPreguntas; set => listadoPreguntas = value; }
        #endregion
        #region commands
        private bool CheckRespuesta_CanExecute()
        {
            return preguntaSeleccionada != null && !string.IsNullOrWhiteSpace(TxtBoxRespuestaJugador);
        }

        private void CheckRespuestaCommand_Execute()
        {
            PreguntaSeleccionada.Estado = TxtBoxRespuestaJugador == PreguntaSeleccionada.Respuesta ? 1 : -1;//TODO preguntarle a fernando lo del bool?=null como estado por defecto
            preguntaSeleccionada = listadoPreguntas.Where(pregunta => pregunta.Estado == 0 &&
               true).FirstOrDefault();
            switch (PreguntaSeleccionada.Estado)//TODO MODURALIZAR
            {
                case 1:
                    Aciertos++;
                    NotifyPropertyChanged("Aciertos");
                    break;
                case -1:
                    Fallos++;
                    NotifyPropertyChanged("Fallos");
                    break;
                default:
                    PalabrasRestantes++;
                    NotifyPropertyChanged("PalabrasRestantes");
                    break;
            }
            //TODO METER AQUI LO QUE HARIA SI HA TERMINADO EL ROSCO
            recargarPregunta(preguntaSeleccionada);
        }
        #endregion
        #region metodos auxiliares
        private void cargarListadoPreguntas()
        {
            //clsListadosPreguntaBL.CargarListadoPreguntaBL().ForEach(pregunta => listadoPreguntas.Add(new clsModelPregunta(0, pregunta.Id, pregunta.Enunciado, pregunta.Respuesta)));
            listadoPreguntas = dameAlgo();
        }

        /// <summary>
        /// Método que elimina las tildes de la cadena que entra como parámetro y devuelve la misma cadena pero sin las tildes
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Devuelve asociado al nombre una cadena sin tildes</returns>
        private static string sinTildes(string texto) =>
            new String(
                texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
            )
            .Normalize(NormalizationForm.FormC);

        private void recargarPregunta(clsModelPregunta preguntaSeleccionada)
        {
            TxtBoxEnunciadoPregunta = preguntaSeleccionada.Enunciado;
            NotifyPropertyChanged("TxtBoxEnunciadoPregunta");
            TxtBoxLetraPregunta = preguntaSeleccionada.Letra.ToString();
            NotifyPropertyChanged("TxtBoxLetraPregunta");
        }

        private void iniciarContador()
        {
            //TODO MODURALIZAR Y DISCUTIR QUE QUEREMOS QUE SE MUESTRE
            ContentDialog contentDialogPartidaTerminada = new ContentDialog
            {
                Title = "Se te ha acabado el tiempo :(",
                Content = "",
                PrimaryButtonText = "Joder que malo soy",
                CloseButtonText = "",
                DefaultButton = ContentDialogButton.Primary
            };

            tiempo = new DispatcherTimer();
            tiempo.Interval = new TimeSpan(0, 0, 1);
            tiempo.Start();
            tiempo.Tick += (a, b) =>
            {
                TiempoMax--;
                NotifyPropertyChanged("TiempoMax");
                if (TiempoMax == 0)
                {
                    contentDialogPartidaTerminada.ShowAsync();
                    tiempo.Stop();
                }
            };
        }
        #endregion

        private List<clsModelPregunta> dameAlgo()
        {
            List<clsModelPregunta> a = new List<clsModelPregunta>();
            for (int i = 0; i < 10; i++)
            {
                a.Add(new clsModelPregunta(0, i, i.ToString(), i.ToString(), 'A'));
            }
            return a;
        }

    }
}
