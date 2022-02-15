using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RGB_LED_Controller
{
    internal class Effects
    {
        private byte red;
        private byte green;
        private byte blue;
        private int effect;
        bool UpDown;

        public byte Red
        {
            get { return red; }
            set { red = value; }
        }

        public byte Green
        {
            get { return green; }
            set { green = value; }
        }

        public byte Blue
        {
            get { return blue; }
            set { blue = value; }
        }

        public void RGB_Static()
        {

        }

        //Make a sawtooth like cycling pattern for R, G and B values
        public void RGB_Cycle(/*byte r, byte g, byte b*/)
        {
            if ((red > 0) && (blue == 0))
            {
                red--;
                green++;
            }

            if ((green > 0) && (red == 0))
            {
                green--;
                blue++;
            }

            if ((blue > 0) && (green == 0))
            {
                blue--;
                red++;
            }

            //If no value is yet assigned, start with red at value 254
            //The r, g and b values won't exceed 254.
            if ((red == 0) && (green == 0) && (blue == 0))
            {
                red = 254;
            }

            //If none are 0, start anew
            if ((red != 0) && (green != 0) && (blue != 0))
            {
                red = 254;
                green = 0;
                blue = 0;
            }
        }

        //Method for the breathing effect
        public void breath(Slider sliderRed, Slider sliderGreen, Slider sliderBlue)
        {
            //Make temporary doubles
            double tempred;
            double tempgreen;
            double tempblue;

            //Make start values, depending the value of the selected color
            double startred = sliderRed.Value / 100;
            double startgreen = sliderGreen.Value / 100;
            double startblue = sliderBlue.Value / 100;

            //If the value should go up, go up
            if (UpDown)
            {
                //If statement to the make change faster near 0                
                if ((red <= 16) || (green <= 16) || (blue <= 16))
                {
                    tempred = (red * 1.01) + startred;
                    tempgreen = (green * 1.01) + startgreen;
                    tempblue = (blue * 1.01) + startblue;
                }
                else
                {
                    tempred = red * 1.01;
                    tempgreen = green * 1.01;
                    tempblue = blue * 1.01;
                }

                //for (double n = 0.0; n >= Math.PI; n = n + (Math.PI / 180.0))
                //{
                //    tempred = red * Math.Sin(n);
                //    tempgreen = green * Math.Sin(n);
                //    tempblue = blue * Math.Sin(n);
                //}
            }
            else
            {
                //If not going up, go down
                tempred = red * 0.99;
                tempgreen = green * 0.99;
                tempblue = blue * 0.99;

                //for (double n = 0.0; n >= (Math.PI / 2.0); n = n + (Math.PI / 180.0))
                //{
                //    tempred = red * Math.Sin(n);
                //    tempgreen = green * Math.Sin(n);
                //    tempblue = blue * Math.Sin(n);
                //}
            }

            //Temp values may not exceed 254 to prevent crashes (bytes are limited to 255)
            if (tempred >= 254)
            {
                tempred = 254;
            }
            if (tempgreen >= 254)
            {
                tempgreen = 254;
            }
            if (tempblue >= 254)
            {
                tempblue = 254;
            }

            //Make a dispatcher timer for the breathing effect
            var BreathingTimer = new DispatcherTimer();
            BreathingTimer.Interval = TimeSpan.FromMilliseconds(40);
            BreathingTimer.Start();
            //Execute the following code when a tick is made:
            BreathingTimer.Tick += (sender, args) =>
            {
                //Stop the timer to prevent endless loop
                BreathingTimer.Stop();
                //Floor and set the temporary variables to the variable red, green and blue
                red = Convert.ToByte(Math.Floor(tempred));
                green = Convert.ToByte(Math.Floor(tempgreen));
                blue = Convert.ToByte(Math.Floor(tempblue));

                //If one color value reached 0, and it's slider value is not 0, set everything to 0
                if ((red == 0 && sliderRed.Value != 0) || (green == 0 && sliderGreen.Value != 0) || (blue == 0 && sliderBlue.Value != 0))
                {
                    red = 0;
                    green = 0;
                    blue = 0;
                    UpDown = true;
                }

                //If all sliders reached their maximum values, start going down
                if ((red >= Convert.ToByte(sliderRed.Value)) && (green >= Convert.ToByte(sliderGreen.Value)) && (blue >= Convert.ToByte(sliderBlue.Value)))
                {
                    UpDown = false;
                }

                //If red is the max value of it's slider, keep it at that value, same goes for green and blue
                if (red >= Convert.ToByte(sliderRed.Value))
                {
                    red = Convert.ToByte(sliderRed.Value);
                }

                if (green >= Convert.ToByte(sliderGreen.Value))
                {
                    red = Convert.ToByte(sliderRed.Value);
                }

                if (blue >= Convert.ToByte(sliderBlue.Value))
                {
                    blue = Convert.ToByte(sliderBlue.Value);
                }
            };
        }
    }
}
