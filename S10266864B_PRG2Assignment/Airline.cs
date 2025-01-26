//==========================================================
// Student Number	: S10266864
// Student Name	: Aw Ming Jie
// Partner Name	: May Cherry Aung
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace S10266864B_PRG2Assignment
{
    class Airline
    {
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string code;

		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		private Dictionary<string, Flight> flights;
		public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
		
		public Airline(string name, string code)
		{
			Name = name;
			Code = code;
		}
		public bool AddFlight(Flight flight)
		{
			string num = flight.FlightNumber;
			if (flight != null)
			{
				if (Flights.ContainsKey(num))
				{
					return false;
				}
				else
				{
					flights.Add(num, flight);
					return true;
				}
			}
			return false;
		}
		public double CalculateFees()
		{
			double total_fee = 0.0;
			foreach (var kvp in Flights)
			{
				Flight flight = kvp.Value;
                total_fee = flight.CalculateFees() + 300;		
			}
			return total_fee;
		}
		public bool RemoveFlight(Flight flight)
		{
			string temp = flight.FlightNumber;
			foreach (var kvp in Flights)
			{
				if (kvp.Key == temp)
				{
					Flights.Remove(kvp.Key);
					return true;
				}
			}
			return false; 
		}
		public string ToString()
		{
			string s = $"Name: {name,-20} Code: {code,-5}";
			foreach (var flight in Flights)
			{
				s += "\n" + flight.ToString();
			}
			return s;
		}
	}
}
