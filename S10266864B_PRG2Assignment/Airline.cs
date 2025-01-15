using System;
using System.Collections.Generic;
using System.Linq;
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
		private Dictionary<string,Flight> flights;

		public Dictionary<string,Flight> Flights
		{
			get { return flights; }
			set { flights = value; }
		}
		public Airline(string name, string code, Dictionary<string, Flight> flights)
		{
			Name = name;
			Code = code;
			Flights = flights;
		}
		public bool AddFlight(Flight fly)
		{
			return true; //change later
		}
		public double CalculateFee()
		{
			return 0.0; //change later
		}
		public bool RemoveFlight(Flight fly)
		{
			return true; //change later
		}
		public string ToString()
		{
			return "Airline.cs testing";
		}
	}
}
