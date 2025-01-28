//==========================================================
// Student Number	: S10266864
// Student Name	: Aw Ming Jie
// Partner Name	: May Cherry Aung
//==========================================================


using S10266864B_PRG2Assignment;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

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

void create_flights(string num, string origin, string desti, DateTime time,string status, string special_code)
{
    if (special_code == "DDJB")
    {
        DDJBFlight d1 = new DDJBFlight(num, origin, desti, time, status, 300.0);
        terminal.Flights.Add(num, d1);
    }
    else if (special_code == "CFFT")
    {
        CFFTFlight c1 = new CFFTFlight(num, origin, desti, time, status, 150.0);
        terminal.Flights.Add(num, c1);
    }
    else if (special_code == "LWTT")
    {
        LWTTFlight l1 = new LWTTFlight(num, origin, desti, time, status, 500.0);
        terminal.Flights.Add(num, l1);
    }
    else
    {
        NORMFlight n1 = new NORMFlight(num, origin, desti, time, status);
        terminal.Flights.Add(num, n1);
    }
}
void loadfiles_flight()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(',');
            string special_code = data[4];
            string flight_num = data[0];
            string flight_origin = data[1];
            string flight_destination = data[2];
            DateTime expected_time = Convert.ToDateTime(data[3]);
            string flight_status = "on time";
            create_flights(flight_num, flight_origin, flight_destination, expected_time, flight_status, special_code);
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
    string flight_num = Console.ReadLine();
    bool flag = false;
    Flight flight = null;
    foreach (var f in terminal.Flights)
    {
        if (f.Key == flight_num)
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
                DDJBFlight D_new = new DDJBFlight(flightno, origin, destination, time, "On time", 300.0);
                terminal.Flights.Add(flightno, D_new);
                
            }
            else if (special_req == "LWTT")
            {
                LWTTFlight L_new = new LWTTFlight(flightno, origin, destination, time, "On time", 500.0);
                terminal.Flights.Add(flightno, L_new);
                
            }
            else if (special_req == "CFFT")
            {
                CFFTFlight C_new = new CFFTFlight(flightno, origin, destination, time, "On time", 150.0);
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

//A separate function to list all the airlines
void display_airlines()
{
    Console.WriteLine("==============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("==============================================");
    Console.WriteLine($"{"Airline Name",-20} {"Airline Code",-5}" + "\n");
    foreach (var a in terminal.Airlines)
    {
        Console.WriteLine($"{a.Value.Name,-20} {a.Value.Code}");
    }
}

//Another function to get user_airline
Airline get_user_airline ()
{
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
    return user_airline;
}

//Another function to list flights in airline
void list_airline_flights(Airline user_airline)
{
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
    if (user_airline != null)
    {
        Console.WriteLine("==============================================");
        Console.WriteLine($"List of Flights for {user_airline.Name}");
        Console.WriteLine("==============================================");
        Console.WriteLine($"{"Airline Number",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-30}");
        foreach (var f in user_airline.Flights)
        {
            Console.WriteLine($"{f.Value.FlightNumber,-20}{f.Value.Origin,-20}{f.Value.Destination,-20}{f.Value.ExpectedTime,-30}");
        }
    }
}

string get_boarding_gate_name(Flight user_flight)
{
    string boarding_gate = "Unassigned";
    foreach (var b in terminal.BoardingGates)
    {
        if (b.Value.Flight == user_flight)
        {
            boarding_gate = b.Value.GateName;
        }
    }
    return boarding_gate;
}
string get_special_request(Flight f)
{
    string special_request_code = "";
    foreach (var f1 in terminal.Flights)
    {
        if (f1.Value == f)
        {
            Flight user_flight = f1.Value;
         
            if (f.GetType() == typeof(LWTTFlight))
            {
                special_request_code = "LWTT";
            }
            else if (f.GetType() == typeof(CFFTFlight))
            {
                special_request_code = "CFFT";
            }
            else if (f.GetType() == typeof(DDJBFlight))
            {
                special_request_code = "DDJB";
            }
        }
    }
    return special_request_code;
}
void display_flight_from_airline()
{
    display_airlines();
    Airline user_airline = get_user_airline();
    list_airline_flights(user_airline);
    Console.Write("Enter the flight number to select: ");
    string user_flight_num = Console.ReadLine();
    Flight user_flight = null;
    foreach (var f in terminal.Flights)
    {
        if (f.Value.FlightNumber == user_flight_num)
        {
            user_flight = f.Value;
        }
    }
    string boarding_gate = get_boarding_gate_name(user_flight);
    string special_request_code = get_special_request(user_flight);
    Console.WriteLine($"{"Flight Number",-15}{"Airline name",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-35}{"Special Request Code",-25}{"Boarding Gate",-20}");

    Console.WriteLine($"{user_flight.FlightNumber,-15}{user_airline.Name,-20}{user_flight.Origin,-20}{user_flight.Destination,-20}{user_flight.ExpectedTime,-35}{special_request_code,-25}{boarding_gate,-20}");
}

//display_flight_from_airline();
///*assign_boarding_gate();*/
//create_new_flight();

// question 8 (May) NOT FINISHED

void display_flight_details(Flight f)
{
    Airline user_airline = get_user_airline();
    string boarding_gate = get_boarding_gate_name(f);
    string special_request_code = get_special_request(f);
    Console.WriteLine($"Flight Number: {f.FlightNumber}" + "\n" +
        $"Airline Name: {user_airline}" + "\n" +
        $"Origin: {f.Origin}" + "\n" +
        $"Destination: {f.Destination}" + "\n" +
        $"Expected Departure/Arrival Time: {f.ExpectedTime}" + "\n" +
        $"Status: {f.Status}" + "\n" +
        $"Special Request Code: {special_request_code}" + "\n" +
        $"Boarding Gate: {boarding_gate}");
}
void modify_flight_details() { }
{
    //NOT FINISHED
    //display_airlines();
    //Airline user_airline = get_user_airline();
    //list_airline_flights(user_airline);

    ////Ask user to choose a flight to modify or delete

    //Console.Write("[1] choose an existing Flight to modify, or delete: ");
    //string flight_number = Console.ReadLine();
    //Flight flight_to_modify = null;
    //foreach (var f in terminal.Flights)
    //{
    //    if (f.Key == flight_number)
    //    {
    //        flight_to_modify = f.Value;
    //        Console.WriteLine("1. Modify Flight" + "\n" +
    //            "2. Delete Flight");
    //        Console.Write("Choose an option: ");
    //        string choice = Console.ReadLine();
    //        if (choice == "1")
    //        {
    //            Console.WriteLine(
    //                "1. Modify Basic Information" + "\n" +
    //                "2. Modify Status" + "\n" +
    //                "3. Modify Special Request Code" + "\n" +
    //                "4. Modify Boarding Gate"
    //                );
    //            Console.Write("Choose an option: ");
    //            string modify_choice = Console.ReadLine();
    //            if (modify_choice == "1")
    //            {
    //                Console.Write("Enter new Origin");
    //                string new_origin = Console.ReadLine();
    //                Console.WriteLine("Enter new Destination: ");
    //                string new_desti = Console.ReadLine();
    //                Console.WriteLine("Enter new Expected Departure/Arrival Time(dd/mm/yyyy hh:mm): ");
    //                string new_time = Console.ReadLine();
    //                flight_to_modify.Origin = new_origin;
    //                flight_to_modify.Destination = new_desti;
    //                flight_to_modify.ExpectedTime = Convert.ToDateTime(new_time);
    //                Console.WriteLine("Flight updated!");
    //                display_flight_details(flight_to_modify);
    //            }
    //            else if (modify_choice == "2")
    //            {
    //                Console.Write("Enter new Status");
    //                string new_status = Console.ReadLine();
    //                flight_to_modify.Status = new_status;
    //                Console.WriteLine("Status modified");
    //                display_flight_details(flight_to_modify);
    //            }
    //            else if (modify_choice == "3")
    //            {
    //                Console.Write("Enter new special request code: ");
    //                string new_special_request = Console.ReadLine();
    //                create_flights(flight_to_modify.FlightNumber, flight_to_modify.Origin, flight_to_modify.Destination, flight_to_modify.ExpectedTime, flight_to_modify.Status, new_special_request);
    //                Console.WriteLine("Special request code modified.");
    //                display_flight_details(flight_to_modify);
    //            }
    //            else if(modify_choice == "4") {
    //                Console.Write("Enter new boarding gate: ");
    //                string new_boarding_gate = Console.ReadLine();
    //                string boarding_gate = get_boarding_gate_name(flight_to_modify);

    //                foreach (var b in terminal.BoardingGates)
    //                {
    //                    if (b.Key == boarding_gate)
    //                    {
    //                        b.Value.Flight = null;
    //                    }
    //                }
    }
    

display_flight_from_airline();
/*assign_boarding_gate();*/
create_new_flight();
//assign_boarding_gate();
//create_new_flight();
Display_Scheduled_Flights();
modify_flight_details();