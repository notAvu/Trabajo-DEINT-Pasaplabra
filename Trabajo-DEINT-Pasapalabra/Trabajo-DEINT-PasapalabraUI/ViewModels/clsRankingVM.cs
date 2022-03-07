using _18_CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_DEINT_PasapalabraBL.Listados;
using Trabajo_DEINT_PasapalabraEntities;
using Trabajo_DEINT_PasapalabraUI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DEINT_PasapalabraUI.ViewModels
{
    public class clsRankingVM : clsVMBase
    {
        #region propiedades autoimplementadas
        public List<clsPartida> ListaPartidas { get; set; }
        #endregion

        #region constructores
        public clsRankingVM()
        {
            try
            {
                ListaPartidas = clsListadosPartidaBL.CargarListadoPartidaBL();
            }
            catch (Exception)
            {
                mostrarContentDialogCargadoRankingFallidoAsync();
            }
        }
        #endregion

        #region propiedades privadas
        /// <summary>
        /// Muestra un content dialog informando que la carga del ranking ha fallado y al pulsar el botón lleva al usuario al Frame de inicio (MainPage)
        /// </summary>
        /// <returns></returns>
        private async Task mostrarContentDialogCargadoRankingFallidoAsync()
        {
            ContentDialog contentDialogCargadoRankingFallido = new ContentDialog
            {
                Title = "Cargado de ranking fallido",
                Content = "No se ha podido cargar el ranking del juego, disculpa las molestias",
                PrimaryButtonText = "Aceptar"
            };
            var result = await contentDialogCargadoRankingFallido.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                (Window.Current.Content as Frame).Navigate(typeof(MainPage));
            }
        }
        #endregion
    }
}
