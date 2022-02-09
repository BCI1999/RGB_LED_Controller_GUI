using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RGB_LED_Controller
{
    internal class Cycle_Effect
    {
        private byte red;
        private byte green;
        private byte blue;

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

        //Make a sawtooth like cycling pattern for R, G and B values
        public void RGB_Cycle(byte r, byte g, byte b)
        {
            if ((r > 0) && (b == 0))
            {
                r--;
                g++;
            }

            if ((g > 0) && (r == 0))
            {
                g--;
                b++;
            }

            if ((b > 0) && (g == 0))
            {
                b--;
                r++;
            }

            //If no value is yet assigned, start with red at value 254
            //The r, g and b values won't exceed 254.
            if ((r == 0) && (g == 0) && (b == 0))
            {
                r = 254;
            }

            red = r;
            green = g;
            blue = b;

        }

    }
}
