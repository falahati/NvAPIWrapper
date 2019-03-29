using NvAPICodeGenerator.Parser.Models;

namespace NvAPICodeGenerator.Parser
{
    internal class CProcessorParseResult
    {
        /// <inheritdoc />
        public CProcessorParseResult(CTree currentTree, bool endOfProcess, string unParsed)
        {
            CurrentTree = currentTree;
            EndOfProcess = endOfProcess;
            UnParsed = unParsed;
        }

        public CTree CurrentTree { get; }
        public bool EndOfProcess { get; }
        public string UnParsed { get; }
    }
}