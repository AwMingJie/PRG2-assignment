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
    class CFFTFlight : Flight
    {
        private double requestFee;

        public const double RequestFee = 150;
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
        }
        public double CalculateFee()
        {
            return base.CalculateFees() + RequestFee;
        }
        public string ToString()
        {
            return base.ToString() + $"Special Request: CFFT     RequestFee: {requestFee,-15}";
        }
    }
}
