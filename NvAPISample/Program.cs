using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleUtilities;
using NvAPIWrapper;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Mosaic;
using NvAPIWrapper.Native;

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
                {"Temp Test", TempTest},
                {"Connected Displays", PrintConnectedDisplays},
                {"Disconnected Displays", PrintDisconnectedDisplays},
                {"Display Configurations", PrintDisplayPathInformation},
                {"Physical GPUs", PrintPhysicalGPUs},
                {"GPU Temperatures", PrintGPUSensors},
                {"GPU Coolers", PrintGPUCoolers},
                {"GPU Performance States", PrintGPUPerformanceStates},
                {"TCC GPUs", PrintTCCGPUs},
                {"Grid Topologies (Mosaic - NVIDIA Surround)", PrintGridTopologies},
                {"NVIDIA Driver and API version", PrintDriverVersion},
                {"System Chipset Information", PrintChipsetInformation},
                {"Lid and Dock Information", PrintDockInformation}
            };

            ConsoleNavigation.Default.PrintNavigation(
                navigation.Keys.ToArray(),
                (i, o) => navigation[o](),
                "Select an execution line to browse NvAPIWrapper functionalists."
            );
        }

        private static void PrintChipsetInformation()
        {
            ConsoleWriter.Default.PrintCaption("NVIDIA.ChipsetInfo");
            ConsoleWriter.Default.WriteObject(new
            {
                NVIDIA.ChipsetInfo.ChipsetName,
                NVIDIA.ChipsetInfo.DeviceId,
                NVIDIA.ChipsetInfo.Flags,
                NVIDIA.ChipsetInfo.VendorId,
                NVIDIA.ChipsetInfo.VendorName
            });
        }

        private static void PrintConnectedDisplays()
        {
            ConsoleWriter.Default.PrintCaption("Display.GetDisplays()");
            ConsoleNavigation.Default.PrintNavigation(Display.GetDisplays(),
                (i, display) => ConsoleWriter.Default.WriteObject(display),
                "Select a display to show device information");
        }

        private static void PrintDisconnectedDisplays()
        {
            ConsoleWriter.Default.PrintCaption("UnAttachedDisplay.GetUnAttachedDisplays()");
            ConsoleNavigation.Default.PrintNavigation(UnAttachedDisplay.GetUnAttachedDisplays(),
                (i, unAttachedDisplay) => ConsoleWriter.Default.WriteObject(unAttachedDisplay, 0),
                "Select a display to show additional information");
        }

        private static void PrintDisplayPathInformation()
        {
            ConsoleWriter.Default.PrintCaption("PathInfo.GetDisplaysConfig()");
            ConsoleNavigation.Default.PrintNavigation(PathInfo.GetDisplaysConfig().ToArray(), (i, info) =>
            {
                ConsoleWriter.Default.WriteObject(info, 2);
            }, "Select a path info to show additional information");
        }

        private static void PrintDockInformation()
        {
            ConsoleWriter.Default.PrintCaption("NVIDIA.LidAndDockParameters");
            ConsoleWriter.Default.WriteObject(new
            {
                NVIDIA.LidAndDockParameters.CurrentDockPolicy,
                NVIDIA.LidAndDockParameters.CurrentDockState,
                NVIDIA.LidAndDockParameters.CurrentLidPolicy,
                NVIDIA.LidAndDockParameters.CurrentLidState,
                NVIDIA.LidAndDockParameters.ForcedDockMechanismPresent,
                NVIDIA.LidAndDockParameters.ForcedLidMechanismPresent
            });
        }

        private static void PrintDriverVersion()
        {
            ConsoleWriter.Default.PrintCaption("NVIDIA");
            ConsoleWriter.Default.WriteObject(new
            {
                NVIDIA.DriverVersion,
                NVIDIA.DriverBranchVersion,
                NVIDIA.InterfaceVersionString
            });
        }

        private static void PrintGPUCoolers()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.PrintCaption("PhysicalGPU.CoolerInformation");
                ConsoleWriter.Default.WriteObject(gpu.CoolerInformation.Coolers.ToArray());
            }, "Select a GPU to show cooler values");
        }

        private static void PrintGPUPerformanceStates()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.PrintCaption("PhysicalGPU.PerformanceStatesInfo");
                ConsoleWriter.Default.WriteObject(gpu.PerformanceStatesInfo, 3);
            }, "Select a GPU to show performance states configuration");
        }

        private static void TempTest()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.PrintCaption("Temp Test");

                // find bits
                var maxBit = 0;
                for (; maxBit < 32; maxBit++)
                {
                    try
                    {
                        GPUApi.QueryThermalSensors(gpu.Handle, 1u << maxBit);
                    }
                    catch
                    {
                        break;
                    }
                }

                if (maxBit == 0)
                {
                    return;
                }

                while (true)
                {
                    try
                    {
                        var temp = GPUApi.QueryThermalSensors(gpu.Handle, (1u << maxBit) - 1);
                        ConsoleWriter.Default.WriteObject(
                            new
                            {
                                GetThermalSettings = gpu.ThermalInformation.ThermalSensors.ToArray(),
                                QueryThermalSensors = temp.Temperatures.Take(maxBit).Select((t) => t.ToString("F2")),
                            }
                        );
                    }
                    catch
                    {
                        // ignore
                    }
                    Thread.Sleep(500);
                }
            }, "Select a GPU to show thermal sensor values");
        }


        private static void PrintGPUSensors()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.PrintCaption("PhysicalGPU.ThermalSensors");
                ConsoleWriter.Default.WriteObject(gpu.ThermalInformation.ThermalSensors.ToArray());
            }, "Select a GPU to show thermal sensor values");
        }

        private static void PrintGridTopologies()
        {
            ConsoleWriter.Default.PrintCaption("GridTopology.GetGridTopologies()");
            ConsoleNavigation.Default.PrintNavigation(GridTopology.GetGridTopologies(), (i, topology) =>
            {
                ConsoleWriter.Default.WriteObject(topology, 3);
            }, "Select a grid topology to show additional information");
        }

        private static void PrintPhysicalGPUs()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.WriteObject(gpu, 0);
            }, "Select a GPU to show additional information");
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once IdentifierTypo
        private static void PrintTCCGPUs()
        {
            ConsoleWriter.Default.PrintCaption("PhysicalGPU.GetTCCPhysicalGPUs()");
            ConsoleNavigation.Default.PrintNavigation(PhysicalGPU.GetTCCPhysicalGPUs(), (i, gpu) =>
            {
                ConsoleWriter.Default.WriteObject(gpu, 0);
            }, "Select a GPU to show additional information");
        }
    }
}