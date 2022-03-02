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

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value);}
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(SolidColorBrush),
                typeof(LetraControl),
                new PropertyMetadata(null, colorChanged));

        private static void colorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush;
            LetraControl ucLetra = d as LetraControl;
            SolidColorBrush solidColorBrush = (SolidColorBrush)e.NewValue;
            if (solidColorBrush.Color.Equals(Colors.Red))
            {
                mySolidColorBrush = new SolidColorBrush(Colors.Red);
            }else if (solidColorBrush.Color.Equals(Colors.Green))
            {
                mySolidColorBrush= new SolidColorBrush(Colors.Green);
            }
            else
            {
                mySolidColorBrush= new SolidColorBrush(Colors.Blue);
            }

            ucLetra.ellipse.Fill = mySolidColorBrush;
            ucLetra.storyBoard.Begin();

        }
    }
}
