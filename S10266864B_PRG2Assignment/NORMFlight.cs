﻿//==========================================================
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
    internal class NORMFlight:Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status) 
        { }
        public double CalculateFee()
        {
            return base.CalculateFees();
        }
        public string ToString()
        {
            return base.ToString();
        }
    }
}
