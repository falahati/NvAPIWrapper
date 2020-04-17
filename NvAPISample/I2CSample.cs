using System;
using System.Linq;
using System.Threading;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.GPU.Structures;

namespace NvAPISample
{
    internal class I2CSample
    {
        private const byte EDIDSlaveAddress = 0x50;
        private const byte DDCCISlaveAddress = 0x37;
        private const byte DDCCIRegisterAddress = 0x51;
        private const byte DDCCIBrightnessIndex = 0x10;
        private const byte DDCCIDataLengthMask = 0x80;
        private const byte DDCCIReadValue = 0x01;
        private const byte DDCCIChangeValue = 0x03;

        public static Tuple<ushort, ushort> I2CGetDDCCIBrightness(GPUOutput output)
        {
            // DDC/CI protocol
            var data = new byte[]
            {
                DDCCIDataLengthMask | 0x02, // 2 byte follows
                DDCCIReadValue,
                DDCCIBrightnessIndex,
                0x00 // Checksum, to be calculated later
            };
            var register = new[] {DDCCIRegisterAddress};

            I2CInfoV3.FillDDCCIChecksum(DDCCISlaveAddress, register, data);

            output.WriteI2C(
                null,
                true,
                DDCCISlaveAddress,
                register,
                data
            );

            Thread.Sleep(100);

            data = output.ReadI2C(
                null,
                true,
                DDCCISlaveAddress,
                new byte[] {0},
                11
            );

            // Check if this is the response to the request that we sent
            if (data[0] != DDCCISlaveAddress << 1)
            {
                return null;
            }

            var length = data[1] & (DDCCIDataLengthMask - 1);

            data = data.Skip(2).Take(length).ToArray();

            // Irrelevant response
            if (data[2] != DDCCIBrightnessIndex)
            {
                return null;
            }

            // Check if this operation is unsupported
            if (data[1] != 0)
            {
                return null;
            }

            var max = (data[4] << 8) | data[5];
            var current = (data[6] << 8) | data[7];

            return new Tuple<ushort, ushort>((ushort) current, (ushort) max);
        }

        public static byte[] I2CReadEDID(GPUOutput output)
        {
            output.WriteI2C(
                null,
                true,
                EDIDSlaveAddress,
                new byte[] {0},
                new byte[] {0}
            );

            Thread.Sleep(100);

            return output.ReadI2C(
                null,
                true,
                EDIDSlaveAddress,
                new byte[] {0},
                128
            );
        }

        public static bool I2CSetDDCCIBrightness(GPUOutput output, ushort brightness)
        {
            var range = I2CGetDDCCIBrightness(output);

            if (range == null)
            {
                return false;
            }

            // Normalize value in range
            brightness = (ushort) Math.Max(Math.Min((int) brightness, range.Item2), 0);

            // Already has same brightness value
            if (range.Item1 == brightness)
            {
                return true;
            }

            Thread.Sleep(100);

            // DDC/CI protocol
            var data = new byte[]
            {
                DDCCIDataLengthMask | 0x04, // 4 byte follows
                DDCCIChangeValue,
                DDCCIBrightnessIndex,
                (byte) (brightness >> 8), // Brightness value (High Byte)
                (byte) (brightness & 0xFF), // Brightness value (Low Byte)
                0x00 // Checksum, to be calculated later
            };
            var register = new[] {DDCCIRegisterAddress};

            I2CInfoV3.FillDDCCIChecksum(DDCCISlaveAddress, register, data);

            output.WriteI2C(
                null,
                true,
                DDCCISlaveAddress,
                register,
                data
            );

            return true;
        }
    }
}