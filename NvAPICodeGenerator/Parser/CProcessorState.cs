using NvAPICodeGenerator.Parser.Models;

namespace NvAPICodeGenerator.Parser
{
    internal class CProcessorState
    {
        /// <inheritdoc />
        public CProcessorState(CTree currentTree, CHeaderFileParser parser, CProcessorStateValues globalValues)
        {
            CurrentTree = currentTree;
            Parser = parser;
            GlobalValues = globalValues;
            Values = new CProcessorStateValues();
        }

        public CTree CurrentTree { get; set; }
        public CProcessorStateValues GlobalValues { get; }
        public CHeaderFileParser Parser { get; }
        public CProcessorStateValues Values { get; }
    }
}