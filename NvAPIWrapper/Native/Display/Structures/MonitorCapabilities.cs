using System;
using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <summary>
    ///     Contains the monitor capabilities read from the Vendor Specific Data Block or the Video Capability Data Block
    /// </summary>
    [StructureVersion(1)]
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct MonitorCapabilities : IInitializable
    {
        [FieldOffset(0)] internal StructureVersion _Version;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        [FieldOffset(4)] private readonly ushort _Size;
        [FieldOffset(8)] private readonly MonitorCapabilitiesType _Type;
        [FieldOffset(12)] private readonly MonitorCapabilitiesConnectorType _ConnectorType;

        [FieldOffset(16)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        private readonly byte[] _Data;

        public MonitorCapabilities(MonitorCapabilitiesType type)
        {
            this = typeof(MonitorCapabilities).Instantiate<MonitorCapabilities>();
            _Size = (ushort) _Version.StructureSize;
            _Type = type;
        }

        /// <summary>
        ///     Gets a boolean value indicating if this instance contains valid information
        /// </summary>
        public bool IsValid
        {
            get => _Data[0].GetBit(0);
        }

        /// <summary>
        ///     Gets the monitor capability type
        /// </summary>
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public MonitorCapabilitiesType Type
        {
            get => _Type;
        }

        /// <summary>
        ///     Gets the monitor connector type
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public MonitorCapabilitiesConnectorType ConnectorType
        {
            get => _ConnectorType;
        }

        /// <summary>
        ///     Gets the monitor VCDB capabilities information
        /// </summary>
        public MonitorVCDBCapabilities VCDBCapabilities
        {
            get
            {
                if (IsValid && _Type == MonitorCapabilitiesType.VCDB)
                {
                    return new MonitorVCDBCapabilities(_Data.Skip(1).ToArray());
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the monitor VSDB capabilities information
        /// </summary>
        public MonitorVSDBCapabilities VSDBCapabilities
        {
            get
            {
                if (IsValid && _Type == MonitorCapabilitiesType.VSDB)
                {
                    return new MonitorVSDBCapabilities(_Data.Skip(1).ToArray());
                }

                return null;
            }
        }

        /// <summary>
        ///     Contains monitor VCDB capabilities
        /// </summary>
        public class MonitorVCDBCapabilities
        {
            private readonly byte[] _data;

            internal MonitorVCDBCapabilities(byte[] data)
            {
                if (data.Length != 49)
                {
                    throw new ArgumentOutOfRangeException(nameof(data));
                }

                _data = data;
            }

            /// <summary>
            ///     Gets a boolean value indicating RGB range quantization
            /// </summary>
            public bool QuantizationRangeRGB
            {
                get => _data[0].GetBit(1);
            }

            /// <summary>
            ///     Gets a boolean value indicating Ycc range quantization
            /// </summary>
            public bool QuantizationRangeYcc
            {
                get => _data[0].GetBit(0);
            }

            public byte ScanInfoConsumerElectronicsVideoFormats
            {
                get => (byte) _data[0].GetBits(6, 2);
            }

            public byte ScanInfoInformationTechnologyVideoFormats
            {
                get => (byte) _data[0].GetBits(4, 2);
            }

            public byte ScanInfoPreferredVideoFormat
            {
                get => (byte) _data[0].GetBits(2, 2);
            }
        }

        /// <summary>
        ///     Contains monitor VSDB capabilities
        /// </summary>
        public class MonitorVSDBCapabilities
        {
            private readonly byte[] _data;

            internal MonitorVSDBCapabilities(byte[] data)
            {
                if (data.Length != 49)
                {
                    throw new ArgumentOutOfRangeException(nameof(data));
                }

                _data = data;
            }

            /// <summary>
            ///     Gets the audio latency if available or null
            /// </summary>
            public byte? AudioLatency
            {
                get
                {
                    if (!_data[4].GetBit(7))
                    {
                        return null;
                    }

                    return _data[6];
                }
            }

            public byte[] HDMI3D
            {
                get
                {
                    if (!_data[9].GetBit(7))
                    {
                        return new byte[0];
                    }

                    return _data.Skip(18).Take(31).Take((int) _data[10].GetBits(0, 5)).ToArray();
                }
            }

            public byte[] HDMIVideoImageCompositors
            {
                get
                {
                    if (!_data[4].GetBit(5))
                    {
                        return new byte[0];
                    }

                    return _data.Skip(11).Take(7).Take((int) _data[10].GetBits(5, 3)).ToArray();
                }
            }

            /// <summary>
            ///     Gets the interlaced audio latency if available or null
            /// </summary>
            public byte? InterlacedAudioLatency
            {
                get
                {
                    if (!_data[4].GetBit(6))
                    {
                        return null;
                    }

                    return _data[8];
                }
            }

            /// <summary>
            ///     Gets the interlaced video latency if available or null
            /// </summary>
            public byte? InterlacedVideoLatency
            {
                get
                {
                    if (!_data[4].GetBit(6))
                    {
                        return null;
                    }

                    return _data[7];
                }
            }

            public bool IsAISupported
            {
                get => _data[2].GetBit(7);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the cinematic content is supported by the monitor or the connection
            /// </summary>
            public bool IsCinemaContentSupported
            {
                get => _data[4].GetBit(2);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the 30bit deep color is supported by the monitor or the connection
            /// </summary>
            public bool IsDeepColor30BitsSupported
            {
                get => _data[2].GetBit(4);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the 36bit deep color is supported by the monitor or the connection
            /// </summary>
            public bool IsDeepColor36BitsSupported
            {
                get => _data[2].GetBit(5);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the 48bit deep color is supported by the monitor or the connection
            /// </summary>
            public bool IsDeepColor48BitsSupported
            {
                get => _data[2].GetBit(6);
            }


            /// <summary>
            ///     Returns a boolean value indicating if the YCbCr444 deep color is supported by the monitor or the connection
            /// </summary>
            public bool IsDeepColorYCbCr444Supported
            {
                get => _data[2].GetBit(3);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the dual DVI operation is supported by the monitor or the connection
            /// </summary>
            public bool IsDualDVIOperationSupported
            {
                get => _data[2].GetBit(0);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the gaming content is supported by the monitor or the connection
            /// </summary>
            public bool IsGameContentSupported
            {
                get => _data[4].GetBit(3);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the graphics text content is supported by the monitor or the connection
            /// </summary>
            public bool IsGraphicsTextContentSupported
            {
                get => _data[4].GetBit(0);
            }

            /// <summary>
            ///     Returns a boolean value indicating if the photo content is supported by monitor or the connection
            /// </summary>
            public bool IsPhotoContentSupported
            {
                get => _data[4].GetBit(1);
            }

            /// <summary>
            ///     Gets the connection max TMDS clock supported by the monitor or the connection
            /// </summary>
            public byte MaxTMDSClock
            {
                get => _data[3];
            }

            /// <summary>
            ///     Gets the monitor physical address on port
            /// </summary>
            public MonitorPhysicalAddress PhysicalAddress
            {
                get => new MonitorPhysicalAddress(
                    (byte) _data[0].GetBits(4, 4),
                    (byte) _data[0].GetBits(0, 4),
                    (byte) _data[1].GetBits(4, 4),
                    (byte) _data[1].GetBits(0, 4)
                );
            }

            /// <summary>
            ///     Gets the video latency if available or null
            /// </summary>
            public byte? VideoLatency
            {
                get
                {
                    if (!_data[4].GetBit(7))
                    {
                        return null;
                    }

                    return _data[5];
                }
            }

            /// <summary>
            ///     Represents a monitor physical address
            /// </summary>
            public class MonitorPhysicalAddress
            {
                internal MonitorPhysicalAddress(byte a, byte b, byte c, byte d)
                {
                    A = a;
                    B = b;
                    C = c;
                    D = d;
                }

                /// <summary>
                ///     Gets the first part of a monitor physical address
                /// </summary>
                public byte A { get; set; }

                /// <summary>
                ///     Gets the second part of a monitor physical address
                /// </summary>
                public byte B { get; set; }


                /// <summary>
                ///     Gets the third part of a monitor physical address
                /// </summary>
                public byte C { get; set; }

                /// <summary>
                ///     Gets the forth part of a monitor physical address
                /// </summary>
                public byte D { get; set; }

                /// <inheritdoc />
                public override string ToString()
                {
                    return $"{A:D}.{B:D}.{C:D}.{D:D}";
                }
            }
        }
    }
}