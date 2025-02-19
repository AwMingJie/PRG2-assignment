﻿//==========================================================
// Student Number	: S10266864
// Student Name	: Aw Ming Jie
// Partner Name	: May Cherry Aung
//==========================================================

using System.Globalization;
using S10266864B_PRG2Assignment;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

//Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
//Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
//Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();
DateTime convert_to_dateTime(string s)
{
    DateTime dateTime = DateTime.ParseExact(s, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture);
    return dateTime;

}
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
    Console.WriteLine($"=============================================\r\nList of Flights for {terminal.TerminalName}\r\n=============================================\r\n");
    Console.WriteLine($"{ "Flight Number",-15} { "Airline Name",-20} { "Origin",-20} { "Destination",-25} { "Expected Departure/Arrival Time",-15}");

    foreach (var f in terminal.Flights)
    {
        Console.WriteLine(f.Value.DisplayFlightToString(terminal.GetAirlineFromFlight(f.Value).Name)); // terminal.GetAirlineFromFlight returns an airline class.
    }
}

//question 4 (May)
void display_boarding_gates()
{
    Console.WriteLine($"=============================================\r\nList of Boarding Gates for {terminal.TerminalName}\r\n=============================================\r\n");
    Console.WriteLine("{0, -14} {1,-12} {2,-12} {3,-12} {4, -13}", "Gate Name", "DDJB", "CFFT", "LWTT", "Flight Assigned");
    foreach (var kvp in terminal.BoardingGates)
    {
        string flight = "";
        if (kvp.Value.Flight != null)
        {
            flight = kvp.Value.Flight.FlightNumber;
        }
        else
        {
            flight = "None";
        }
        Console.WriteLine($"{kvp.Value.GateName,-14} {kvp.Value.SupportsDDJB,-12} {kvp.Value.SupportsCFFT,-12} {kvp.Value.SupportsLWTT,-12} {flight,-13}");
    }
}
// Returns a "" if input doesnt match
string check_str_is_a_str(string input_str)
{
    try
    {
        Console.Write(input_str);
        string str = Console.ReadLine();
        try
        {
            int temp = int.Parse(str);
        }
        catch (FormatException ex)
        {
            return str;
        }
        Console.WriteLine("Invalid input! Enter a string!");
        return "";
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid input! Enter a string!");
        return "";
    }
}

//Returns a -1 if input doesnt match
int check_str_is_an_int(string input_str, string range_of_inputs, int low, int high)
{
    Console.Write(input_str);
    string str = Console.ReadLine();
    try
    {
        int temp = int.Parse(str);
        if (temp < low || temp > high)
        {
            Console.WriteLine("Input not in range, enter a range between " + range_of_inputs);
            return -1;
        }
        else
        {
            return temp;
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid input, enter a range between " + range_of_inputs);
        return -1;
    }
}

//question 5 (mingjie)
void assign_boarding_gate()
{
    Console.WriteLine("=============================================\nAssign a Boarding Gate to a Flight\n=============================================\n");
    string flight_num = check_str_is_a_str("Enter Flight Number: \n");
    if (flight_num == "") { return; }
    Flight flight = terminal.Check_Flight_Given_Flight_num(flight_num);
    if (flight != null)
    {
        string boardingGate = "";
        bool condition = true;
        while (condition)
        {
            boardingGate = check_str_is_a_str("Enter Boarding Gate: \n");
            condition = terminal.Check_Boarding_Gate_Assigned(boardingGate, flight);
        }
        condition = true;
        while (condition)
        {
            string choice = check_str_is_a_str("Would you like to update the status of the Flight? (Y/N): \n");
            if (choice == "Y")
            {
                int status = -1;
                while (status == -1)
                {
                    status = check_str_is_an_int("1. Delayed\n2. Boarding\n3. On Time\nPlease select the new status of the flight:\n", "(1-3)", 1, 3);
                }
                flight.UpdateStatus(status);
                condition = false;
            }
            else if (choice == "N")
            {
                condition = false;
            }
            else
            {
                Console.WriteLine("Please type only (Y) or (N)");
            }
        }
        Console.WriteLine("Flight " + flight.FlightNumber + " has been assigned to Boarding Gate " + boardingGate + "!");
    }
    else
    {
        Console.WriteLine("Flight is not found in terminal");
    }

}
// Question 6 (mingjie)
void create_new_flight()
{
    string choice = null;
    while (choice != "N")
    {
        string flightno = "", origin = "", destination = "", timeString = "";
        while (flightno == "") { flightno = check_str_is_a_str("Enter Flight Number: "); }
        while (origin == "") { origin = check_str_is_a_str("Enter Origin: "); }
        while (destination == "") { destination = check_str_is_a_str("Enter Destination: "); }
        while (timeString == "") { timeString = check_str_is_a_str("Enter expected Departure/Arrival time (dd/mm/yyyy hh:mm): "); }
        DateTime time = convert_to_dateTime(timeString);
        string option = "";
        while (option == "")
        {
            option = check_str_is_a_str("Do you want to enter any additional information? (Y/N): ");
            if (option.ToUpper() == "Y" || option.ToUpper() == "N") { break; }
            else { option = ""; }
        }

        string special_req = null;
        if (option == "Y")
        {
            while (special_req != "DDJB" && special_req != "LWTT" && special_req != "CFFT")
            {
                special_req = check_str_is_a_str("What is the special request code: ").ToUpper();
            }
            create_flights(flightno, origin, destination, time, "", special_req);
        }
        else
        {
            create_flights(flightno, origin, destination, time, "", "");
        }

        using (StreamWriter sw = new StreamWriter("flights.csv", true))
        {
            /*string flights = flightno + "," + origin + "," + destination + time.ToString("HH:mm tt") + "," + special_req;
            Console.WriteLine(flights);*/
            sw.WriteLine(flightno + "," + origin + "," + destination + "," + time.ToString("HH:mm tt") + "," + special_req);
            sw.Close();
        }
        Console.WriteLine($"Flight {flightno} have been added!");

        choice = "";    //To Reset the choice
        while (choice != "Y" && choice != "N")
        {
            choice = check_str_is_a_str("Do you want to add another Flight? (Y/N) : ").ToUpper();
        }
    }

}
//question 9 (mingjie)
void display_scheduled_flights()
{
    Console.WriteLine($"=============================================\r\nFlight Schedule for {terminal.TerminalName}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number", 15}{"Airline Name", 20}{"Origin", 20}{"Destination", 20}{"Expected Departure/Arrival Time", 35}{"Status", 9}{"Boarding Gate", 15}");
    List<Flight> Flights_list = new List<Flight>();
    foreach (var f in terminal.Flights)
    {
        Flights_list.Add(f.Value);
    }
    Flights_list.Sort();
    
    foreach (var f in Flights_list)
    {
        string flightnum = f.FlightNumber;
        string[] flightnumArray = flightnum.Split(" ");
        Airline a = get_user_airline(flightnumArray[0]);
        bool flag = false;
        BoardingGate temp = null;
        foreach (var b in terminal.BoardingGates)
        {
            if (b.Value.Flight == f) { flag = true; temp = b.Value; break; }

            
        }
        
        if (flag)
        {
            //Console.WriteLine(f.ToString()+ $"Boarding Gate: {temp.GateName}" );
            Console.WriteLine($"{f.FlightNumber, 15}{a.Name, 20}{f.Origin, -20}{f.Destination, 20}{f.ExpectedTime, 35}{f.Status, 9}{temp.GateName, 15}");
        }
        else
        {
            //Console.WriteLine(f.ToString());
            Console.WriteLine($"{f.FlightNumber,15}{a.Name,20}{f.Origin,20}{f.Destination,20}{f.ExpectedTime,35}{f.Status,9}{"Unassigned",15}");
        }
    }
}

//question 7 (May)

//A separate function to list all the airlines
void display_airlines()
{
    Console.WriteLine("==============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("==============================================");
    Console.WriteLine($"{"Airline Code",-16} {"Airline Name",-20}" + "\n");
    foreach (var a in terminal.Airlines)
    {
        Console.WriteLine($"{a.Value.Code,-16} {a.Value.Name,-20}");
    }
}

//Another function to get user_airline
Airline get_user_airline(string code)
{
    //Console.Write("Enter Airline Code: ");
    //string? code = Console.ReadLine();
    Airline user_airline = null;
    foreach (var a in terminal.Airlines)
    {
        if (a.Value.Code == code)
        {
            user_airline = a.Value;
        }
    }
    return user_airline;
}

//Another function to list flights in airline

void put_flights_into_airlines()
{
    foreach (var f in terminal.Flights)
    {
        Flight flight = f.Value;
        string[] flight_num = flight.FlightNumber.Split(" ");
        string airline_code = flight_num[0];
        foreach (var a in terminal.Airlines)
        {
            if (a.Value.Code == airline_code)
            {
                a.Value.Flights.Add(flight.FlightNumber, flight);
                break;
            }
        }
    }
}
void list_airline_flights(Airline user_airline)
{
    if (user_airline != null)
    {
        Console.WriteLine("==============================================");
        Console.WriteLine($"List of Flights for {user_airline.Name}");
        Console.WriteLine("==============================================");
        Console.WriteLine($"{"Flight Number",-20}{"Airline Name", -20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-30}");
        foreach (var f in user_airline.Flights)
        {
            Console.WriteLine($"{f.Value.FlightNumber,-20}{user_airline.Name, -20}{f.Value.Origin,-20}{f.Value.Destination,-20}{f.Value.ExpectedTime.ToString("dd/MM/yyyy h:mm:ss tt"),-30}");
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
    string special_request_code = "None";
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

bool check_airline_input(string input)
{
    //Create a list of airline codes
    List<string> airline_codes = new List<string>();
    foreach (var kvp in terminal.Airlines)
    {
        airline_codes.Add(kvp.Value.Code);
    }
    if (airline_codes.Contains(input))
    {
        return true;
    }
    else
    {
        Console.WriteLine("Please enter the airline code (e.g, SQ).");
        return false;
    }
}
void display_all_flights_from_airlines()
{
    display_airlines();
    Console.Write("Enter Airline Code: ");
    string? user_input = Console.ReadLine();
    bool check_input = check_airline_input(user_input);
    if (check_input)
    {
        Airline user_airline = get_user_airline(user_input);
        list_airline_flights(user_airline);
    }
}

//question 7 (May)
void display_flight_from_airline()
{
    display_airlines();
    Console.Write("Enter Airline Code: ");
    string? user_input = Console.ReadLine();
    bool check_input = check_airline_input(user_input);
    if (check_input)
    {
        Airline user_airline = get_user_airline(user_input);
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
}

// question 8 (May)

void display_flight_details(Flight f)
{
    string flightNumber = f.FlightNumber;
    string airline_name = "";
    foreach (var a in terminal.Airlines)
    {
        if (a.Value.Flights.ContainsKey(flightNumber))
        {
            airline_name = a.Value.Name;
        }
    }
    string boarding_gate = get_boarding_gate_name(f);
    string special_request_code = get_special_request(f);
    DateTime expected_time = f.ExpectedTime;
    string expected_time_str = expected_time.ToString("d/M/yyyy h:mm:ss tt");
    Console.WriteLine($"Flight Number: {f.FlightNumber}" + "\n" +
        $"Airline Name: {airline_name}" + "\n" +
        $"Origin: {f.Origin}" + "\n" +
        $"Destination: {f.Destination}" + "\n" +
        $"Expected Departure/Arrival Time: {expected_time_str}" + "\n" +
        $"Status: {f.Status}" + "\n" +
        $"Special Request Code: {special_request_code}" + "\n" +
        $"Boarding Gate: {boarding_gate}");
}
void modify_flight_details()

{
    bool modifyRequestCode = false;
    string new_special_request = "";
    display_airlines();
    Console.Write("Enter Airline Code: ");
    string? user_input = Console.ReadLine();
    bool check_input = check_airline_input(user_input);
    if (check_input)
    {
        Airline user_airline = get_user_airline(user_input);
        list_airline_flights(user_airline);

        //Ask user to choose a flight to modify or delete

        Console.Write("[1] choose an existing Flight to modify, or delete: ");
        string flight_number = Console.ReadLine();
        if (terminal.Flights.ContainsKey(flight_number) == false)
        {
            Console.WriteLine("The flight number was entered incorrectly. Please start over.");

        }
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
                        Console.Write("Enter new Origin: ");
                        string new_origin = Console.ReadLine();
                        Console.Write("Enter new Destination: ");
                        string new_desti = Console.ReadLine();
                        Console.Write("Enter new Expected Departure/Arrival Time(dd/mm/yyyy hh:mm): ");
                        string new_time_input = Console.ReadLine();
                        try
                        {
                            DateTime new_time_output = convert_to_dateTime(new_time_input);
                            flight_to_modify.ExpectedTime = new_time_output;
                            flight_to_modify.Origin = new_origin;
                            flight_to_modify.Destination = new_desti;
                            Console.WriteLine("Flight updated!");
                            display_flight_details(flight_to_modify);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (modify_choice == "2")
                    {
                        Console.Write("Enter new Status: ");
                        string new_status = Console.ReadLine();
                        flight_to_modify.Status = new_status;
                        Console.WriteLine("Status modified");
                        display_flight_details(flight_to_modify);
                    }
                    else if (modify_choice == "3")
                    {
                        Console.Write("Enter new special request code('None' for no special requests): ");
                        new_special_request = Console.ReadLine();
                        List<string> special_request_list = new List<string> { "DDJB", "LWTT", "CFFT", "None" };
                        if (special_request_list.Contains(new_special_request))
                        {
                            modifyRequestCode = true;
                        }
                        else
                        {
                            Console.WriteLine("Special request code entered incorrectly. Please start over.");
                        }
                    }
                    else if (modify_choice == "4")
                    {
                        Console.Write("Enter new boarding gate: ");
                        string new_boarding_gate = Console.ReadLine();
                        string boarding_gate = get_boarding_gate_name(flight_to_modify);

                        foreach (var b in terminal.BoardingGates)
                        {
                            if (b.Key == boarding_gate)
                            {
                                b.Value.Flight = null;
                            }
                            if (b.Key == new_boarding_gate)
                            {
                                if (b.Value.Flight == null)
                                {
                                    b.Value.Flight = flight_to_modify;
                                    Console.WriteLine("Boarding gate successfully assigned.");
                                }
                                else
                                {
                                    Console.WriteLine("This boarding gate is already assigned with another flight. You cannot assign to this boarding gate.");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please start over.");
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine($"Flight {flight_to_modify.FlightNumber} has been removed.");
                    user_airline.RemoveFlight(flight_to_modify);        
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please start over.");
                }
            }
        }

        if (modifyRequestCode)
        {
            string flightnum = flight_to_modify.FlightNumber, origin = flight_to_modify.Origin, destination = flight_to_modify.Destination, status = flight_to_modify.Status;
            DateTime time = flight_to_modify.ExpectedTime;
            if (terminal.Remove_Flight_From_Terminal(flight_to_modify.FlightNumber))
            {
                create_flights(flightnum, origin, destination, time, status, new_special_request);
                Console.WriteLine("Special request code modified.");
            }
            else
            {
                Console.WriteLine("Flight not found in the Dictionary");
            }
        }


    }
}

void AdvancedB()
{
    bool unassigned_flights = false;
    foreach (var flight in terminal.Flights)
    {
        string gate_name = get_boarding_gate_name(flight.Value);
        if (gate_name == "Unassigned")
        {
            unassigned_flights = true;
            break;
        }
    }

    if (unassigned_flights == false) // all flights have been assigned
    {
        double terminal_total = 0;
        double terminal_discount = 0;
        double terminal_discounted_total = 0;
        Console.WriteLine($"{"Airline Name",-20} {"Total Fees Before Discount",-30} {"Total Discounts",-19} {"Final Total Fees",-16}");
        foreach (var a in terminal.Airlines)
        {
            double airline_total = 0;
            double airline_discount = 0;
            foreach (var f in a.Value.Flights)
            {
                double flight_total = f.Value.CalculateFees();
                double flight_discount = 0;
                airline_total += flight_total;
                //Each flight before 11am and after 9pm gets a discount of -100
                if (f.Value.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) || f.Value.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                {
                    flight_discount += 110;
                }
                //Each flight with Origin (Dubai, Bangkok or Tokyo gets -25 discount
                if (f.Value.Origin == "DXB" || f.Value.Origin == "BKK" || f.Value.Origin == "NRT")
                {
                    flight_discount += 25;
                }
                //Each flight not indicating any special request code
                if (f.Value.GetType() == typeof(NORMFlight))
                {
                    flight_discount += 50;
                }
                airline_discount += flight_discount;
            }
            double airline_discounted_total = airline_total - airline_discount;
            terminal_total += airline_total;
            terminal_discount += airline_discount;
            terminal_discounted_total += airline_discounted_total;
            Console.WriteLine($"{a.Value.Name,-20} {airline_total,26} {airline_discount,19} {airline_discounted_total,20}");
        }
        double discount_percent = (terminal_discount / terminal_total) * 100;
        Console.WriteLine($"Subtotal of All Airline Fees Before Discount : ${terminal_total}");
        Console.WriteLine($"Subtotal of All Airline Discounts : ${terminal_discount}");
        Console.WriteLine($"Final Total of All Airline Fees : ${terminal_discounted_total}");
        Console.WriteLine($"Total Discount Received (in Percentage): {discount_percent.ToString("0.00")}%");
    }
    else
    {
        Console.WriteLine("Please have all the flights assigned to boarding gates to carry out this action.");
    }
}

bool main()
{
    Console.WriteLine($"{terminal.ToString()}1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n8. Assign Flight In Bulk\r\n9. Calculate Airline Fees\r\n0. Exit\r\n\r\nPlease select your option:\r\n");
    try
    {
        int option = Convert.ToInt32(Console.ReadLine());
        if (option < 10 && option > -1)
        {
            if (option == 1)
            {
                display_flights();
            }
            else if (option == 2)
            {
                display_boarding_gates();
            }
            else if (option == 3) 
            { 
                assign_boarding_gate(); 
            }
            else if (option == 4) 
            {
                create_new_flight();
            }
            else if (option == 5) 
            {
                display_all_flights_from_airlines();
            }
            else if (option == 6) 
            { 
                modify_flight_details();
            }
            else if (option == 7) 
            {
                display_scheduled_flights();
            }
            else if (option == 8)
            {
                terminal.AdvancedA();
            }
            else if (option == 9)
            {
                AdvancedB();
            }

            else if (option == 0) 
            {
                Console.WriteLine("Goodbye!");
                return false;
            }
            //To make space between output and menu
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("Please choose a valid option.");
        }
        return true;
    }
    catch (FormatException ex)
    {
        Console.WriteLine(ex.Message);
        return true;
    }    
}

loadfiles_airlines_and_boarding_gates();
loadfiles_flight();
put_flights_into_airlines();
/*bool test = true;
while (test)
{
    test = main();
}*/

Console.WriteLine($"Loading Airlines...\r\n{terminal.Airlines.Count} Airlines Loaded!\r\nLoading Boarding Gates...\r\n{terminal.BoardingGates.Count} Boarding Gates Loaded!\r\nLoading Flights...\r\n{terminal.Flights.Count} Flights Loaded!\r\n");
while (main()) { }