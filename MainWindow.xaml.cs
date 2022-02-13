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
using System.Threading;
using System.Timers;

namespace RGB_LED_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Making serial port
        SerialPort _SerialPort;

        //Make new dispatch timer
        DispatcherTimer _dispatcherTimer;

        //Make instance of the effects class
        Effects _effects;

        public MainWindow()
        {
            InitializeComponent();
            _SerialPort = new SerialPort();
            _SerialPort.BaudRate = 57600;

            //Dsipatcher timer setup
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            //Add effects to choose from to the combobox
            cbxEffect.Items.Add("Static");      //Index 0
            cbxEffect.Items.Add("Cycle");       //Index 1

            _effects = new Effects();
        }

        //Adding serial port names to the combo box
        private void ProgramLoaded(object sender, RoutedEventArgs e)
        {
            cbxSerial.ItemsSource = SerialPort.GetPortNames();
        }

        //Dispatcher timer event, ran every 1ms to process the chosen effect and send to the controller
        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Depending on chosen effect, send correct RGB values over UART(serial).
            switch (cbxEffect.SelectedIndex)
            {
                //Static:
                case 0:
                    _effects.RGB_Static();
                    break;
                
                //Cycle:
                case 1:
                    _effects.RGB_Cycle();
                break;
            }

            //If any programmed effect is chosen, lock the sliders
            if (cbxEffect.SelectedIndex != 0)
            {
                Slider_Lock(RED, false);
                Slider_Lock(GREEN, false);
                Slider_Lock(BLUE, false);
            }
            //Else, enable
            else
            {
                Slider_Lock(RED, true);
                Slider_Lock(GREEN, true);
                Slider_Lock(BLUE, true);
            }
            
            //Send RGB values and an "end" byte (255) for synchronisation 
            //The RGB value sent by the program may not exceed 254! (this is barely noticable by eye)
            byte[] RGBdata = { _effects.Red, _effects.Green, _effects.Blue, 0xFF };
            if (_SerialPort.IsOpen)
            {
                _SerialPort.Write(RGBdata, 0, 4);
            }

            //Display a preview of the displayed color
            RGB(_effects.Red, _effects.Green, _effects.Blue, Preview);
        }

        //Sliders
        private void RED_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _effects.Red = Convert.ToByte(RED.Value);
        }

        private void GREEN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _effects.Green = Convert.ToByte(GREEN.Value);
        }

        private void BLUE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _effects.Blue = Convert.ToByte(BLUE.Value);
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

        //Method to put RGB values onto the preview
        private void RGB(byte RED, byte GREEN, byte BLUE, Rectangle rectangle)
        {
            Color RGBColor = new Color();
            RGBColor = Color.FromRgb(RED, GREEN, BLUE);
            SolidColorBrush rgb = new SolidColorBrush();
            rgb.Color = RGBColor;
            rectangle.Fill = rgb;
        }

        //Method to prevent usage of sliders during programmed effect
        private void Slider_Lock(Slider _slider, bool enable)
        {
            if (enable)
            {
                _slider.Maximum = 254;
            }
            else
            {
                _slider.Maximum = 0;
            }
        }
    }
}
