using System.ComponentModel;

namespace IBM.Api.Weather.CleanedHistoric.Enum {
    public enum Interval {
        [Description("hourly")]
        Hourly,

        [Description("daily")]
        Daily,

        [Description("monthly")]
        Monthly
    }
}
