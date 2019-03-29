using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NvAPICodeGenerator.Generator;

namespace NvAPICodeGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Initialize SDK path (first argument)
            var sdkDirectory = new DirectoryInfo(args.Length > 0 ? args[0] : Environment.CurrentDirectory);

            if (!sdkDirectory.Exists)
            {
                throw new ArgumentException("Invalid NvAPI SDK path.", nameof(args));
            }

            // Initialize output path (second argument)
            var outputDirectory =
                new DirectoryInfo(args.Length > 1 ? args[1] : Path.Combine(Environment.CurrentDirectory, "Generated"));

            if (!outputDirectory.Exists)
            {
                outputDirectory.Create();
            }

            var removedFiles = new List<FileInfo>();
            var addedFiles = new List<FileInfo>();

            // Translate driver settings header file
            var result = TranslateDriverSettings(sdkDirectory, outputDirectory);
            removedFiles.AddRange(result.FilesRemoved);
            addedFiles.AddRange(result.FilesAdded);


            // Report changes
            foreach (var fileInfo in removedFiles)
            {
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (addedFiles.Any(info => info.FullName.Equals(fileInfo.FullName)))
                {
                    Console.WriteLine("Replaced: {0}", fileInfo.FullName);
                }
                else
                {
                    Console.WriteLine("Removed: {0}", fileInfo.FullName);
                }
            }

            foreach (var fileInfo in addedFiles)
            {
                if (!removedFiles.Any(info => info.FullName.Equals(fileInfo.FullName)))
                {
                    Console.WriteLine("Added: {0}", fileInfo.FullName);
                }
            }

            // Wait for exit
            Console.WriteLine();
            Console.WriteLine("End of Execution, Press Enter to exit.");
            Console.ReadLine();
        }

        private static CodeGeneratorResult TranslateDriverSettings(
            DirectoryInfo sdkDirectory,
            DirectoryInfo outputDirectory)
        {
            var driverSettingsPath = new DirectoryInfo(Path.Combine(outputDirectory.FullName, "DRS"));

            if (!driverSettingsPath.Exists)
            {
                driverSettingsPath.Create();
            }

            var driverSettingsTranslator =
                new DriverSettingsTranslator(sdkDirectory, driverSettingsPath, "NvAPIWrapper.DRS");

            return driverSettingsTranslator.Generate();
        }
    }
}