using System;
using System.Collections.Generic;
using System.Linq;
using NvAPIWrapper;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Mosaic;

namespace NvAPISample
{
    internal class Program
    {
        // ReSharper disable once TooManyDeclarations
        private static void Main()
        {
            NVIDIA.Initialize();
            var navigation = new Dictionary<object, Action>
            {
                {
                    "Connected Displays",
                    () =>
                        ConsoleNavigation.PrintObject(Display.GetDisplays(),
                            display => ConsoleNavigation.PrintObject(display.DisplayDevice, "Display.DisplayDevice"),
                            "Display.GetDisplays()", "Select a display to show device information")
                },
                {
                    "Disconnected Displays",
                    () =>
                        ConsoleNavigation.PrintObject(UnAttachedDisplay.GetUnAttachedDisplays(),
                            display =>
                                ConsoleNavigation.PrintObject(display.PhysicalGPU, "UnAttachedDisplay.PhysicalGPU"),
                            "UnAttachedDisplay.GetUnAttachedDisplays()", "Select a display to show GPU information")
                },
                {
                    "Display Configurations",
                    () =>
                        ConsoleNavigation.PrintObject(PathInfo.GetDisplaysConfig().ToArray(),
                            pathInfo =>
                                ConsoleNavigation.PrintObject(pathInfo.TargetsInfo,
                                    targetInfo =>
                                        ConsoleNavigation.PrintObject(targetInfo.DisplayDevice,
                                            "PathTargetInfo.DisplayDevice"), "PathInfo.TargetsInfo[]",
                                    "Select a path target info to show display device information"),
                            "PathInfo.GetDisplaysConfig()", "Select a path info to show target information")
                },
                {
                    "Physical GPUs", () =>
                        ConsoleNavigation.PrintObject(PhysicalGPU.GetPhysicalGPUs(),
                            gpu => ConsoleNavigation.PrintObject(gpu.ActiveOutputs, "PhysicalGPU.ActiveOutputs"),
                            "PhysicalGPU.GetPhysicalGPUs()", "Select a GPU to show active outputs")
                },
                {
                    "GPU Temperatures", () =>
                        ConsoleNavigation.PrintNavigation(
                            PhysicalGPU.GetPhysicalGPUs()
                                .ToDictionary(gpu => (object) gpu.ToString(), gpu => new Action(
                                    () =>
                                    {
                                        ConsoleNavigation.PrintObject(gpu.ThermalSensors, "PhysicalGPU.ThermalSensors");
                                    })),
                            "PhysicalGPU.GetPhysicalGPUs()", "Select a GPU to show thermal sensor values")
                },
                {
                    "GPU Dynamic Performance States", () =>
                        ConsoleNavigation.PrintNavigation(
                            PhysicalGPU.GetPhysicalGPUs()
                                .ToDictionary(gpu => (object) gpu.ToString(), gpu => new Action(
                                    () =>
                                    {
                                        ConsoleNavigation.PrintObject(gpu.DynamicPerformanceStatesInfo,
                                            "PhysicalGPU.DynamicPerformanceStatesInfo");
                                    })),
                            "PhysicalGPU.GetPhysicalGPUs()", "Select a GPU to show dynamic performance state domains")
                },
                {
                    "GPU Clock Frequencies", () =>
                        ConsoleNavigation.PrintNavigation(
                            PhysicalGPU.GetPhysicalGPUs()
                                .ToDictionary(gpu => (object) gpu.ToString(), gpu => new Action(
                                    () =>
                                    {
                                        ConsoleNavigation.PrintObject(new
                                            {
                                                CurrentClock = gpu.CurrentClockFrequencies,
                                                BaseClock = gpu.BaseClockFrequencies,
                                                BoostClock = gpu.BoostClockFrequencies
                                            },
                                            "PhysicalGPU.CurrentClockFrequencies, PhysicalGPU.BaseClockFrequencies, PhysicalGPU.BoostClockFrequencies");
                                    })),
                            "PhysicalGPU.GetPhysicalGPUs()", "Select a GPU to show clock frequencies")
                },
                {
                    "TCC GPUs", () =>
                        ConsoleNavigation.PrintObject(PhysicalGPU.GetTCCPhysicalGPUs(),
                            "PhysicalGPU.GetTCCPhysicalGPUs()")
                },
                {
                    "Grid Topologies (Mosaic)",
                    () =>
                        ConsoleNavigation.PrintObject(GridTopology.GetGridTopologies(),
                            grid =>
                                ConsoleNavigation.PrintObject(grid.Displays,
                                    display =>
                                        ConsoleNavigation.PrintObject(display.DisplayDevice,
                                            "GridTopologyDisplay.DisplayDevice"), "GridTopology.Displays",
                                    "Select a grid topology display to show display device information"),
                            "GridTopology.GetGridTopologies()", "Select a grid topology to show display information")
                },
                {
                    "NVIDIA Driver and API version", () => ConsoleNavigation.PrintObject(new object[]
                    {
                        "Driver Version: " + NVIDIA.DriverVersion,
                        "Driver Branch Version: " + NVIDIA.DriverBranchVersion,
                        "NvAPI Version: " + NVIDIA.InterfaceVersionString
                    }, "NVIDIA")
                },
                {"System Chipset Info", () => ConsoleNavigation.PrintObject(NVIDIA.ChipsetInfo, "NVIDIA.ChipsetInfo")},
                {
                    "Lid and Dock Information",
                    () => ConsoleNavigation.PrintObject(NVIDIA.LidAndDockParameters, "NVIDIA.LidAndDockParameters")
                }
            };
            ConsoleNavigation.PrintNavigation(navigation, "Execution Lines",
                "Select an execution line to browse NvAPIWrapper functionalities.");
        }
    }
}