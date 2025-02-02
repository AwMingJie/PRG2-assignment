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
    abstract class Flight:IComparable<Flight>
    {
		private string flightNumber;

		public string FlightNumber
		{
			get { return flightNumber; }
			set { flightNumber = value; }
		}
		private string origin;

		public string Origin
		{
			get { return origin; }
			set { origin = value; }
		}
		private string destination;

		public string Destination
		{
			get { return destination; }
			set { destination = value; }
		}
		private DateTime expectedTime;

		public DateTime ExpectedTime
		{
			get { return expectedTime; }
			set { expectedTime = value; }
		}
		private string status;

		public string Status
		{
			get { return status; }
			set { status = value; }
		}
		public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
		{
			FlightNumber = flightNumber;
			Origin = origin;
			Destination = destination;
			ExpectedTime = expectedTime;
			Status = status;
		}
		public virtual double CalculateFees()
		{
			return 300;
        }
		public int CompareTo(Flight f)
		{
			return ExpectedTime.CompareTo(f.ExpectedTime);
		}

		public string DisplayFlightToString(string airline_name)
		{
            return $"{flightNumber,-15} {airline_name, -20} {origin,-20} {destination,-25} {expectedTime,-25}";
        }

		public string ToString()
		{
			
			return $"{flightNumber,-15} {origin,-20} {destination,-15} {expectedTime,-25}";
		}
        //TODO 
        public void UpdateStatus(int num)
        {
            if (num == 1)
            {
                status = "Delayed";
            }
            else if (num == 2)
            {
                status = "Boarding";
            }
            else if (num == 3)
            {
                status = "On Time";
            }
            else
            {
                Console.WriteLine("Status invalid");
            }
        }
    }
}
