using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.Portable.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public int AttendeeCapacity { get; set; }
        public int AttendeeCount { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string EventyType { get; set; }
    }
}
