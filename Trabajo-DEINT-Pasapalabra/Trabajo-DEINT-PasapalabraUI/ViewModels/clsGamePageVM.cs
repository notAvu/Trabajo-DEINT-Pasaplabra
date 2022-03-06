using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Gestora;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraDAL.Gestora;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraUI.Models;
using Trabajo_DEINT_PasapalabraUI.Views;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsGamePageVM : clsVMBase
    {
        #region propiedades privadas
        private MediaElement bgMusic;
        private MediaElement correctSfx;
        private MediaElement wrongSfx;
        private List<clsModelPregunta> listadoPreguntas;
        private clsModelPregunta preguntaSeleccionada;
        private string txtBoxRespuestaJugador;
        private DispatcherTimer tiempo;
        private DelegateCommand checkRespuestaCommand;
        private DelegateCommand saltarPreguntaCommand;
        private bool visibilityPreguntaFallidaControl;
        #endregion

        #region constructor por defecto
        public clsGamePageVM()
        {
            tiempo = new DispatcherTimer();
            mostrarControlPreguntaFallada(false);
            SelectedIndex = 0;
            cargarListadoPreguntas();

            preguntaSeleccionada = listadoPreguntas[0];
            preguntaSeleccionada.Animado = true;

            PalabrasRestantes = listadoPreguntas.Count;
            NotifyPropertyChanged("PalabrasRestantes");
            recargarPregunta();
            TiempoMax = 5;
            NotifyPropertyChanged("TiempoMax");
            iniciarContador();
            TxtBoxRespuestaJugador = "";
            wrongSfx = new MediaElement();
            correctSfx = new MediaElement();
            bgMusic = new MediaElement();
            _ = PlaySound("bgMusic.mp3", bgMusic);
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
            PreguntaSeleccionada.Estado = sinTildes(TxtBoxRespuestaJugador.ToLower()).Equals(PreguntaSeleccionada.Respuesta.ToLower()) ? 1 : -1;//TODO preguntarle a fernando lo del bool?=null como estado por defecto
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
            if (PalabrasRestantes > 0)
            {
                SiguientePregunta();
            }
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
            preguntaSeleccionada.Animado = false;
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
            preguntaSeleccionada.Animado = true;
        }

        private void cargarListadoPreguntas()
        {
            listadoPreguntas = new List<clsModelPregunta>();
            clsListadosPreguntaBL.CargarListadoPreguntaBL().ForEach(pregunta => listadoPreguntas.Add(new clsModelPregunta(0, pregunta.Id, pregunta.Enunciado, pregunta.Respuesta, pregunta.Letra)));
            NotifyPropertyChanged("ListadoPreguntas");
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
            ContentDialog contentDialogPartidaTerminada;
            tiempo.Interval = new TimeSpan(0, 0, 1);
            tiempo.Start();
            tiempo.Tick += (a, b) =>
            {
                TiempoMax--;
                NotifyPropertyChanged("TiempoMax");
                if (TiempoMax == 0)
                {
                    contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("El tiempo se ha terminado :(");
                    mostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
                }
                else if (Aciertos == 26)
                {
                    contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("¡Victoria! Has ganado el bote");
                    mostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
                }
                else if (PalabrasRestantes == 0) {
                    contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("Partida terminada, ya no quedan preguntas");
                    mostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
                }
            };
        }

        private async Task mostrarContentDialogPartidaTerminadaAsync(ContentDialog contentDialogPartidaTerminada)
        {
            tiempo.Stop();
            preguntaSeleccionada.Animado = false;

            var result = await contentDialogPartidaTerminada.ShowAsync();
            if (result == ContentDialogResult.Primary)//Volver a jugar
            {
                tiempo = new DispatcherTimer();
                mostrarControlPreguntaFallada(false);//Para que no muestre el control, en caso de haber fallado la ultima pregunta y volver a jugar
                cargarListadoPreguntas();
                SelectedIndex = 0;
                Fallos = 0;
                NotifyPropertyChanged("Fallos");
                Aciertos = 0;
                NotifyPropertyChanged("Aciertos");
                TiempoMax = 10;
                NotifyPropertyChanged("TiempoMax");

                preguntaSeleccionada = listadoPreguntas[0];
                preguntaSeleccionada.Animado = true;
                recargarPregunta();

                PalabrasRestantes = listadoPreguntas.Count;
                NotifyPropertyChanged("PalabrasRestantes");

                iniciarContador();
                _ = PlaySound("bgMusic.mp3", bgMusic);
            }
            else
            {
                (Window.Current.Content as Frame).Navigate(typeof(MainPage));
            }
            StackPanel stck = contentDialogPartidaTerminada.Content as StackPanel;
            UIElementCollection hijosDelStck = stck.Children;
            String user = (hijosDelStck[1] as TextBox).Text;
            if (String.IsNullOrWhiteSpace(user))
            {
                user = "Invitado";
            }
            clsGestoraPartidaBL.insertarPartidaBL(new clsPartida(user, Aciertos, Fallos, tiempo.Interval));//TODO TRY-CATCH
        }
        /// <summary>
        /// Método auxiliar que que retorna un contentDialog que pregunta por el nick del usuario
        /// </summary>
        /// <returns>ContentDialog</returns>
        private ContentDialog crearCuadroDialogoPartidaTerminada(string resultado)
        {
            bgMusic.Stop();
            StackPanel stckPanelContentDialog = new StackPanel();
            TextBlock txtBlockContentDialog = new TextBlock()
            {
                Text = new StringBuilder("Aciertos: ").Append(Aciertos).Append(Environment.NewLine).Append("Fallos: ").Append(Fallos).Append(Environment.NewLine)
                    .Append("Tiempo Restante: ").Append(TiempoMax).Append(Environment.NewLine).Append("Puntuación: ").Append(Aciertos - Fallos).ToString(),    
                     Padding = new Thickness(10),
                Width = 330,
            };
            TextBox txtBoxContentDialog = new TextBox()
            {
                PlaceholderText = "Introduce tu nick (Longitud máxima 10 carácteres)",
                AcceptsReturn = false,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10),
                Width = 330,
                MaxLength = 10,
            };
            stckPanelContentDialog.Children.Add(txtBlockContentDialog);
            stckPanelContentDialog.Children.Add(txtBoxContentDialog);
            return new ContentDialog
            {
                Title = resultado,
                Content = stckPanelContentDialog,
                PrimaryButtonText = "Volver a Jugar",
                CornerRadius = new CornerRadius(5),
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("../Assets/Fonts/#Keedy Sans Regular"),
                CloseButtonText = "Volver al Menu Principal",
                DefaultButton = ContentDialogButton.Primary,
                Background = resultado.Equals("¡Victoria! Has ganado el bote") ? new SolidColorBrush(Windows.UI.Colors.Green) : new SolidColorBrush(Windows.UI.Colors.White)
            };
        }

        /// <summary>
        /// Metodo auxiliar para reproducir un sonido de la carpeta Sounds dado el nombre del archivo 
        /// </summary>
        /// <param name="soundFileName"></param>
        private async Task PlaySound(string soundFileName, MediaElement media)
        {
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file = await folder.GetFileAsync(soundFileName);
            media.SetSource(await file.OpenAsync(FileAccessMode.Read), "");
            if (correctSfx.CurrentState == MediaElementState.Playing) correctSfx.Stop();
            if (wrongSfx.CurrentState == MediaElementState.Playing) wrongSfx.Stop();
            media.Play();
        }
        #endregion

        #region metodos finPartida
        #endregion

    }
}
