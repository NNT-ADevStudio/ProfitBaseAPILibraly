using System;
using System.Globalization;

namespace ProfitBaseAPILibraly.Classes
{
    public class CastomProperty
    {
        private string value;

        public string Id { get; }

        public string Name { get; }

        public Type Type { get; private set; } = typeof(string);

        public CastomProperty(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public CastomProperty(string id, string name, string value) : this(id, name) => this.value = value;

        public CastomProperty(string id, string name, string value, Type type) : this(id, name, value) => Type = type;

        public object GetValue()
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value)) return null;
            if (Type == null) return null;
            return Convert.ChangeType(value, Type, CultureInfo.CurrentCulture);
        }

        public void SetValue(string value, Type type)
        {
            this.value = value;
            Type = type;
        }
    }
}
