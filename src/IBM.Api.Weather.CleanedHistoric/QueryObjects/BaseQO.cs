using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using IBM.Api.Weather.CleanedHistoric.Attributes;
using IBM.Api.Weather.CleanedHistoric.Extensions;

namespace IBM.Api.Weather.CleanedHistoric.QueryObjects {
    public class BaseQO
    {
        [QO("page")]
        public int? PageNumber { get; set; }

        [QO("recordsPerPage")]
        public int? RecordsPerPage { get; set; }

        /// <summary>
        /// Fellowship One changed the query variable from recordsPerPage on the activities realm
        /// </summary>
        [QO("pagesize")]
        public int? PageSize { get; set; }

        internal string ToQueryString() {
            var queryString = "?";
            queryString += string.Join('&', ToDictionary().Select(x => x.Key + "=" + x.Value.UrlEncode()));
            return queryString;
        }

        internal Dictionary<string, string> ToDictionary() {
            var ret = new Dictionary<string, string>();
            var props = GetType().GetProperties();

            foreach (var p in props) {
                if (!IgnoreProperty(p)) {
                    if (IsRightType(p.PropertyType)) {
                        var value = p.GetValue(this, null);
                        if (value != null && value.ToString() != string.Empty) {// null means the property won't be the part of search parameters
                                                                                //ret.Add(GetKey(p), value.ToString());
                            var d = value as Nullable<DateTime>;
                            if (d != null) { // DateTime need special handling for converting to string.
                                var format = GetFormat(p);
                                ret.Add(GetKey(p), d.Value.ToString(format == null ? "MM/dd/yyyy" : format));
                            } else {
                                if (value is System.Enum) {
                                    ret.Add(GetKey(p), ToDescription((System.Enum)value));
                                } else {
                                    ret.Add(GetKey(p), value.ToString());
                                }
                            }
                        }
                    } else {
                        var message = "All the properties in the DataAccess query object have to be nullable primitive or nullabel datetime or nullable enum or string, property \"" + p.Name + "\" has type : " + p.PropertyType.ToString();
                        throw new Exception(message);
                    }
                }
            }
            return ret;
        }

        private bool IgnoreProperty(PropertyInfo pi) {
            var pa = pi.GetCustomAttributes(typeof(QOIgnoreAttribute), false);
            return pa.Length > 0;
        }

        private string GetKey(PropertyInfo pi) {
            var pa = pi.GetCustomAttributes(typeof(QOAttribute), false);
            return pa.Length == 0 ? pi.Name : ((QOAttribute)pa[0]).Value;
        }

        private string GetFormat(PropertyInfo pi) {
            var pa = pi.GetCustomAttributes(typeof(QOAttribute), false);
            return pa.Length == 0 ? null : ((QOAttribute)pa[0]).Format;
        }

        private bool IsRightType(Type t) {

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                var types = t.GetGenericArguments();
                if (types.Length != 1) // we only take one argument nullable type.
                    return false;
                else
                    t = types[0];
            }

            return AllowedType(t);
        }

        private bool AllowedType(Type t) {
            return t == typeof(string) || t == typeof(DateTime) || t == typeof(TimeSpan) || t == typeof(decimal) || t.IsPrimitive || t.IsEnum;
        }

        public string ToDescription(System.Enum en) {
            if (en == null) {
                return string.Empty;
            }

            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0) {
                var attrs = memInfo[0].GetCustomAttributes(
                   typeof(DescriptionAttribute),
                   false);

                if (attrs != null && attrs.Length > 0) {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}
