using System.ComponentModel;

namespace IBM.Api.Weather.CleanedHistoric.Enum {
    public enum Time {
        [Description("lwt")]
        Local,

        [Description("gmt")]
        UTC,
    }
}
