//==========================================================
// Student Number	: S10269732
// Student Name	: May Cherry Aung
// Partner Name	: Aw Ming Jie
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace S10266864B_PRG2Assignment
{
    class Terminal
    {
        private string terminalName;
        public string TerminalName
        {
            get { return terminalName; }
            set { terminalName = value; }
        }
        private Dictionary<string, Airline> airlines;

        public Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
        
        private Dictionary<string, Flight> flights;

        public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();

        private Dictionary<string, BoardingGate> boardingGates;

        public Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();

        private Dictionary<string, double> gateFees;

        public Dictionary<string, double> GateFees = new Dictionary<string, double>();

        public Terminal() { } 
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }
        public bool AddAirline(Airline airline)
        {
            if (airline != null)
            {
                string name = airline.Name;
                if (Airlines.ContainsKey(name))
                {
                    return false;
                }
                else
                {
                    Airlines.Add(name, airline);
                    return true;
                }
            }
            return true;
        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (boardingGate != null)
            {
                string name = boardingGate.GateName;
                if (BoardingGates.ContainsKey(name))
                {
                    return false;
                }
                else
                {
                    BoardingGates.Add(name, boardingGate);
                    return true;
                }
            }
            return true;
        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            string flightNum = flight.FlightNumber;
            string[] array = flightNum.Split(' ');
            string airlineCode = array[0];
            Airline airline = Airlines[airlineCode];
            return airline;          
        }
        public void PrintAirlineFees()
        {
            Console.WriteLine($"{"Airline Name",-25} {"Fees",-5}");
            foreach (Airline airline in Airlines.Values)
            {
                Console.WriteLine($"{airline.Name,-25} {airline.CalculateFees,-5}");
            }
        }
        public string ToString()
        {
            string s = $"=============================================\r\nWelcome to {TerminalName}\r\n=============================================\r\n";
            return s;
        }


    }
}