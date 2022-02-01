using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGB_LED_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        byte SliderRed;
        byte SliderGreen;
        byte SliderBlue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RED_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderRed = Convert.ToByte(RED.Value);
            RGB(SliderRed, SliderGreen, SliderBlue, Preview);
        }

        private void GREEN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderGreen = Convert.ToByte(GREEN.Value);
            RGB(SliderRed, SliderGreen, SliderBlue, Preview);
        }

        private void BLUE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderBlue = Convert.ToByte(BLUE.Value);
            RGB(SliderRed, SliderGreen, SliderBlue, Preview);
        }

        private void RGB(byte RED, byte GREEN, byte BLUE, Rectangle rectangle)
        {
            Color RGBKleur = new Color();
            RGBKleur = Color.FromRgb(RED, GREEN, BLUE);
            SolidColorBrush rgb = new SolidColorBrush();
            rgb.Color = RGBKleur;
            rectangle.Fill = rgb;
        }
    }
}
