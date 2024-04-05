using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;

namespace ProfitBaseAPILibraly.Classes.SubClasses
{
    public class CastomProperty
    {
        private object value;

        [JsonProperty("value")]
        public object Value { get => GetValue(); set => this.value = value; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public Type Type { get; set; } = typeof(string);

        internal CastomProperty() { }

        public CastomProperty(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public CastomProperty(string id, string name, object value) : this(id, name) => Value = value;

        public CastomProperty(string id, string name, object value, Type type) : this(id, name, value) => Type = type;

        public object GetValue()
        {
            try
            {
                if (value == null) return null;
                if (Type == null) return null;

                return Convert.ChangeType(value, Type, CultureInfo.CurrentCulture);
            }
            catch (InvalidCastException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            catch (FormatException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public void SetValue(string value, Type type)
        {
            Value = value;
            Type = type;
        }
    }
}
