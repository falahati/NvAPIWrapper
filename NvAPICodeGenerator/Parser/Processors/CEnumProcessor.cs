using System;
using NvAPICodeGenerator.Parser.Models;

namespace NvAPICodeGenerator.Parser.Processors
{
    // ReSharper disable once HollowTypeName
    internal class CEnumProcessor : CProcessorBase

    {
        /// <inheritdoc />
        public override bool CanParse(string str)
        {
            return str.Trim().StartsWith("enum ", StringComparison.InvariantCulture);
        }

        /// <inheritdoc />
        public override CProcessorParseResult Parse(string str, CProcessorState state)
        {
            if (!state.Values.GetValue<bool>("InEnum"))
            {
                str = str.Substring("enum ".Length).Trim();
                var openIndex = str.IndexOf("{", StringComparison.CurrentCulture);
                var name = str.Substring(0, openIndex).Trim();
                state.Values.SetValue("InEnum", true);
                var e = new CEnum(name, state.CurrentTree);
                state.CurrentTree.AddSubTree(e);

                return new CProcessorParseResult(e, false, string.Empty);
            }

            str = str.Trim();
            var equalIndex = str.IndexOf("=", StringComparison.InvariantCulture);

            if (equalIndex > 0)
            {
                var name = str.Substring(0, equalIndex).Trim();
                str = str.Substring(equalIndex + 1).Trim();
                var nextIndex = str.IndexOf(",", StringComparison.InvariantCulture);
                var value = nextIndex > 0 ? str.Substring(0, nextIndex).Trim() : str.Trim();
                var eValue = new CEnumValue(name, value, state.CurrentTree);
                state.CurrentTree.AddSubTree(eValue);

                return new CProcessorParseResult(state.CurrentTree, false, string.Empty);
            }

            if (str.IndexOf("}", StringComparison.InvariantCulture) >= 0)
            {
                return new CProcessorParseResult(state.CurrentTree.Parent, true, string.Empty);
            }

            // Skip Line
            return new CProcessorParseResult(state.CurrentTree, false, string.Empty);
        }
    }
}