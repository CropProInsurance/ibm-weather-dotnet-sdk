using System.ComponentModel;

namespace IBM.Api.Weather.CleanedHistoric.Enum {
    public enum Delivery {
        /// <summary>
        /// Data is delivered directly to the browser or the application that makes the API call
        /// </summary>
        [Description("stream")]
        Stream,

        /// <summary>
        /// Data is delivered in a file that is downloaded to your system 
        /// </summary>
        [Description("file")]
        File,
    }
}
