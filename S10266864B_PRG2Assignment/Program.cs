﻿
using S10266864B_PRG2Assignment;

Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();
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
void loadfiles_airlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
            Airline airline = new Airline(data[0], data[1], Flights);
            Airlines.Add(data[0], airline);
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
    loadfiles_airlines();
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
display_flights();
display_boarding_gates();
