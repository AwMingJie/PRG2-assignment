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
// Question 6 (mingjie)
void create_new_flight()
{
    string choice = null;
    while (choice != "N")
    {
        Console.Write("Enter Flight Number: ");
        string flightno = Console.ReadLine();
        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.Write("Enter expected Departure/Arrival time: ");
        DateTime time = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Do you want to enter any additional information? (Y/N): ");
        string option = Console.ReadLine();
        string special_req = null;
        List<Flight> f_list = new List<Flight>();
        if (option == "Y")
        {
            Console.Write("What is the special request code: ");
            special_req = Console.ReadLine();
            if (special_req == "DDJB")
            {
                DDJBFlight D_new = new DDJBFlight(flightno, origin, destination, time, "On time", 300);
                Flights.Add(flightno, D_new);
                f_list.Add(D_new);
            }
            else if (special_req == "LWTT")
            {
                LWTTFlight L_new = new LWTTFlight(flightno, origin, destination, time, "On time", 500);
                Flights.Add(flightno, L_new);
                f_list.Add(L_new);
            }
            else if (special_req == "CFFT")
            {
                CFFTFlight C_new = new CFFTFlight(flightno, origin, destination, time, "On time", 150);
                Flights.Add(flightno, C_new);
                f_list.Add(C_new);
            }
        }
        else 
        {
            NORMFlight N_new = new NORMFlight(flightno, origin, destination, time, "On time");
            Flights.Add(flightno, N_new);
            f_list.Add (N_new);
        }
        
        using(StreamWriter sw = new StreamWriter("flights.csv",true))
        {
            foreach(var s in f_list)
            {
                sw.WriteLine(s.ToString());
            }
            sw.Close();
        }
        Console.WriteLine("The flight(s) have been successfully added!");

        Console.Write("Do you want to add another Flight? (Y/N) : ");
        choice = Console.ReadLine().ToUpper();
    }
    



    
}


loadfiles_airlines();
loadfiles_flight();
/*assign_boarding_gate();*/
create_new_flight();