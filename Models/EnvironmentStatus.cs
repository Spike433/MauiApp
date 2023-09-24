using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirApp.Models
{
    public class EnvironmentStatus
    {
        public int Id { get; set; }

        public int? Uuid { get; set; }

        public int? SerialNumber { get; set; }

        public DateTime? DeviceEpoch { get; set; }

        public int? ConfigurationNumber { get; set; }

        public double? AbsoluteAirPressure { get; set; }

        public double? AirHumidity { get; set; }

        public double? AirTemperature { get; set; }

        public string? IndoorAirQuality { get; set; }

        public double? BatteryLevel { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }    
    }  
}
