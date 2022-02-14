using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using System.Windows.Controls;

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

        public int Effect
        {
            get { return effect; }
            set { effect = value; }
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

        public void breath(Slider sliderRed, Slider sliderGreen, Slider sliderBlue)
        {
            double tempred;
            double tempgreen;
            double tempblue;

            double lastred = sliderRed.Value / 95;
            double lastgreen = sliderGreen.Value / 95;
            double lastblue = sliderBlue.Value / 95;
            if (UpDown)
            {
                tempred = (red / 0.95) + lastred;
                tempgreen = (green / 0.95) + lastgreen;
                tempblue = (blue / 0.95) + lastblue;
            }
            else
            {
                //If to make the value lower slow at first, then go faster
                if ((red > 200) || (green > 200) || (blue > 200))
                {
                    tempred = red * 0.99;
                    tempgreen = green * 0.99;
                    tempblue = blue * 0.99;
                }
                else
                {
                    tempred = red * 0.95;
                    tempgreen = green * 0.95;
                    tempblue = blue * 0.95;
                }
            }


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

            red = Convert.ToByte(Math.Floor(tempred));
            green = Convert.ToByte(Math.Floor(tempgreen));
            blue = Convert.ToByte(Math.Floor(tempblue));
            Thread.Sleep(100);

            if ((red <= 0) && (green <= 0) && (blue <= 0))
            {
                red = 0;
                green = 0;
                blue = 0;
                UpDown = true;
            }

            if ((red >= Convert.ToByte(sliderRed.Value)) && (green >= Convert.ToByte(sliderGreen.Value)) && (blue >= Convert.ToByte(sliderBlue.Value)))
            {
                red = Convert.ToByte(sliderRed.Value);
                green = Convert.ToByte(sliderGreen.Value);
                blue = Convert.ToByte(sliderBlue.Value);
                UpDown = false;
            }
        }
    }
}
