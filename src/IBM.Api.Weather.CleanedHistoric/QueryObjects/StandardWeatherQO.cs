using System;
using System.Collections.Generic;
using System.Text;
using IBM.Api.Weather.CleanedHistoric.Attributes;
using IBM.Api.Weather.CleanedHistoric.Enum;

namespace IBM.Api.Weather.CleanedHistoric.QueryObjects {
    public class StandardWeatherQO : BaseQO {
        /// <summary>
        /// Indicates the starting date for weather request (Start date is first hour of requested date) 
        /// </summary>
        [QO("startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  indicates the ending date for weather request (End date is first hour of date requested,
        ///  Data will be returned between the first hour of start date and first hour of end date.  Make end date an extra day if you would like data for that day.) 
        /// </summary>
        [QO("endDate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Data can be requested either by latitude/longitude or zip code.
        /// Currently searching by zip code is only supported for US zip codes. 
        /// </summary>
        [QO("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// Data can be requested either by latitude/longitude or zip code.
        /// </summary>
        [QO("lat")]
        public double? Latitude { get; set; }

        /// <summary>
        ///  Data can be requested either by latitude/longitude or zip code.
        /// </summary>
        [QO("long")]
        public double? Longitude { get; set; }

        /// <summary>
        /// The specific data source to use for the requested location. 
        /// </summary>
        [QO("station")]
        public WeatherStation Station { get; set; } = WeatherStation.Closest;

        /// <summary>
        /// The desired temporal resolution of the data being retrieved.
        /// </summary>
        [QO("interval")]
        public Interval Interval { get; set; } = Interval.Daily;

        /// <summary>
        /// The desired units in which to express the data being retrieved
        /// </summary>
        [QO("units")]
        public Unit Unit { get; set; } = Unit.Metric;

        [QO("format")]
        public string Format => "json";

        /// <summary>
        /// Specify the time unit the requested data is returned in
        /// </summary>
        [QO("time")]
        public Time Time { get; set; } = Time.UTC;

        /// <summary>
        /// Specify how the data is returned
        /// </summary>
        [QO("delivery")]
        public Delivery Delivery { get; set; } = Delivery.Stream;
    }
}
