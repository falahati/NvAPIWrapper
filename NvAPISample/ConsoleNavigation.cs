using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NvAPISample
{
    internal class ConsoleNavigation
    {
        public static void PrintNavigation(Dictionary<object, Action> menuItems, string title, string message)
        {
            PrintObject(menuItems.Keys.ToArray(), index =>
            {
                menuItems[index]();
            }, title, message);
        }

        public static void PrintObject(object obj, string title, string message = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine();
                Console.WriteLine(title);
            }

            WriteObject(obj);
            Console.Write(string.IsNullOrWhiteSpace(message)
                ? "Press enter to go back"
                : $"{message} (Press enter to go back)");
            Console.ReadLine();
        }

        // ReSharper disable once MethodTooLong
        // ReSharper disable once TooManyArguments
        public static void PrintObject<T>(T[] objects, Action<T> action, string title, string message)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine();
                    Console.WriteLine(title);
                }

                WriteObject(objects);
                Console.Write($"{message} (Press enter to go back): ");
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    return;
                }

                if (int.TryParse(userInput, out var pathIndex) &&
                    pathIndex >= objects.GetLowerBound(0) &&
                    pathIndex <= objects.GetUpperBound(0))
                {
                    try
                    {
                        action(objects[pathIndex]);
                    }
                    catch (Exception ex)
                    {
                        WriteException(ex);
                    }
                }
            }
        }

        private static void WriteException(Exception ex)
        {
            Console.WriteLine("{0} - Error: {1}", ex.GetType().Name, ex.Message);
        }

        private static void WriteObject(object obj, int padding = 0)
        {
            try
            {
                if (padding == 0)
                {
                    Console.WriteLine(new string('_', Console.BufferWidth));
                }

                if (obj.GetType().IsValueType || obj is string)
                {
                    Console.WriteLine(new string(' ', padding * 3) + obj);
                }
                else if (obj is IEnumerable)
                {
                    var i = 0;

                    foreach (var arrayItem in (IEnumerable) obj)
                    {
                        Console.WriteLine("[{0}]: {{", i);
                        WriteObject(arrayItem, padding + 1);
                        Console.WriteLine("},");
                        i++;
                    }
                }
                else
                {
                    foreach (var propertyInfo in obj.GetType().GetProperties().OrderBy(info => info.Name))
                    {
                        Console.WriteLine(new string(' ', padding * 3) +
                                          propertyInfo.Name +
                                          ": " +
                                          propertyInfo.GetValue(obj));
                    }
                }

                if (padding == 0)
                {
                    Console.WriteLine(new string('_', Console.BufferWidth));
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
    }
}