using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace IBM.Api.Weather.CleanedHistoric.Enum {
    public enum WeatherStation {
        /// <summary>
        /// Use the closest virtual grid point to the requested location.
        /// You are guaranteed to have data returned for the entire time frame requested when using this value 
        /// </summary>
        [Description("cfsr")]
        Closest,

        /// <summary>
        /// Will conduct a nearest neighbor search and chooses a METAR station if it is 17.5 km or less from the requested location.
        /// If a METAR station is used, you are not guaranteed to have data returned for the entire time frame requested.
        /// METAR data is only returned for the period of the requested time period in which it is available.
        /// Premium Weather Variables are not available when using this option.
        /// </summary>
        [Description("metar")]
        Metar
    }
}
