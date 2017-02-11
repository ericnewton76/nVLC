//    nVLC
//
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
// ========================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace nVLC.Utils
{
    internal class EnumUtils
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static Dictionary<string, Enum> GetEnumMapping(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Enum type expected");
            }

            Dictionary<string, Enum> dic = new Dictionary<string, Enum>();
            var values = Enum.GetValues(enumType);
            foreach (Enum item in values)
            {
                dic.Add(GetEnumDescription(item), item);
            }

            return dic;
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>An object of type T with the specified value</returns>
        public static bool TryParse<T>(string value, out T result)
        {
            try {
                result = (T)System.Enum.Parse(typeof(T), value);
                return true;
            } catch {
                result = default(T);
                return false;
            }
        }
    }
}
