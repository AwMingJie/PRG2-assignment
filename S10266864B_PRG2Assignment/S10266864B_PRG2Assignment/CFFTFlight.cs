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
        public double RequestFee { get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            double total_fee = 0.0;
            if (Destination == "Singapore (SIN)")
            {
                total_fee += 500;
            }
            if (Origin == "Singapore (SIN)")
            {
                total_fee += 800;
            }
            total_fee += RequestFee;
            return base.CalculateFees() + total_fee;
        }
        public string ToString()
        {
            return base.ToString() + $"Special Request: CFFT     RequestFee: {RequestFee,-15}";
        }
    }
}
