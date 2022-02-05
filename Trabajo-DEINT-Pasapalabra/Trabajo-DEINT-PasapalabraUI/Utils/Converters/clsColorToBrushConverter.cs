using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Trabajo_DEINT_PasapalabraUI.Utils.Converters
{
    public class clsColorToBrushConverter : IValueConverter
    { 

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush c = new SolidColorBrush(Colors.LimeGreen);
            switch ((int)value)
            {
                    case 1:
                    c = new SolidColorBrush(Colors.LimeGreen);
                    break;
                    case -1:
                    c = new SolidColorBrush(Colors.Red);
                    break;
                    default: c = new SolidColorBrush(Colors.Red); 
                    break;
            }

            return c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int estado = 0;
            SolidColorBrush c = (SolidColorBrush)value;
            switch (c.Color)
            {
                //case Colors.Red: TODO DUDA
                //    estado = 1;
                //    break;
                //case Colors.Red:
                //    estado = -1;
                //    break;
                default:
                    estado= 0;
                    break;
            }

            return estado;
        }
    }
}
