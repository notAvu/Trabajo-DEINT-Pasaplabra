using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Trabajo_DEINT_PasapalabraUI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<String> list = new List<String>() { "A", "B", "A", "B", "A", "B", "A", "B", "A", "B", "A", "B", "A", "B", "A", "B" };
            listBoxPrueba.ItemsSource = list;
            (listBoxPrueba.Resources.ToList().First().Value as Storyboard).Begin();
            DataTemplate dt = listBoxPrueba.ItemTemplate;
            StackPanel stck = dt.LoadContent() as StackPanel;
            Storyboard s = stck.Resources.ToList().First().Value as Storyboard;
            ColorAnimation c = s.Children.First() as ColorAnimation; 
            storyboard.Begin();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Storyboard.SetTarget(colorAnimation,btn);
            storyboard2.Begin();
            btn.Background=(SolidColorBrush)Resources["RedColor"];
            storyboard2.Stop();
        }
    }
}
