using System.Collections.Generic;
using System.IO;
using NvAPICodeGenerator.Parser.Models;
using NvAPICodeGenerator.Parser.Processors;

namespace NvAPICodeGenerator.Parser
{
    internal class CHeaderFileParser
    {
        public CHeaderFileParser(FileInfo fileInfo, CProcessorBase[] processors)
        {
            File = fileInfo;
            Root = null;
            Processors = processors;
        }

        public CHeaderFileParser(FileInfo fileInfo) : this(fileInfo, DefaultProcessors.ToArray())
        {
        }

        public static List<CProcessorBase> DefaultProcessors { get; } = new List<CProcessorBase>(
            new CProcessorBase[]
            {
                new CDefineProcessor(),
                new CEnumProcessor()
            }
        );

        public FileInfo File { get; }

        public CProcessorBase[] Processors { get; }

        public CTree Root { get; private set; }

        public void Parse()
        {
            Root = new CTree(null, null);
            var parentTree = Root;
            var globalValues = new CProcessorStateValues();
            CProcessorState state = null;
            CProcessorBase currentProcessor = null;

            using (var stream = File.OpenText())
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();

                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        if (currentProcessor == null)
                        {
                            foreach (var cProcessor in Processors)
                            {
                                if (cProcessor.CanParse(line))
                                {
                                    state = new CProcessorState(parentTree, this, globalValues);
                                    currentProcessor = cProcessor;

                                    break;
                                }
                            }
                        }

                        if (currentProcessor != null)
                        {
                            var result = currentProcessor.Parse(line, state);

                            state.CurrentTree = result.CurrentTree;

                            if (result.EndOfProcess)
                            {
                                state = null;
                                currentProcessor = null;
                            }

                            parentTree = result.CurrentTree;
                            line = result.UnParsed;
                        }
                        else
                        {
                            // Skip Line
                            break;
                        }
                    }
                }
            }
        }
    }
}