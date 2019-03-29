using System;
using System.Linq;

namespace NvAPICodeGenerator.Parser.Models
{
    internal class CDefine : CTree
    {
        /// <inheritdoc />
        public CDefine(string name, string value, CTree parentTree) : base(name, parentTree)
        {
            Value = value;
        }

        public string Value { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} = {Value}";
        }

        public object ResolveValue()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return null;
            }

            if (Value.StartsWith("L\""))
            {
                return Value.Substring(2, Value.Length - 3);
            }

            if (Value.StartsWith("0x"))
            {
                return Convert.ToInt64(Value.Substring(2), 16);
            }

            if (long.TryParse(Value, out var lValue))
            {
                return lValue;
            }

            if (bool.TryParse(Value, out var bValue))
            {
                return bValue;
            }

            var reference = Parent.SubTrees.FirstOrDefault(tree => tree.Name == Value);

            if (reference != null && reference is CDefine cDefine)
            {
                return cDefine.ResolveValue();
            }

            return Value;
        }
    }
}