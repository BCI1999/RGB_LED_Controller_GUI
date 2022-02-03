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
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Threading;

namespace RGB_LED_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Defining variables
        byte SliderRed;
        byte SliderGreen;
        byte SliderBlue;

        //Making serial port
        SerialPort _SerialPort;

        public MainWindow()
        {
            InitializeComponent();
            _SerialPort = new SerialPort();

            cbxEffect.Items.Add("Static");
            cbxEffect.Items.Add("Rainbow");
            cbxEffect.Items.Add("Cycling");
        }

        private void RED_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderRed = Convert.ToByte(RED.Value);
            //If selected effect is static, show preview:
            switch (cbxEffect.SelectedIndex)
            {
                case 0:
                    RGB(SliderRed, SliderGreen, SliderBlue, Preview);
                break;
            }
        }

        private void GREEN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderGreen = Convert.ToByte(GREEN.Value);
            //If selected effect is static, show preview:
            switch (cbxEffect.SelectedIndex)
            {
                case 0:
                    RGB(SliderRed, SliderGreen, SliderBlue, Preview);
                break;
            }
        }

        private void BLUE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderBlue = Convert.ToByte(BLUE.Value);
            //If selected effect is static, show preview:
            switch (cbxEffect.SelectedIndex)
            {
                case 0:
                    RGB(SliderRed, SliderGreen, SliderBlue, Preview);
                break;
            }
        }

        //Adding serial port names to the combo box
        private void ProgramLoaded(object sender, RoutedEventArgs e)
        {
            cbxSerial.ItemsSource = SerialPort.GetPortNames();
        }

        //Setting chosen serial port as the actual serial port
        private void cbxSerial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_SerialPort != null)
            {
                if (_SerialPort.IsOpen)
                    _SerialPort.Close();

                _SerialPort.PortName = cbxSerial.SelectedItem.ToString();

                _SerialPort.Open();
            }
        }

        //Method to put RGB slider values onto the preview
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
