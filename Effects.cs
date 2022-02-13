using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RGB_LED_Controller
{
    internal class Effects
    {
        private byte red;
        private byte green;
        private byte blue;
        private int effect;

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

            //red = r;
            //green = g;
            //blue = b;

        }

    }
}
