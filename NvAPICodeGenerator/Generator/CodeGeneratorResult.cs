using System.IO;

namespace NvAPICodeGenerator.Generator
{
    internal class CodeGeneratorResult
    {
        /// <inheritdoc />
        public CodeGeneratorResult(FileInfo[] filesAdded, FileInfo[] filesRemoved, DirectoryInfo generatedPath)
        {
            FilesAdded = filesAdded;
            FilesRemoved = filesRemoved;
            GeneratedPath = generatedPath;
        }

        public FileInfo[] FilesRemoved { get; }
        public FileInfo[] FilesAdded { get; }

        public DirectoryInfo GeneratedPath { get; }
    }
}