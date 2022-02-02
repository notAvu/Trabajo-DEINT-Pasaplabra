using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Trabajo_DEINT_PasapalabraUI.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
            storyboard4.Begin();
        }
        
        public void comenzarAnimaciones()
        {
            for (int i = 0; i < 25; i++)
            {
                crearAnimacion();
            }         
        }


        private void crearAnimacion(){
            Random r = new Random();
            Storyboard str = new Storyboard();
            DoubleAnimation dbAnimationEjeX = new DoubleAnimation();            
            DoubleAnimation dbAnimationEjeY = new DoubleAnimation();            
            animarYConfigurarElementos(r, crearElementosDeLaInterfaz(r), dbAnimationEjeX,dbAnimationEjeY,str);
        }

        private void animarYConfigurarElementos(Random r,Border borde2,DoubleAnimation dbAnimationEjeX, DoubleAnimation dbAnimationEjeY,Storyboard str)
        {
            dbAnimationEjeX.AutoReverse = true;
            dbAnimationEjeX.RepeatBehavior = RepeatBehavior.Forever;
            dbAnimationEjeY.AutoReverse = true;
            dbAnimationEjeY.To = r.Next(-1000, 1000);
            dbAnimationEjeY.RepeatBehavior = RepeatBehavior.Forever;
            dbAnimationEjeX.To = r.Next(-1000, 1000);
            TranslateTransform moveTransform = new TranslateTransform();
            moveTransform.X = r.Next(-1000, 1000);
            moveTransform.Y = r.Next(-1000, 1000);
            borde2.RenderTransform = moveTransform;
            dbAnimationEjeX.Duration = new Duration(TimeSpan.FromSeconds(10));
            dbAnimationEjeY.Duration = new Duration(TimeSpan.FromSeconds(10));
            str.Children.Add(dbAnimationEjeX);
            str.Children.Add(dbAnimationEjeY);
            Storyboard.SetTarget(dbAnimationEjeY, moveTransform);
            Storyboard.SetTarget(dbAnimationEjeX, moveTransform);
            Storyboard.SetTargetProperty(dbAnimationEjeY, "Y");
            Storyboard.SetTargetProperty(dbAnimationEjeX, "X");
            RelativePanel.SetAlignHorizontalCenterWithPanel(borde2, true);
            RelativePanel.SetBelow(borde2, stckMenu);
            rltRoot.Children.Add(borde2);
            str.Begin();

        }

        private Border crearElementosDeLaInterfaz(Random r){            
            string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Windows.UI.Color[] colores = new Windows.UI.Color[] { Colors.Red, Colors.Blue, Colors.Green };
            var borde = new Border();
            var textBlock = new TextBlock();
            textBlock.Text = letras[r.Next(0, 25)].ToString();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize = 25;
            textBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            textBlock.FontWeight = FontWeights.Bold;
            borde.Child = textBlock;
            borde.CornerRadius = new CornerRadius(25);
            borde.Background = new SolidColorBrush(colores[r.Next(0, 3)]);            
            Border borde2 = new Border();
            borde2.Width = 56;
            borde2.Height = 56;
            Canvas.SetZIndex(borde2, -99);
            borde.Width = 50;
            borde.Height = 50;
            borde.HorizontalAlignment = HorizontalAlignment.Center;
            borde.VerticalAlignment = VerticalAlignment.Center;
            borde2.Background = new SolidColorBrush(Windows.UI.Colors.White);
            borde2.Child = borde;
            borde2.BackgroundSizing = BackgroundSizing.OuterBorderEdge;
            borde2.CornerRadius = new CornerRadius(35);
            return borde2;
        }
    }
}
