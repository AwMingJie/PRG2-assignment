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
		public double CalculateFees()
		{
            double total_fee = 0.0;
			if (destination == "SIN")
			{
				total_fee += 500;
			}
			else if (origin == "SIN")
			{
				total_fee += 800;
			}	
			return total_fee;
        }
		public int CompareTo(Flight f)
		{
			return ExpectedTime.CompareTo(f.ExpectedTime);
		}
		public string ToString()
		{
			return $"FlightNumber: {flightNumber,-15} Origin: {origin,-20} Destination: {destination,-15} ExpectedTime: {expectedTime,-25} Status: {status,-15}";
		}
	}
}
