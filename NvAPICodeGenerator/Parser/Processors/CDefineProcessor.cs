using System;
using NvAPICodeGenerator.Parser.Models;

namespace NvAPICodeGenerator.Parser.Processors
{
    // ReSharper disable once HollowTypeName
    internal class CDefineProcessor : CProcessorBase
    {
        /// <inheritdoc />
        public override bool CanParse(string str)
        {
            return str?.Trim().StartsWith("#define ") == true;
        }

        /// <inheritdoc />
        public override CProcessorParseResult Parse(string str, CProcessorState state)
        {
            str = str.Trim().Substring("#define ".Length).Trim();
            var spaceIndex = str.IndexOf(" ", StringComparison.CurrentCulture);

            string name;
            string value;

            if (spaceIndex >= 0)
            {
                name = str.Substring(0, spaceIndex).Trim();
                str = str.Substring(spaceIndex).Trim();
                value = str.Trim();
                str = string.Empty;
            }
            else
            {
                name = str.Trim();
                value = true.ToString();
                str = string.Empty;
            }

            var define = new CDefine(name, value, state.CurrentTree);
            state.CurrentTree.AddSubTree(define);

            return new CProcessorParseResult(state.CurrentTree, true, str);
        }
    }
}