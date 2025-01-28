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
        public double CalculateFee()
        {
            return base.CalculateFees() + RequestFee;
        }
        public string ToString()
        {
            return base.ToString() + $"Special Request: LWTT     RequestFee: {RequestFee,-15}";
        }
    }
}