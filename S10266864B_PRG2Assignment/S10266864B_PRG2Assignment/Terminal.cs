//==========================================================
// Student Number	: S10269732
// Student Name	: May Cherry Aung
// Partner Name	: Aw Ming Jie
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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

        public Dictionary<string, Airline> Airlines
        {
            get { return airlines; }
            set { airlines = value; }
        }

        private Dictionary<string, Flight> flights;

        public Dictionary<string, Flight> Flights
        {
            get { return flights; }
            set { flights = value; }
        }

        private Dictionary<string, BoardingGate> boardingGates;

        public Dictionary<string, BoardingGate> BoardingGates
        {
            get { return boardingGates; }
            set { boardingGates = value; }
        }
        //End of Comment

        private Dictionary<string, double> gateFees;

        public Dictionary<string, double> GateFees = new Dictionary<string, double>();

        public Terminal() { }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            Airlines = new Dictionary<string, Airline>();
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
                if (boardingGates.ContainsKey(name))
                {
                    return false;
                }
                else
                {
                    boardingGates.Add(name, boardingGate);
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
            foreach (var air in airlines)
            {
                if (air.Value.Code == airlineCode)
                {
                    return air.Value;
                }
            }
            //Airline airline = airlines[airlineCode];
            return null;
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

        //TODO Check Boarding Gate Assigned
        public bool Check_Boarding_Gate_Assigned(string boarding_gate_input, Flight flight)
        {
            foreach (var bg in BoardingGates)
            {
                if (bg.Key == boarding_gate_input)
                {
                    if (bg.Value.Flight == null)
                    {
                        bg.Value.Flight = flight;
                        Console.WriteLine("Flight Number: " + flight.FlightNumber);
                        Console.WriteLine("Origin: " + flight.Origin);
                        Console.WriteLine("Destination: " + flight.Destination);
                        Console.WriteLine("Expected Time: " + flight.ExpectedTime);
                        //TODO special request code??
                        Console.WriteLine("Boarding Gate Name: " + bg.Value.GateName);
                        Console.WriteLine("Supports DDJB: " + bg.Value.SupportsDDJB);
                        Console.WriteLine("Supports CFFT: " + bg.Value.SupportsCFFT);
                        Console.WriteLine("Supports LWTT: " + bg.Value.SupportsLWTT);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("The Boarding Gate is already assigned.");
                        return true;
                    }
                }
            }
            Console.WriteLine("Boarding Gate not found, Please type a valid boarding gate");
            return true;
        }


        //TODO Find Flight based on flight number
        public Flight Check_Flight_Given_Flight_num(string flight_num)
        {
            foreach (var f in flights)
            {
                if (f.Key == flight_num)
                {
                    return f.Value;
                }
            }
            return null;
        }

        public bool Remove_Flight_From_Terminal(string flightnum)
        {
            foreach (var f in flights)
            {
                if (f.Key == flightnum)
                {
                    flights.Remove(flightnum);
                    return true;
                }
            }

            return false;
        }

        public void AdvancedA()
        {
            Queue<Flight> flights_queue = new Queue<Flight>();
            Dictionary<string, Flight> assigned_flights = new Dictionary<string, Flight>();
            Dictionary<string, BoardingGate> unassigned_boarding_gate = new Dictionary<string, BoardingGate>();
            foreach (var bg in boardingGates)
            {
                if (bg.Value.Flight == null)
                {
                    unassigned_boarding_gate.Add(bg.Key, bg.Value);
                }
                else
                {
                    assigned_flights.Add(bg.Value.Flight.FlightNumber, bg.Value.Flight);
                }
            }

            foreach (var flight in flights)
            {
                if (unassigned_boarding_gate.ContainsKey(flight.Key))
                {
                    Console.WriteLine(flight.Key + "is in Dictionary");
                }
                else
                {
                    flights_queue.Enqueue(flight.Value);
                }
            }

            Console.WriteLine("Total number of flights that do not have any Boarding Gate assigned: " + flights_queue.Count);
            Console.WriteLine("Total number of Boarding Gates that do not have a Flight Number assigned yet: " + unassigned_boarding_gate.Count);

            int i = 0;
            foreach (var flight in flights_queue)
            {

                if (flight.GetType() == typeof(NORMFlight))
                {
                    foreach (var bg in unassigned_boarding_gate)
                    {
                        if (bg.Value.IsNormal() == true)
                        {
                            bg.Value.Flight = flight;
                            boardingGates[bg.Key].Flight = flight;
                            Console.WriteLine(bg.Value.ToString());
                            unassigned_boarding_gate.Remove(bg.Key);
                            i++;
                            break;
                        }
                    }
                }
                else if (flight.GetType() == typeof(CFFTFlight))
                {
                    foreach (var bg in unassigned_boarding_gate)
                    {
                        if (bg.Value.SupportsCFFT == true)
                        {
                            bg.Value.Flight = flight;
                            boardingGates[bg.Key].Flight = flight;
                            Console.WriteLine(bg.Value.ToString());
                            unassigned_boarding_gate.Remove(bg.Key);
                            i++;
                            break;
                        }
                    }
                }
                else if (flight.GetType() == typeof(DDJBFlight))
                {
                    foreach (var bg in unassigned_boarding_gate)
                    {
                        if (bg.Value.SupportsDDJB == true)
                        {
                            bg.Value.Flight = flight;
                            boardingGates[bg.Key].Flight = flight;
                            Console.WriteLine(bg.Value.ToString());
                            unassigned_boarding_gate.Remove(bg.Key);
                            i++;
                            break;
                        }
                    }
                }
                else if (flight.GetType() == typeof(LWTTFlight))
                {
                    foreach (var bg in unassigned_boarding_gate)
                    {
                        if (bg.Value.SupportsLWTT == true)
                        {
                            bg.Value.Flight = flight;
                            boardingGates[bg.Key].Flight = flight;
                            Console.WriteLine(bg.Value.ToString());
                            unassigned_boarding_gate.Remove(bg.Key);
                            i++;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No such type found");
                }
            }

            Console.WriteLine("Flights assigned: " + i.ToString());
        }
    }
}