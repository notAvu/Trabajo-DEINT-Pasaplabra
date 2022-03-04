using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraDAL.Gestora;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraUI.Models;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsGamePageVM : clsVMBase
    {
        #region propiedades privadas
        private MediaElement correctSfx;
        private MediaElement wrongSfx;
        private List<clsModelPregunta> listadoPreguntas;
        private clsModelPregunta preguntaSeleccionada;
        private string txtBoxRespuestaJugador;
        private DispatcherTimer tiempo;
        private DelegateCommand checkRespuestaCommand;
        private DelegateCommand saltarPreguntaCommand;
        private bool visibilityPreguntaFallidaControl;
        public bool PartidaFinalizada => PalabrasRestantes == 0 || tiempo.Interval <= TimeSpan.MinValue;

        #endregion

        #region constructor por defecto
        public clsGamePageVM()
        {
            tiempo = new DispatcherTimer();
            mostrarControlPreguntaFallada(false);
            SelectedIndex = 0;
            cargarListadoPreguntas();
            preguntaSeleccionada = listadoPreguntas[0];
            PalabrasRestantes = listadoPreguntas.Count;
            NotifyPropertyChanged("PalabrasRestantes");
            recargarPregunta();
            TiempoMax = 300;
            NotifyPropertyChanged("TiempoMax");
            iniciarContador();
            TxtBoxRespuestaJugador = "";
            wrongSfx = new MediaElement();
            correctSfx = new MediaElement();
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
        public int Aciertos { get; set; }
        public int Fallos { get; set; }
        public int PalabrasRestantes { get; set; }
        public int TiempoMax { get; set; }
        public int SelectedIndex { get; set; }//¿Esta esto bien o es un poco peruano?
        public DelegateCommand CheckRespuestaCommand
        {
            get
            {
                return checkRespuestaCommand = new DelegateCommand(CheckRespuestaCommand_Execute, CheckRespuesta_CanExecute);
            }
        }
        public DelegateCommand SaltarPreguntaCommand
        {
            get
            {
                return saltarPreguntaCommand = new DelegateCommand(SaltarPregunta_Execute);
            }
        }
        public clsModelPregunta PreguntaSeleccionada { get => preguntaSeleccionada; set => preguntaSeleccionada = value; }
        public List<clsModelPregunta> ListadoPreguntas { get => listadoPreguntas; set => listadoPreguntas = value; }

        public String LetraPreguntaFallada { get; set; }
        public String RespuestaPreguntaFallada { get; set; }


        public bool VisibilityPreguntaFallidaControl
        {
            get { return visibilityPreguntaFallidaControl; }
            set
            {
                visibilityPreguntaFallidaControl = value;
                if (visibilityPreguntaFallidaControl)
                {
                    tiempo.Stop();
                }
                else
                {
                    tiempo.Start();
                }
            }

        }
        #endregion

        #region commands
        private void SaltarPregunta_Execute()
        {
            SiguientePregunta();
        }
        private bool CheckRespuesta_CanExecute()
        {
            return preguntaSeleccionada != null && !string.IsNullOrWhiteSpace(TxtBoxRespuestaJugador);
        }

        private void CheckRespuestaCommand_Execute()
        {
            PreguntaSeleccionada.Estado = TxtBoxRespuestaJugador.ToLower().Equals(PreguntaSeleccionada.Respuesta.ToLower()) ? 1 : -1;//TODO preguntarle a fernando lo del bool?=null como estado por defecto
            switch (PreguntaSeleccionada.Estado)
            {
                case 1:
                    Aciertos++;
                    PalabrasRestantes--;
                    _ = PlaySound("correct.mp3", correctSfx);
                    NotifyPropertyChanged("Aciertos");
                    break;
                case -1:
                    mostrarControlPreguntaFallada(true);//TODO HACER METODO DEL BOTON CLICK SINCRONO
                    Fallos++;
                    PalabrasRestantes--;
                    _ = PlaySound("Wrong.mp3", wrongSfx);
                    NotifyPropertyChanged("Fallos");
                    break;
            }
            NotifyPropertyChanged("PreguntaSeleccionada");
            SiguientePregunta();
            //TODO METER AQUI LO QUE HARIA SI HA TERMINADO EL ROSCO
            NotifyPropertyChanged("PalabrasRestantes");
            recargarPregunta();
        }


        #endregion

        #region metodos auxiliares
        private void mostrarControlPreguntaFallada(bool visible)
        {
            if (visible)
            {
                LetraPreguntaFallada = preguntaSeleccionada.Letra.ToString();
                RespuestaPreguntaFallada = preguntaSeleccionada.Respuesta;
                VisibilityPreguntaFallidaControl = true;
                NotifyPropertyChanged("VisibilityPreguntaFallidaControl");
                NotifyPropertyChanged("LetraPreguntaFallada");
                NotifyPropertyChanged("RespuestaPreguntaFallada");
            }
            else
            {
                VisibilityPreguntaFallidaControl = false;
            }
            NotifyPropertyChanged("VisibilityPreguntaFallidaControl");
        }

        private void SiguientePregunta()
        {
            for (int i = SelectedIndex + 1; i < listadoPreguntas.Count + 1; i++)
            {
                if (i == listadoPreguntas.Count)
                { i = 0; }
                if (ListadoPreguntas[i].Estado == 0)
                {
                    SelectedIndex = i;
                    PreguntaSeleccionada = listadoPreguntas[SelectedIndex];
                    recargarPregunta();
                    NotifyPropertyChanged("PreguntaSeleccionada");
                    break;
                }
            }
        }

        private void cargarListadoPreguntas()
        {
            listadoPreguntas = new List<clsModelPregunta>();
            clsListadosPreguntaBL.CargarListadoPreguntaBL().ForEach(pregunta => listadoPreguntas.Add(new clsModelPregunta(0, pregunta.Id, pregunta.Enunciado, pregunta.Respuesta, pregunta.Letra)));
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

        private void recargarPregunta()
        {
            int indice = preguntaSeleccionada.Enunciado.IndexOf(":");
            TxtBoxEnunciadoPregunta = preguntaSeleccionada.Enunciado.Substring(indice + 1, preguntaSeleccionada.Enunciado.Length - indice - 1);//PERUANO, PERO NO SE COMO PONERLO
            NotifyPropertyChanged("TxtBoxEnunciadoPregunta");
            TxtBoxLetraPregunta = preguntaSeleccionada.Enunciado.Substring(0, indice);
            NotifyPropertyChanged("TxtBoxLetraPregunta");
            TxtBoxRespuestaJugador = "";
            NotifyPropertyChanged("TxtBoxRespuestaJugador");
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
        /// <summary>
        /// Metodo auxiliar para reproducir un sonido de la carpeta Sounds dado el nombre del archivo 
        /// </summary>
        /// <param name="soundFileName"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        private async Task PlaySound(string soundFileName, MediaElement media)
        {
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file = await folder.GetFileAsync(soundFileName);
            media.SetSource(await file.OpenAsync(FileAccessMode.Read), "");
            if (correctSfx.CurrentState == Windows.UI.Xaml.Media.MediaElementState.Playing) correctSfx.Stop();
            if (wrongSfx.CurrentState == Windows.UI.Xaml.Media.MediaElementState.Playing) wrongSfx.Stop();
            media.Play();
        }
        #endregion

        #region metodos finPartida
        /// <summary>
        /// Metodo auxiliar que se llama al finalizar la partida para cargar los datos de la misma en la base de datos
        /// TODO Finalizar implementacion
        /// </summary>
        private async Task GameFinishedAsync()
        {
            string nick = await askNickAsync();
            clsPartida partidaJugada = new clsPartida(nick, Aciertos, Fallos, tiempo.Interval);
            if (!string.IsNullOrEmpty(partidaJugada.Nick))
                clsGestoraPartida.insertarPartida(partidaJugada);
        }
        /// <summary>
        /// Metodo auxiliar que muestra en pantala un contentDialog para recibir el nick del usuario
        /// </summary>
        /// <returns></returns>
        private async Task<string> askNickAsync()
        {
            string nickName = "";
            TextBox inputTbx = new TextBox();
            inputTbx.AcceptsReturn = true;
            ContentDialog nickContent = new ContentDialog();
            nickContent.Title = "Introducza un apodo";
            nickContent.PrimaryButtonText = "Enviar puntuacion";
            nickContent.Content = inputTbx;
            if (await nickContent.ShowAsync() == ContentDialogResult.Primary)
                nickName = inputTbx.Text;
            return nickName;
        }
        #endregion

    }
}
