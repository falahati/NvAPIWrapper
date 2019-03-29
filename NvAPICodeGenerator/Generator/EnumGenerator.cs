using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NvAPICodeGenerator.Generator
{
    internal class EnumGenerator<T> : CodeGeneratorBase where T : struct
    {
        private readonly Dictionary<string, string> _descriptions;
        private readonly string _name;
        private readonly FriendlyNameRuleSet _nameRuleSet;
        private readonly string _ns;
        private readonly Dictionary<string, T> _values;
        private readonly DirectoryInfo _writeDirectory;

        // ReSharper disable once TooManyDependencies
        public EnumGenerator(
            DirectoryInfo writeDirectory,
            string name,
            Dictionary<string, T> values,
            FriendlyNameRuleSet nameRuleSet,
            string ns = null,
            Dictionary<string, string> descriptions = null)
        {
            _writeDirectory = writeDirectory;
            _name = name;
            _nameRuleSet = nameRuleSet;
            _values = values;
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

                writer.WriteLine($"{extraSpaces}public enum {_name} : {typeof(T).Name}");
                writer.WriteLine($"{extraSpaces}{{");

                var formattedValues = _values
                    .ToDictionary(pair => pair.Key, pair => GetEnumValueAsString(pair.Value))
                    .Where(pair => !string.IsNullOrWhiteSpace(pair.Value))
                    .ToArray();

                for (var i = 0; i < formattedValues.Length; i++)
                {
                    var formattedValue = formattedValues[i];

                    if (_descriptions?.ContainsKey(formattedValue.Key) == true &&
                        !string.IsNullOrWhiteSpace(_descriptions[formattedValue.Key]))
                    {
                        var description = _descriptions[formattedValue.Key]
                            .Trim()
                            .Replace("\"", "\\\"")
                            .Replace("\r", "\\r")
                            .Replace("\n", "\\n");

                        writer.WriteLine($"{extraSpaces}    /// <summary>");
                        writer.WriteLine($"{extraSpaces}    ///    {description}");
                        writer.WriteLine($"{extraSpaces}    /// </summary>");
                        writer.WriteLine($"{extraSpaces}    [Description(\"{description}\")]");
                    }

                    var friendlyName = _nameRuleSet.Apply(formattedValue.Key);
                    if (!char.IsLetter(friendlyName[0]))
                    {
                        friendlyName = "_" + friendlyName;
                    }
                    writer.WriteLine(
                        $"{extraSpaces}    {friendlyName} = {formattedValue.Value}" +
                        (i == formattedValues.Length - 1 ? "" : ",\r\n")
                    );
                }

                writer.WriteLine($"{extraSpaces}}}");

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


        private string GetEnumValueAsString(T value)
        {
            // ReSharper disable once InterpolatedStringExpressionIsNotIFormattable
            return $"0x{value:X}";
        }
    }
}