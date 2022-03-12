using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Ticket.Extensions
{

    public static class EnumExtension
    {

        /// <summary>取得指定列舉的描述</summary>
        /// <param name="value">要取得描述的列舉</param>
        /// <returns>傳回描述，或是依照指定的強制處理狀況，回應其列舉名稱或擲出例外</returns>
        public static string GetEnumDescription<T>(this T value)
        {
            Type type = value.GetType();

            //Make sure the object is an enum.
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("其值必須為列舉");
            }
            FieldInfo fieldInfo = type.GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttributes == null || descriptionAttributes.Count() == 0)
            {
                return value.ToString();
            }
            else if (descriptionAttributes.Count() > 1)
            {
                throw new Exception($"列舉類型「{type.Name}」有過多的Description屬性，相對應的列舉為「{value.ToString()}」");
            }

            //Return the value of the DescriptionAttribute.
            return (descriptionAttributes.First() as DescriptionAttribute).Description;
        }

    }
}

