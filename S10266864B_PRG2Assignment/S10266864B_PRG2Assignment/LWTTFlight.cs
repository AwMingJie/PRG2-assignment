//==========================================================
// Student Number	: S10269732
// Student Name	: May Cherry Aung
// Partner Name	: Aw Ming Jie
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266864B_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
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
            return base.ToString() + $"Special Request: LWTT     RequestFee: {RequestFee,-15}";
        }
    }
}