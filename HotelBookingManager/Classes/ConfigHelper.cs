using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HotelBookingManager.Classes
{
    internal class ConfigHelper
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
