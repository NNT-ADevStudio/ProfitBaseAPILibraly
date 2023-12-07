﻿using System;
using System.Diagnostics;
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
            try
            {
                if (value == null) return null;
                if (string.IsNullOrEmpty(value)) return null;
                if (Type == null) return null;

                return Convert.ChangeType(value, Type, CultureInfo.CurrentCulture);
            }
            catch (InvalidCastException ex)
            {
                // Обработка исключения, возникающего, если преобразование не поддерживается
                Debug.WriteLine(ex); // Пример логирования
                return null;
            }
            catch (FormatException ex)
            {
                // Обработка исключения, возникающего, если значение не соответствует формату ожидаемого типа
                Debug.WriteLine(ex); // Пример логирования
                return null;
            }

        }

        public void SetValue(string value, Type type)
        {
            this.value = value;
            Type = type;
        }
    }
}
