using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NvAPICodeGenerator.Generator
{
    internal class ConstEnumGenerator : CodeGeneratorBase
    {
        private readonly Dictionary<string, string> _descriptions;
        private readonly string _name;
        private readonly FriendlyNameRuleSet _nameRuleSet;
        private readonly string _ns;
        private readonly Dictionary<string, object> _values;
        private readonly DirectoryInfo _writeDirectory;

        // ReSharper disable once TooManyDependencies
        public ConstEnumGenerator(
            DirectoryInfo writeDirectory,
            string name,
            Dictionary<string, object> values,
            FriendlyNameRuleSet nameRuleSet,
            string ns = null,
            Dictionary<string, string> descriptions = null)
        {
            _writeDirectory = writeDirectory;
            _name = name;
            _values = values;
            _nameRuleSet = nameRuleSet;
            _ns = ns;
            _descriptions = descriptions;
            if (!char.IsLetter(_name[0]))
            {
                _name = "_" + _name;
            }
        }

        /// <inheritdoc />
        public override CodeGeneratorResult Generate()
        {
            var isDeleted = false;
            var file = new FileInfo(Path.Combine(_writeDirectory.FullName, _name + ".cs"));

            if (file.Exists)
            {
                isDeleted = true;
                file.Delete();
            }

            using (var writer = file.CreateText())
            {
                var extraSpaces = "";

                writer.WriteLine("using System;");

                if (_descriptions?.Count > 0)
                {
                    writer.WriteLine("using System.ComponentModel;");
                }

                writer.WriteLine();

                if (!string.IsNullOrWhiteSpace(_ns))
                {
                    writer.WriteLine($"namespace {_ns}");
                    writer.WriteLine("{");
                    extraSpaces = "    ";
                }

                writer.WriteLine("#pragma warning disable 1591");
                writer.WriteLine($"{extraSpaces}public static class {_name}");
                writer.WriteLine($"{extraSpaces}{{");

                var formattedValues = _values
                    .ToDictionary(pair => pair.Key, pair => GetEnumValueAsString(pair.Value))
                    .Where(pair => !string.IsNullOrWhiteSpace(pair.Value))
                    .ToArray();

                foreach (var formattedValue in formattedValues)
                {
                    if (_descriptions?.ContainsKey(formattedValue.Key) == true &&
                        !string.IsNullOrWhiteSpace(_descriptions[formattedValue.Key]))
                    {
                        var description = _descriptions[formattedValue.Key]
                            .Trim()
                            .Replace("\"", "\\\"")
                            .Replace("\r", "\\r")
                            .Replace("\n", "\\n");

                        writer.WriteLine($"{extraSpaces}    /// <summary>");
                        writer.WriteLine($"{extraSpaces}    ///      {description}");
                        writer.WriteLine($"{extraSpaces}    /// </summary>");
                        writer.WriteLine($"{extraSpaces}    [Description(\"{description}\")]");
                    }

                    var friendlyName = _nameRuleSet.Apply(formattedValue.Key);

                    if (!char.IsLetter(friendlyName[0]))
                    {
                        friendlyName = "_" + friendlyName;
                    }

                    var constType = GetValueType(formattedValue.Value);
                    writer.WriteLine(
                        $"{extraSpaces}    public const {constType} {friendlyName} = \"{formattedValue.Value}\";"
                    );
                    writer.WriteLine();
                }

                writer.WriteLine($"{extraSpaces}}}");
                writer.WriteLine("#pragma warning restore 1591");

                if (!string.IsNullOrWhiteSpace(_ns))
                {
                    writer.WriteLine("}");
                }
            }

            return new CodeGeneratorResult(
                new[] {file},
                isDeleted ? new[] {file} : new FileInfo[0],
                _writeDirectory
            );
        }

        private string GetEnumValueAsString(object value)
        {
            var t = value.GetType();

            if (t == typeof(uint))
            {
                return ((uint) value).ToString("X");
            }

            if (t == typeof(int))
            {
                return ((int) value).ToString("X");
            }

            if (t == typeof(ushort))
            {
                return ((ushort) value).ToString("X");
            }

            if (t == typeof(short))
            {
                return ((short) value).ToString("X");
            }

            if (t == typeof(ulong))
            {
                return ((ulong) value).ToString("X");
            }

            if (t == typeof(long))
            {
                return ((long) value).ToString("X");
            }

            if (t == typeof(byte))
            {
                return ((uint) value).ToString("X");
            }

            if (t == typeof(string))
            {
                return value as string;
            }

            return null;
        }

        private Type GetValueType(object value)
        {
            return value.GetType();
        }
    }
}