//==========================================================
// Student Number	: S10266864
// Student Name	: Aw Ming Jie
// Partner Name	: May Cherry Aung
//==========================================================


using S10266864B_PRG2Assignment;
using System.Diagnostics.CodeAnalysis;

//Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
//Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
//Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();

Terminal terminal = new Terminal("Changi Airport Terminal 5");
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
            terminal.AddAirline(airline);
        }
    }
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
            Flight? flight = null;
            BoardingGate boardingGate = new BoardingGate(data[0], Convert.ToBoolean(data[1]), Convert.ToBoolean(data[2]), Convert.ToBoolean(data[3]), flight);
            terminal.AddBoardingGate(boardingGate);
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
            DDJBFlight d1 = new DDJBFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time");
            terminal.Flights.Add(data[0], d1);
        }
        else if (data[4] == "CFFT")
        {
            CFFTFlight c1 = new CFFTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time");
            terminal.Flights.Add(data[0], c1);
        }
        else if (data[4] == "LWTT")
        {
            LWTTFlight l1 = new LWTTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time");
            terminal.Flights.Add(data[0], l1);
        }
        else
        {
            NORMFlight n1 = new NORMFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time");
            terminal.Flights.Add(data[0], n1);
        }

    }

}
}

//question 3 (Ming Jie)
void display_flights()
{
    foreach (var f in terminal.Flights)
    {
        Console.WriteLine(f.Value.ToString());
        
    }
}

//question 4 (May)
void display_boarding_gates()
{
    
    Console.WriteLine("{0, -18} {1,-25 } {2, -13}", "Boarding gates", "Special Request Codes", "Flight Number");
    foreach (var kvp in terminal.BoardingGates)
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
    foreach (var f in terminal.Flights)
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
            foreach (var b in terminal.BoardingGates)
            {
                if (b.Key == boardingGate)
                {
                    if (b.Value.Flight == null)
                    {

                        b.Value.Flight = flight;
                        Console.WriteLine(flight.ToString() + $"Boarding Gate: {boardingGate}");
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
        while (condition == false)
        {
            
            Console.Write("Would you like to update the status of the Flight? (Y/N): ");
            string choice = Console.ReadLine();
            if (choice == "Y")
            {

                Console.Write("Enter the new status of the flight(Delayed/Boarding/On Time): ");
                string status = Console.ReadLine();
                flight.Status = status;
                Console.WriteLine("Boarding Gate successfully assigned!");
                
                condition = true;
            }
            else if (choice == "N")
            {
                condition = true;
            }
            else
            {
                Console.WriteLine("Please type only (Y) or (N)");
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
        if (option == "Y")
        {
            Console.Write("What is the special request code: ");
            special_req = Console.ReadLine();
            if (special_req == "DDJB")
            {
                DDJBFlight D_new = new DDJBFlight(flightno, origin, destination, time, "On time");
                terminal.Flights.Add(flightno, D_new);
                
            }
            else if (special_req == "LWTT")
            {
                LWTTFlight L_new = new LWTTFlight(flightno, origin, destination, time, "On time");
                terminal.Flights.Add(flightno, L_new);
                
            }
            else if (special_req == "CFFT")
            {
                CFFTFlight C_new = new CFFTFlight(flightno, origin, destination, time, "On time");
                terminal.Flights.Add(flightno, C_new);
                
            }
        }
        else 
        {
            NORMFlight N_new = new NORMFlight(flightno, origin, destination, time, "On time");
            terminal.Flights.Add(flightno, N_new);
          
        }
        
        using(StreamWriter sw = new StreamWriter("flights.csv",true))
        {
            
            /*string flights = flightno + "," + origin + "," + destination + time.ToString("HH:mm tt") + "," + special_req;
            Console.WriteLine(flights);*/
            sw.WriteLine(flightno+","+origin+","+destination+","+time.ToString("HH:mm tt")+","+special_req);
            sw.Close();
        }
        Console.WriteLine("The flight(s) have been successfully added!");

        Console.Write("Do you want to add another Flight? (Y/N) : ");
        choice = Console.ReadLine().ToUpper();
    }
    



    
}
//question 9 (mingjie)
void Display_Scheduled_Flights()
{
    List<Flight> Flights_list = new List<Flight>();
    foreach (var f in terminal.Flights)
    {
        Flights_list.Add(f.Value);
    }
    Flights_list.Sort();
    
    foreach (var f in Flights_list)
    {
        bool flag = false;
        BoardingGate temp = null;
        foreach (var b in terminal.BoardingGates)
        {
            if (b.Value.Flight == f) { flag = true; temp = b.Value; break; }

            
        }
        
        if (flag)
        {
            Console.WriteLine(f.ToString()+ $"Boarding Gate: {temp.GateName}" );
        }
        else
        {
            Console.WriteLine(f.ToString());
        }
    }
}


loadfiles_airlines_and_boarding_gates();
loadfiles_flight();
assign_boarding_gate();

//question 7 (May)

void display_flight_from_airline()
{
    Console.WriteLine("==============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("==============================================");
    Console.WriteLine($"{"Airline Name",-20} {"Airline Code",-5}" + "\n");
    foreach (var a in terminal.Airlines)
    {
        Console.WriteLine($"{a.Value.Name,-20} {a.Value.Code}");
    }
    Console.Write("Enter Airline Code: ");
    string? code = Console.ReadLine();
    Airline user_airline = null;
    foreach (var a in terminal.Airlines)
    {
        if (a.Key == code)
        {
            user_airline = a.Value;
        }
    }

    foreach(var f in terminal.Flights)
    {
        Flight flight = f.Value;
        string[] flight_num = flight.FlightNumber.Split(" ");
        string airline_code = flight_num[0];
        foreach (var a in terminal.Airlines)
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
        Console.WriteLine("==============================================");
        Console.WriteLine($"List of Flights for {user_airline.Name}");
        Console.WriteLine("==============================================");
        Console.WriteLine($"{"Airline Number",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-30}");
        foreach (var f in user_airline.Flights)
        {
            Console.WriteLine($"{f.Value.FlightNumber,-20}{f.Value.Origin,-20}{f.Value.Destination,-20}{f.Value.ExpectedTime, -30}");
        }

        Console.Write("Enter the flight number to select: ");
        string user_flight = Console.ReadLine();
        Console.WriteLine($"{"Flight Number", -15}{"Airline name", -20}{"Origin", -20}{"Destination", -20}{"Expected Departure/Arrival Time", -35}{"Special Request Code", -25}{"Boarding Gate", -20}");
        foreach (var f1 in user_airline.Flights)
        {
            if (f1.Key == user_flight)
            {
                Flight f = f1.Value;
                string boarding_gate = "";
                foreach (var b in terminal.BoardingGates)
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

//display_flight_from_airline();
///*assign_boarding_gate();*/
//create_new_flight();

// question 8 (May)

void modify_flight_details()
{
    //List all the airlines with their codes
    Console.WriteLine($"{"Airline Name",-20} {"Airline Code",-5}" + "\n");
    foreach (var a in terminal.Airlines)
    {
        Console.WriteLine($"{a.Value.Name,-20} {a.Value.Code}");
    }

    //Ask user for airline code and put the user's chosen Airline object in variable user_airline
    Console.Write("Enter Airline Code: ");
    string? code = Console.ReadLine();
    Airline user_airline = null;
    foreach (var a in terminal.Airlines)
    {
        if (a.Key == code)
        {
            user_airline = a.Value;
        }
    }

    //Assign Flight objects in dictionaries of their respective airlines
    foreach (var f in terminal.Flights)
    {
        Flight flight = f.Value;
        string[] flight_num = flight.FlightNumber.Split(" ");
        string airline_code = flight_num[0];
        foreach (var a in terminal.Airlines)
        {
            if (a.Key == airline_code)
            {
                a.Value.Flights.Add(flight.FlightNumber, flight);
                break;
            }
        }
    }
    //Check if user's input airline exists, if it does, prints out all the flights in the airline's dictionary
    if (user_airline != null)
    {
        Console.WriteLine($"{"Airline Number",-20}{"Origin",-20}{"Destination",-20}");
        foreach (var f in user_airline.Flights)
        {
            Console.WriteLine($"{f.Value.FlightNumber,-20}{f.Value.Origin,-20}{f.Value.Destination,-20}");
        }

    }

    //Ask user to choose a flight to modify or delete

    Console.Write("[1] choose an existing Flight to modify, or delete: ");
    string flight_number = Console.ReadLine();
    Flight flight_to_modify = null;
    foreach (var f in terminal.Flights)
    {
        if (f.Key == flight_number)
        {
            flight_to_modify = f.Value;
            Console.WriteLine("1. Modify Flight" + "\n" +
                "2. Delete Flight");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.WriteLine(
                    "1. Modify Basic Information" + "\n" +
                    "2. Modify Status" + "\n" +
                    "3. Modify Special Request Code" + "\n" +
                    "4. Modify Boarding Gate"
                    );
                Console.Write("Choose an option: ");
                string modify_choice = Console.ReadLine();
                if (modify_choice == "1")
                {
                    Console.Write("Enter new Origin");
                    string new_origin = Console.ReadLine();
                    Console.WriteLine("Enter new Destination: ");
                    string new_desti = Console.ReadLine();
                    Console.WriteLine("Enter new Expected Departure/Arrival Time(dd/mm/yyyy hh:mm): ");
                    string new_time = Console.ReadLine();
                    f.Value.Origin = new_origin;
                    f.Value.Destination = new_desti;
                    f.Value.ExpectedTime = Convert.ToDateTime(new_time);
                    Console.WriteLine("Flight updated!");
                    Console.WriteLine($"Flight Number: {flight_to_modify.FlightNumber}" + "\n" +
                        $"Airline Name: {user_airline}" + "\n" +
                        $"Origin: {flight_to_modify.Origin}" + "\n" +
                        $"Destination: {flight_to_modify.Destination}" + "\n" +
                        $"Expected Departure/Arrival Time: {flight_to_modify.ExpectedTime}" + "\n" +
                        $"Status: {flight_to_modify.Status}" + "\n" +
                        $"Special Request Code: {flight_to_modify.}" + "\n" +);

                }

            }
        }
    }
    

}

display_flight_from_airline();
/*assign_boarding_gate();*/
create_new_flight();
//assign_boarding_gate();
//create_new_flight();
Display_Scheduled_Flights();
modify_flight_details();