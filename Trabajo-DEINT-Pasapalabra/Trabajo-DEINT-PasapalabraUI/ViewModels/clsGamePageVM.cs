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
        private MediaElement correctSfx;
        private MediaElement wrongSfx;
        private List<clsModelPregunta> listadoPreguntas;
        private clsModelPregunta preguntaSeleccionada;
        private string txtBoxRespuestaJugador;
        private DispatcherTimer tiempo;
        private DelegateCommand checkRespuestaCommand;
        private DelegateCommand saltarPreguntaCommand;
        private DelegateCommand volverAInicioCommand;
        private bool visibilityPreguntaFallidaControl;
        private ContentDialog contentDialogPartidaTerminada;
        #endregion

        #region constructor por defecto
        public clsGamePageVM()
        {
            tiempo = new DispatcherTimer();
            MostrarUserControlPreguntaFallada(false);

            CargarListadoPreguntas();
            preguntaSeleccionada = listadoPreguntas[0];
            preguntaSeleccionada.Animado = true;
            RecargarPregunta();

            PalabrasRestantes = listadoPreguntas.Count;
            NotifyPropertyChanged("PalabrasRestantes");

            TiempoMax = 30;
            NotifyPropertyChanged("TiempoMax");
            IniciarContador();

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
        public DispatcherTimer TiempoRestante { get => tiempo; set => tiempo = value; }
        public int Aciertos { get; set; }
        public int Fallos { get; set; }
        public int PalabrasRestantes { get; set; }
        public int TiempoMax { get; set; }
        public int SelectedIndex { get; set; }
        public DelegateCommand CheckRespuestaCommand
        {
            get
            {
                return checkRespuestaCommand = new DelegateCommand(CheckRespuestaCommand_Execute, CheckRespuestaCommand_CanExecute);
            }
        }
        public DelegateCommand SaltarPreguntaCommand
        {
            get
            {
                return saltarPreguntaCommand = new DelegateCommand(SaltarPregunta_Execute);
            }
        }
        public DelegateCommand VolverAInicioCommand//NOTA: Este metodo se podría haber hecho por code behind, pero si se hiciese así el contador
                                                   //no pararía y se mostaría el content dialog de partida terminada por tiempo estuvieses en cualquier frame
        {
            get { return volverAInicioCommand = new DelegateCommand(VolverAInicio_Execute); }
        }

        public clsModelPregunta PreguntaSeleccionada { get => preguntaSeleccionada; set => preguntaSeleccionada = value; }
        public List<clsModelPregunta> ListadoPreguntas { get => listadoPreguntas; set => listadoPreguntas = value; }

        public String LetraPreguntaFallada { get; set; }
        public String RespuestaPreguntaFallada { get; set; }

        public String TxtBoxLetraPregunta { get; set; }

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
            RecargarPregunta();
        }
        private bool CheckRespuestaCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(TxtBoxRespuestaJugador);
        }

        private void CheckRespuestaCommand_Execute()
        {
            PreguntaSeleccionada.Estado = SinTildes(TxtBoxRespuestaJugador.ToLower()).Equals(PreguntaSeleccionada.Respuesta.ToLower()) ? 1 : -1;//TODO preguntarle a fernando lo del bool?=null como estado por defecto
            switch (PreguntaSeleccionada.Estado)
            {
                case 1:
                    Aciertos++;
                    PalabrasRestantes--;
                    _ = PlaySound("correct.mp3", correctSfx);
                    NotifyPropertyChanged("Aciertos");
                    break;
                case -1:
                    MostrarUserControlPreguntaFallada(true);
                    Fallos++;
                    PalabrasRestantes--;
                    _ = PlaySound("Wrong.mp3", wrongSfx);
                    NotifyPropertyChanged("Fallos");
                    break;
            }
            ComprobarPartidaTerminada();
            NotifyPropertyChanged("PalabrasRestantes");
            if (PalabrasRestantes > 0)
            {
                SiguientePregunta();
            }
            RecargarPregunta();
        }

        private void VolverAInicio_Execute()
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
            tiempo.Stop();
        }
        #endregion

        #region metodos logica juego
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
                    break;
                }
            }
            preguntaSeleccionada.Animado = true;
        }

        private void CargarListadoPreguntas()
        {
            listadoPreguntas = new List<clsModelPregunta>();
            try
            {
                clsListadosPreguntaBL.CargarListadoPreguntaBL().ForEach(pregunta => listadoPreguntas.Add(new clsModelPregunta(0, pregunta.Id, pregunta.Enunciado, pregunta.Respuesta, pregunta.Letra)));
            }
            catch (Exception)
            {
                CrearYmostrarContentDialogOperacionFallida("Cargado de preguntas fallido", "No se ha podido cargar las preguntas, disculpen las molestias");
            }
            NotifyPropertyChanged("ListadoPreguntas");//No hace falta en el constructor, pero si al cargar de nuevo cuando el usuario quiere volver a jugar
        }

        private void RecargarPregunta()
        {
            int indice = preguntaSeleccionada.Enunciado.IndexOf(":");
            TxtBoxEnunciadoPregunta = preguntaSeleccionada.Enunciado.Substring(indice + 1, preguntaSeleccionada.Enunciado.Length - indice - 1);//PERUANO, PERO NO SE COMO PONERLO
            NotifyPropertyChanged("TxtBoxEnunciadoPregunta");
            TxtBoxLetraPregunta = preguntaSeleccionada.Enunciado.Substring(0, indice);
            NotifyPropertyChanged("TxtBoxLetraPregunta");
            TxtBoxRespuestaJugador = "";
            NotifyPropertyChanged("TxtBoxRespuestaJugador");
        }
        #endregion

        #region metodos partida terminada
        public void ComprobarPartidaTerminada()
        {
            if (Aciertos == 26)
            {
                contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("¡Victoria! Has ganado el bote");
                MostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
            }
            else if (PalabrasRestantes == 0)
            {
                contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("Partida terminada, ya no quedan preguntas");
                MostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
            }
        }

        private async Task MostrarContentDialogPartidaTerminadaAsync(ContentDialog contentDialogPartidaTerminada)
        {
            tiempo.Stop();
            preguntaSeleccionada.Animado = false;

            var result = await contentDialogPartidaTerminada.ShowAsync();
            StackPanel stck = contentDialogPartidaTerminada.Content as StackPanel;
            UIElementCollection hijosDelStck = stck.Children;
            String nick = (hijosDelStck[1] as TextBox).Text;
            if (String.IsNullOrWhiteSpace(nick))
            {
                nick = "Invitado";
            }
            InsertarPartida(nick);
            if (result == ContentDialogResult.Primary)//Volver a jugar
            {
                ReiniciarPartida();
            }
            else
            {
                VolverAInicio_Execute();
            }
        }

        private void ReiniciarPartida()
        {
            tiempo = new DispatcherTimer();
            MostrarUserControlPreguntaFallada(false);//Para que no muestre el control, en caso de haber fallado la ultima pregunta y volver a jugar
            CargarListadoPreguntas();
            SelectedIndex = 0;
            Fallos = 0;
            NotifyPropertyChanged("Fallos");
            Aciertos = 0;
            NotifyPropertyChanged("Aciertos");
            TiempoMax = 30;
            NotifyPropertyChanged("TiempoMax");

            preguntaSeleccionada = listadoPreguntas[0];
            preguntaSeleccionada.Animado = true;
            RecargarPregunta();

            PalabrasRestantes = listadoPreguntas.Count;
            NotifyPropertyChanged("PalabrasRestantes");

            IniciarContador();
        }
        private void InsertarPartida(String nick)
        {
            try
            {
                clsGestoraPartidaBL.insertarPartidaBL(new clsPartida(nick, Aciertos, Fallos, tiempo.Interval));//TODO QUE COÑO
            }
            catch (Exception ex)
            {
                CrearYmostrarContentDialogOperacionFallida("Guardado de partida fallido", "No se ha podido guardar la partida del jugador, disculpen las molestias");
                tiempo.Stop();
            }
        }
        #endregion

        #region metodos content dialogs

        private async void CrearYmostrarContentDialogOperacionFallida(String titulo, String contenido)
        {
            var contentDialog = new ContentDialog
            {
                Title = titulo,
                Content = contenido,
                PrimaryButtonText = "Aceptar",
            };
            var result = await contentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                VolverAInicio_Execute();
            }
        }

        /// <summary>
        /// Método auxiliar que que retorna un contentDialog que pregunta por el nick del usuario
        /// </summary>
        /// <returns>ContentDialog</returns>
        private ContentDialog crearCuadroDialogoPartidaTerminada(string resultado)
        {
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
                FontFamily = new FontFamily("../Assets/Fonts/#Keedy Sans Regular"),
                CloseButtonText = "Volver al Menu Principal",
                DefaultButton = ContentDialogButton.Primary,
                Background = resultado.Equals("¡Victoria! Has ganado el bote") ? new SolidColorBrush(Windows.UI.Colors.Green) : new SolidColorBrush(Windows.UI.Colors.White)
            };
        }
        #endregion

        #region metodos privados auxiliares generales
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

        /// <summary>
        /// Método que elimina las tildes de la cadena que entra como parámetro y devuelve la misma cadena pero sin las tildes
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Devuelve asociado al nombre una cadena sin tildes</returns>
        private static string SinTildes(string texto) =>
            new String(
                texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
            )
            .Normalize(NormalizationForm.FormC);

        private void IniciarContador()
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
                    tiempo.Stop();
                    contentDialogPartidaTerminada = crearCuadroDialogoPartidaTerminada("El tiempo se ha terminado :(");
                    MostrarContentDialogPartidaTerminadaAsync(contentDialogPartidaTerminada);
                }
            };
        }

        private void MostrarUserControlPreguntaFallada(bool visible)
        {
            if (visible)
            {
                LetraPreguntaFallada = preguntaSeleccionada.Letra.ToString();
                NotifyPropertyChanged("LetraPreguntaFallada");
                RespuestaPreguntaFallada = preguntaSeleccionada.Respuesta;
                NotifyPropertyChanged("RespuestaPreguntaFallada");
            }
            VisibilityPreguntaFallidaControl = visible;
            NotifyPropertyChanged("VisibilityPreguntaFallidaControl");
        }
        #endregion

    }
}
