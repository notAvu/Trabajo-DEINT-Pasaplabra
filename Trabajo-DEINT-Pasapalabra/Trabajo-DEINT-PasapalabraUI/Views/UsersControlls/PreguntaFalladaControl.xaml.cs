using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Trabajo_DEINT_PasapalabraUI.Views.UsersControlls
{
    public sealed partial class PreguntaFalladaControl : UserControl
    {
        private static bool mostrarAlIniciar;
        public PreguntaFalladaControl()
        {
            this.InitializeComponent();
            mostrarAlIniciar = false;
        }

        public static readonly DependencyProperty respuestaProperty =
            DependencyProperty.Register("Respuesta", typeof(string),
                typeof(PreguntaFalladaControl), new PropertyMetadata(string.Empty, respuestaChanged));

        private static void respuestaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (mostrarAlIniciar)
            {
                var ucPreguntaFallada = d as PreguntaFalladaControl;
                ucPreguntaFallada.txtRespuesta.Text = e.OldValue.ToString();
            }

        }

        public static readonly DependencyProperty letraBotonProperty =
            DependencyProperty.Register("LetraBoton", typeof(string),
            typeof(PreguntaFalladaControl), new PropertyMetadata(string.Empty, letraChanged));


        private static void letraChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (mostrarAlIniciar)
            {
                var ucPreguntaFallada = d as PreguntaFalladaControl;
                ucPreguntaFallada.Visibility = Visibility.Visible;
                ucPreguntaFallada.txtLetra.Text = e.OldValue.ToString();
            }
            mostrarAlIniciar = true;
        }

        private static readonly DependencyProperty visibilidadProperty =
            DependencyProperty.Register("Visibilidad", typeof(bool),
            typeof(PreguntaFalladaControl), new PropertyMetadata(true, visibilidadChanged));

        private static void visibilidadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
                var ucPreguntaFallada = d as PreguntaFalladaControl;
                bool visible = e.NewValue.Equals(true);
                if (visible)
                {
                    ucPreguntaFallada.Visibility = Visibility.Visible;
                }
                else
                {
                    ucPreguntaFallada.Visibility = Visibility.Collapsed;
                }
        }

        public string Respuesta
        {
            get { return (string)GetValue(respuestaProperty); }
            set { SetValue(respuestaProperty, value); }
        }

        public string LetraBoton
        {
            get { return (string)(GetValue(letraBotonProperty)); }
            set { SetValue(letraBotonProperty, value); }
        }

        public bool Visibilidad
        {
            get { return (bool)GetValue(visibilidadProperty); }
            set { SetValue(visibilidadProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ucPreguntaFallada.Visibility=Visibility.Collapsed;
        }
    }
}
