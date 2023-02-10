using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
           

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);        

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command
            var locations = lines.Select(parser.Parse).ToArray();

            

           

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tb1 = null;
            ITrackable tb2 = null;
            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

           
            // Do a loop for your locations to grab each location as the origin 
            for(var i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (var j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    if(corA.GetDistanceTo(corB) > distance) 
                    {
                        distance = corA.GetDistanceTo(corB);
                        tb1 = locA;
                        tb2 = locB;
                    }
                }


            }
            Console.WriteLine($" {tb1.Name} and {tb2.Name} have the greatest distance between them");
            Console.WriteLine($" they are {Math.Round(distance * .00062, 2)} miles apart.");
            
            
            


            
        }
    }
}
