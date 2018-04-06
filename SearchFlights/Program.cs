using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class flight
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string DepartureTime { get; set; }
    public string DestinationTime { get; set; }
    public string price { get; set; }
}

class Program
{
    StreamReader r;
    List<string> lines;
    List<flight> flightData;
    public List<string> readFiles()
    {
        string[] filePath = new string[] { "text1withcomma.txt", "text2withcomma.txt", "text3withline.txt" };
        lines = new List<string>();
        foreach (var item in filePath)
        {
            using (r = new StreamReader(@"D:\firstFlight\" + item))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }
        return lines;
    }

    public List<flight> sortData(List<string> FlightList)
    {
        string[] dataArray;
        flightData = new List<flight>();
        List<flight> sortedFlightList;
        var uniqueFlight = (from uniqueFlightList in FlightList select uniqueFlightList).Distinct().ToList();
        foreach (var line in uniqueFlight)
        {
            if ((line != "Origin,Departure Time,Destination,Destination Time,Price") && (line != "Origin|Departure Time|Destination|Destination Time|Price"))
            {
                if (line.Contains(","))
                {
                    dataArray = line.Split(',');
                    flight flt = new flight()
                    {
                        Origin = dataArray[0].ToLower(),
                        DepartureTime = dataArray[1],
                        Destination = dataArray[2].ToLower(),
                        DestinationTime = dataArray[3],
                        price = dataArray[4]
                    };

                    flightData.Add(flt);
                }
                else
                {
                    dataArray = line.Split('|');
                    flight flt = new flight()
                    {
                        Origin = dataArray[0],
                        DepartureTime = dataArray[1],
                        Destination = dataArray[2],
                        DestinationTime = dataArray[3],
                        price = dataArray[4]
                    };
                    flightData.Add(flt);
                }
            }
        }
        sortedFlightList = flightData.OrderBy(x => x.price).ToList();

        return sortedFlightList;

    }

    public void searchFlight(string origin, string destination, List<flight> fullFlightList)
    {
        bool isflightAvailable = false;
        fullFlightList = fullFlightList.Select(i => new flight
        {
            Origin = i.Origin,
            Destination = i.Destination,
            DepartureTime = i.DepartureTime,
            DestinationTime = i.DestinationTime,
            price = i.price
        })
            .Where(x => x.Origin == origin && x.Destination == destination).ToList<flight>();

        foreach (var item in fullFlightList)
        {
            isflightAvailable = true;
            Console.WriteLine(item.Origin + " --> " + item.Destination + "  " + item.DepartureTime + " --> "
                + item.DestinationTime + " - " + item.price);
        }
        if (!isflightAvailable)
        {
            Console.WriteLine("flight not found From " + origin + " TO " + destination);
        }
    }

    static void Main()
    {
        Program program = new Program();
        List<string> readf = program.readFiles();
        var data = program.sortData(readf);
        Console.WriteLine("------------Search Flight Application------------\n");
        Console.WriteLine("Enter Your Origin -O");
        var origin = Console.ReadLine();
        Console.WriteLine("Enter Your Destination -d");
        var destination = Console.ReadLine();
        program.searchFlight(origin, destination, data);
        Console.ReadLine();
    }
}

