//==========================================================
// Student Number	: S10266864
// Student Name	: Aw Ming Jie
// Partner Name	: May Cherry Aung
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266864B_PRG2Assignment
{
    internal class NORMFlight:Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status) 
        { }
        public double CalculateFee()
        {
            return 0.0; //change ltr
        }
        public string ToString()
        {
            return base.ToString() + "DDJBFlight.cs testing"; //change ltr
        }
    }
}
