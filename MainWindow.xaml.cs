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

        byte RedValue;
        byte GreenValue;
        byte BlueValue;

        //Making serial port
        SerialPort _SerialPort;

        DispatcherTimer _dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            _SerialPort = new SerialPort();
            _SerialPort.BaudRate = 57600;

            //Dsipatch timer
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            cbxEffect.Items.Add("Static");
            cbxEffect.Items.Add("Rainbow");
            cbxEffect.Items.Add("Cycling");
        }

        //Adding serial port names to the combo box
        private void ProgramLoaded(object sender, RoutedEventArgs e)
        {
            cbxSerial.ItemsSource = SerialPort.GetPortNames();
        }

        //Dispatcher timer event, ran every 1ms
        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Depending on chosen effect, send correct RGB values over UART(serial).
            switch (cbxEffect.SelectedIndex)
            {
                case 0:
                    RedValue = SliderRed;
                    GreenValue = SliderGreen;
                    BlueValue = SliderBlue;
                break;
            }

            //Send the actual data, with their respective ASCII reference bytes (R, G and B)
            //And send an "end" byte (255) for synchronisation 
            //The RGB value sent by the program may not exceed 254!
            byte[] RGBdata = { RedValue, GreenValue, BlueValue, 0xFF };
            if (_SerialPort.IsOpen)
            {
                _SerialPort.Write(RGBdata, 0, 4);
            }
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
