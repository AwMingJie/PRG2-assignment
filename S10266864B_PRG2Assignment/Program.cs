using S10266864B_PRG2Assignment;

void loadfiles_flight()
{
    using (StreamReader sr = new StreamReader("fligts.csv"))
    {
        string? s = sr.ReadLine();
        while ((s=sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            if (data[4] == "DDJB")
            {
                DDJBFlight d1 = new DDJBFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]),Convert.ToDateTime(data[3]), "On Time",300);
            }
            else if (data[4] == "CFFT")
            {
                CFFTFlight c1 = new CFFTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time", 150);
            }
            else if (data[4] == "LWTT")
            {
                LWTTFlight l1 = new LWTTFlight(Convert.ToString(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDateTime(data[3]), "On Time", 500);
            }
            
        }
        
        
    }
}