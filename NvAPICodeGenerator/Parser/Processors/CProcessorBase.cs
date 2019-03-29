namespace NvAPICodeGenerator.Parser.Processors
{
    internal abstract class CProcessorBase
    {
        public abstract bool CanParse(string str);

        public abstract CProcessorParseResult Parse(string str, CProcessorState state);
    }
}