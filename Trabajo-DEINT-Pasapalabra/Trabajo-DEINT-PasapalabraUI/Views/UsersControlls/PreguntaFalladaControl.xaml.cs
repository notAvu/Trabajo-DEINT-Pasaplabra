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
        public PreguntaFalladaControl()
        {
            this.InitializeComponent();
        }
        public static readonly DependencyProperty respuestaProperty =
            DependencyProperty.Register("Respuesta", typeof(string),
                typeof(PreguntaFalladaControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty letraBotonProperty =
            DependencyProperty.Register("LetraBoton", typeof(string),
            typeof(PreguntaFalladaControl), new PropertyMetadata(string.Empty));

        public string Respuesta
        {
            get { return (string)GetValue(respuestaProperty); }
            set
            {
                    SetValue(respuestaProperty, value);
                    txtRespuesta.Text = value;
            }
        }

        public string LetraBoton
        {
            get { return (string)(GetValue(letraBotonProperty)); }
            set
            {
                if (value != null)
                {
                    SetValue(letraBotonProperty, value);
                    txtLetra.Text = value;
                }
            }
        }
    }
}
