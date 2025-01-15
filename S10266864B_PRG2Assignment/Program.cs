﻿
using S10266864B_PRG2Assignment;
using System.Diagnostics.CodeAnalysis;

Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();


//question 1 (May)
void loadfiles_airlines_and_boarding_gates()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
            Airline airline = new Airline(data[0], data[1]);
            Airlines.Add(data[1], airline);
        }
    }
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
            Flight? flight = null;
            BoardingGate boardingGate = new BoardingGate(data[0], Convert.ToBoolean(data[1]), Convert.ToBoolean(data[2]), Convert.ToBoolean(data[3]), flight);
            BoardingGates.Add(data[0], boardingGate);
        }


    }
}

//question 2 (Ming Jie)

void loadfiles_flight()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(',');
        if (data[4] == "DDJB")
        {
            DDJBFlight d1 = new DDJBFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time", 300);
            Flights.Add(data[0], d1);
        }
        else if (data[4] == "CFFT")
        {
            CFFTFlight c1 = new CFFTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time", 150);
            Flights.Add(data[0], c1);
        }
        else if (data[4] == "LWTT")
        {
            LWTTFlight l1 = new LWTTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time", 500);
            Flights.Add(data[0], l1);
        }
        else
        {
            NORMFlight n1 = new NORMFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time");
            Flights.Add(data[0], n1);
        }

    }

}
}

//question 3 (Ming Jie)
void display_flights()
{
    loadfiles_flight();
    foreach (var f in Flights)
    {
        Console.WriteLine(f.Value.ToString());
        
    }
}

//question 4 (May)
void display_boarding_gates()
{
    loadfiles_airlines_and_boarding_gates();
    Console.WriteLine("{0, -18} {1,-25 } {2, -13}", "Boarding gates", "Special Request Codes", "Flight Number");
    foreach (var kvp in BoardingGates)
    {
        List<string> special_request_list = new List<string>();
        string special_request = "";
        if (kvp.Value.SupportsDDJB)
            special_request_list.Add("DDJB");
        if (kvp.Value.SupportsCFFT)
            special_request_list.Add("CFFT");
        if (kvp.Value.SupportsLWTT)
            special_request_list.Add("LWTT");
        string flight = "";

        if (special_request_list.Count == 0)
        {
            special_request = "";
        }
        else if (special_request_list.Count == 1)
        {
            special_request = special_request_list[0];
        }
        else
        {
            special_request = special_request_list[0];
            for (int i = 1; i < special_request_list.Count; i++)
            {
                special_request += ", " + special_request_list[i];
            }
        }
        if (kvp.Value.Flight != null)
        {
            flight = kvp.Value.Flight.FlightNumber;
        }
        Console.WriteLine($"{kvp.Value.GateName,-18} {special_request,-25} {flight,-13}");
    }
}

//question 5 (mingjie)
void assign_boarding_gate()
{
    Console.Write("Enter Flight Number: ");
    string flightno = Console.ReadLine();
    bool flag = false;
    Flight flight = null;
    foreach (var f in Flights)
    {
        if (f.Key == flightno)
        {
            Console.WriteLine(f.Value.ToString());
            flag = true;
            flight = f.Value;
            break;
        }   
    }
    if (flag == true)
    {
        bool condition = true;
        while (condition == true)
        {
            Console.Write("Enter Boarding Gate: ");
            string boardingGate = Console.ReadLine();
           /* BoardingGate B34 = new BoardingGate("B34", true, true, true, flight);
            BoardingGates.Add("B34", B34);*/
            foreach (var b in BoardingGates)
            {
                if (b.Key == boardingGate)
                {
                    if (b.Value.Flight == null)
                    {

                        b.Value.Flight = flight;
                        Console.WriteLine(b.Value.ToString());
                        condition = false;
                    }
                    else
                    {
                        Console.WriteLine("The Boarding Gate is already assigned.");
                    }
                    break;
                }
                
            }

        }
    }

}
loadfiles_airlines_and_boarding_gates();
loadfiles_flight();
assign_boarding_gate();

//question 7 (May)

void display_flight_from_airline()
{
    Console.WriteLine($"{"Airline Name",-20} {"Airline Code",-5}" + "\n");
    foreach (var a in Airlines)
    {
        Console.WriteLine($"{a.Value.Name,-20} {a.Value.Code}");
    }
    Console.Write("Enter the 2-letter Airline Code: ");
    string? code = Console.ReadLine();
    Airline user_airline = null;
    foreach (var a in Airlines)
    {
        if (a.Key == code)
        {
            user_airline = a.Value;
        }
    }

    foreach(var f in Flights)
    {
        Flight flight = f.Value;
        string[] flight_num = flight.FlightNumber.Split(" ");
        string airline_code = flight_num[0];
        foreach (var a in Airlines)
        {
            if (a.Key == airline_code)
            {
                a.Value.Flights.Add(flight.FlightNumber, flight);
                break;
            }
        }
    }
    if (user_airline != null)
    {
        Console.WriteLine($"{"Airline Number",-20}{"Origin",-20}{"Destination",-20}");
        foreach (var f in user_airline.Flights)
        {
            Console.WriteLine($"{f.Value.FlightNumber,-20}{f.Value.Origin,-20}{f.Value.Destination,-20}");
        }

        Console.WriteLine("Enter the flight number to select: ");
        string user_flight = Console.ReadLine();
        Console.WriteLine($"{"Flight Number", -15}{"Airline name", -20}{"Origin", -20}{"Destination", -20}{"Expected Departure/Arrival Time", -35}{"Special Request Code", -25}{"Boarding Gate", -20}");
        foreach (var f1 in user_airline.Flights)
        {
            if (f1.Key == user_flight)
            {
                Flight f = f1.Value;
                string boarding_gate = "";
                foreach (var b in BoardingGates)
                {
                    if (b.Value.Flight == f)
                    {
                        boarding_gate = b.Value.GateName;
                    }
                }
                if (f.GetType() == typeof(NORMFlight))
                {
                    Console.WriteLine($"{f.FlightNumber,-15}{user_airline.Name,-20}{f.Origin,-20}{f.Destination,-20}{f.ExpectedTime,-35}{"",-25}{boarding_gate,-20}");
                }
                else if (f.GetType() == typeof(LWTTFlight))
                {
                    Console.WriteLine($"{f.FlightNumber,-15}{user_airline.Name,-20}{f.Origin,-20}{f.Destination,-20}{f.ExpectedTime,-35}{"LWTT",-25}{boarding_gate,-20}");
                }
                else if (f.GetType() == typeof(CFFTFlight))
                {
                    Console.WriteLine($"{f.FlightNumber,-15}{user_airline.Name,-20}{f.Origin,-20}{f.Destination,-20}{f.ExpectedTime,-35}{"CFFT",-25}{boarding_gate,-20}");
                }
                else if (f.GetType() == typeof(DDJBFlight))
                {
                    Console.WriteLine($"{f.FlightNumber,-15}{user_airline.Name,-20}{f.Origin,-20}{f.Destination,-20}{f.ExpectedTime,-35}{"DDJB",-25}{boarding_gate,-20}");
                }
            }
        }
    }

}

display_flight_from_airline();