using S10266864B_PRG2Assignment;

Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();
void loadfiles_airlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s= sr.ReadLine()) != null)
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
            BoardingGates.Add(data[0],boardingGate);
        }
    }
}