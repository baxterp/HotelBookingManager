using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HotelBookingManager.Classes
{
    public class ConfigHelper
    {
        public static List<int> GetRoomList()
        {
            // This data would normally be loaded from a database, so appSettings option used
            return ConfigurationManager.AppSettings["roomNumberList"]
                    .Split(',')
                    .Where(x => int.TryParse(x, out _))
                    .Select(int.Parse)
                    .ToList();
        }
    }
}
