using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class LetraControl : UserControl
    {
        public LetraControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty LetraProperty =
            DependencyProperty.Register(
                "Letra",
                typeof(Char),
                typeof(LetraControl),
                new PropertyMetadata(null, letraChanged));

        public Char Letra
        {
            get { return (Char)GetValue(LetraProperty); }
            set { SetValue(LetraProperty, value); }
        }

        private static void letraChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucLetra = d as LetraControl;
            ucLetra.Target.Text = e.NewValue.ToString();

        }

        public static readonly DependencyProperty ComenzarAnimacionProperty =
            DependencyProperty.Register(
                "ComenzarAnimacion",
                typeof(bool),
                typeof(LetraControl),
                new PropertyMetadata(false, comenzarAnimacionChanged));
        public bool ComenzarAnimacion
        {
            get { return (bool)GetValue(ComenzarAnimacionProperty); }
            set { SetValue(ComenzarAnimacionProperty, value); }
        }
        private static void comenzarAnimacionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LetraControl ucLetra = d as LetraControl;
            bool comenzarAnimacion = e.NewValue.Equals(true);
            if (comenzarAnimacion)
            {
                ucLetra.storyBoardPrueba.Begin();
            }
            else
            {
                ucLetra.storyBoardPrueba.Stop();
            }
        }

        public static readonly DependencyProperty EstadoProperty =
            DependencyProperty.Register(
                "Estado",
                typeof(int),
                typeof(LetraControl),
                new PropertyMetadata(-2, estadoChanged));

        private static void estadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LetraControl ucLetra = d as LetraControl;
            int estado = (int)e.NewValue;
            if (estado == 1)
            {
                ucLetra.ellipse.Fill = new SolidColorBrush(Colors.Green);
            }
            else if (estado == -1)
            {
                ucLetra.ellipse.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ucLetra.ellipse.Fill = new SolidColorBrush(Colors.Blue);
            }
        }

        public int Estado
        {
            get { return (int)GetValue(ComenzarAnimacionProperty); }
            set { SetValue(EstadoProperty, value); }
        }
    }
}
