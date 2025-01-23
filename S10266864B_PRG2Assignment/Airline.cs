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

		public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
		
		public Airline(string name, string code)
		{
			Name = name;
			Code = code;
		}
		public bool AddFlight(Flight fly)
		{
			return true; //change later
		}
		public double CalculateFees()
		{
			return 0.0; //change later
		}
		public bool RemoveFlight(Flight fly)
		{
			return true; //change later
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
