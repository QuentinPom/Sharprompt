﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Sharprompt.Forms;
using Sharprompt.Internal;

namespace Sharprompt
{
    public static class Prompt
    {
        public static T Input<T>(string message, object defaultValue = null, IList<Func<object, ValidationResult>> validators = null)
        {
            using var form = new Input<T>(message, defaultValue, validators);

            return form.Start();
        }

        public static string Password(string message, IList<Func<object, ValidationResult>> validators = null)
        {
            using var form = new Password(message, validators);

            return form.Start();
        }

        public static bool Confirm(string message, bool? defaultValue = null)
        {
            using var form = new Confirm(message, defaultValue);

            return form.Start();
        }

        public static T Select<T>(string message, int? pageSize = null, T? defaultValue = null) where T : struct, Enum
        {
            var items = EnumValue<T>.GetValues();

            using var form = new Select<EnumValue<T>>(message, items, pageSize, (EnumValue<T>)defaultValue, x => x.DisplayName);

            return form.Start().Value;
        }

        public static T Select<T>(string message, IEnumerable<T> items, int? pageSize = null, object defaultValue = null, Func<T, string> valueSelector = null)
        {
            using var form = new Select<T>(message, items, pageSize, defaultValue, valueSelector ?? (x => x.ToString()));

            return form.Start();
        }

        public static IEnumerable<T> MultiSelect<T>(string message, int? pageSize = null, int minimum = 1, int maximum = -1) where T : struct, Enum
        {
            var items = EnumValue<T>.GetValues();

            using var form = new MultiSelect<EnumValue<T>>(message, items, pageSize, minimum, maximum, x => x.DisplayName);

            return form.Start().Select(x => x.Value);
        }

        public static IEnumerable<T> MultiSelect<T>(string message, IEnumerable<T> items, int? pageSize = null, int minimum = 1, int maximum = -1, Func<T, string> valueSelector = null)
        {
            using var form = new MultiSelect<T>(message, items, pageSize, minimum, maximum, valueSelector ?? (x => x.ToString()));

            return form.Start();
        }

        public static class ColorSchema
        {
            public static ConsoleColor Answer { get; set; } = ConsoleColor.Cyan;
            public static ConsoleColor Select { get; set; } = ConsoleColor.Green;
            public static ConsoleColor DisabledOption { get; set; } = ConsoleColor.DarkCyan;
        }

        public static class Symbols
        {
            public static Symbol Prompt { get; set; } = new Symbol("?", "?");
            public static Symbol Done { get; set; } = new Symbol("✔", "V");
            public static Symbol Error { get; set; } = new Symbol("»", ">>");
            public static Symbol Selector { get; set; } = new Symbol("›", ">");
            public static Symbol Selected { get; set; } = new Symbol("◉", "(*)");
            public static Symbol NotSelect { get; set; } = new Symbol("◯", "( )");
        }
    }
}
