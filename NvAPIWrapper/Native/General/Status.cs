namespace NvAPIWrapper.Native.General
{
    public enum Status
    {
        // Success
        Ok = 0,

        // Generic error
        Error = -1,

        // nvapi.dll can not be loaded
        LibraryNotFound = -2,

        // not implemented in current driver installation
        NoImplementation = -3,

        // NvAPI_Initialize has not been called (successfully)
        ApiNotIntialized = -4,

        // invalid argument
        InvalidArgument = -5,

        // no NVIDIA display driver was found
        NvidiaDeviceNotFound = -6,

        // no more to enumerate
        EndEnumeration = -7,

        // invalid handle
        InvalidHandle = -8,

        // an argument's structure version is not supported
        IncompatibleStructVersion = -9,

        // handle is no longer valid (likely due to GPU or display re-configuration)
        HandleInvalidated = -10,

        // no NVIDIA OpenGL context is current (but needs to be)
        OpenglContextNotCurrent = -11,

        // An invalid pointer, usually NULL, was passed as a parameter
        InvalidPointer = -14,

        // OpenGL Expert is not supported by the current drivers
        NoGLExpert = -12,


        // OpenGL Expert is supported, but driver instrumentation is currently disabled
        InstrumentationDisabled = -13,

        // expected a logical GPU handle for one or more parameters
        ExpectedLogicalGPUHandle = -100,

        // expected a physical GPU handle for one or more parameters
        ExpectedPhysicalGPUHandle = -101,

        // expected an NV display handle for one or more parameters
        ExpectedDisplayHandle = -102,

        // used in some commands to indicate that the combination of parameters is not valid
        InvalidCombination = -103,

        // Requested feature not supported in the selected GPU
        NotSupported = -104,

        // NO port ID found for I2C transaction
        PortIdNotFound = -105,

        // expected an unattached display handle as one of the input param
        ExpectedUnattachedDisplayHandle = -106,

        // invalid perf level
        InvalidPerfLevel = -107,

        // device is busy, request not fulfilled
        DeviceBusy = -108,

        // NV persist file is not found
        NvPersistFileNotFound = -109,

        // NV persist data is not found
        PersistDataNotFound = -110,

        // expected TV output display
        ExpectedTVDisplay = -111,

        // expected TV output on D Connector - HDTV_EIAJ4120.
        ExpectedTVDisplayOnDConnector = -112,

        // SLI is not active on this device
        NoActiveSLITopology = -113,

        // setup of SLI rendering mode is not possible right now
        SLIRenderingModeNotallowed = -114,

        // expected digital flat panel
        ExpectedDigitalFlatPanel = -115,

        // argument exceeds expected size
        ArgumentExceedMaxSize = -116,

        // inhibit ON due to one of the flags in NV_GPU_DISPLAY_CHANGE_INHIBIT or SLI Active
        DeviceSwitchingNotAllowed = -117,

        // testing clocks not supported
        TestingClocksNotSupported = -118,

        // the specified underscan config is from an unknown source (e.g. INF)
        UnknownUnderscanConfig = -119,

        // timeout while reconfiguring GPUs
        TimeoutReconfiguringGPUTopo = -120,

        // Requested data was not found
        DataNotFound = -121,

        // expected analog display
        ExpectedAnalogDisplay = -122,

        // No SLI video bridge present
        NoVidlink = -123,

        // NVAPI requires reboot for its settings to take effect
        RequiresReboot = -124,

        // the function is not supported with the current hybrid mode.
        InvalidHybridMode = -125,

        // The target types are not all the same
        MixedTargetTypes = -126,

        // the function is not supported from 32-bit on a 64-bit system
        SYSWOW64NotSupported = -127,

        //there is any implicit GPU topology active. Use NVAPI_SetHybridMode to change topology.
        ImplicitSetGPUTopologyChangeNotAllowed = -128,


        //Prompt the user to close all non-migratable applications.
        RequestUserToCloseNonMigratableApps = -129,

        // Could not allocate sufficient memory to complete the call
        OutOfMemory = -130,

        // The previous operation that is transferring information to or from this surface is incomplete
        WasStillDrawing = -131,

        // The file was not found
        FileNotFound = -132,

        // There are too many unique instances of a particular type of state object
        TooManyUniqueStateObjects = -133,


        // The method call is invalid. For example, a method's parameter may not be a valid pointer
        InvalidCall = -134,

        // d3d10_1.dll can not be loaded
        D3D101LibraryNotFound = -135,

        // Couldn't find the function in loaded DLL library
        FunctionNotFound = -136,

        // Current User is not Administrator
        InvalidUserPrivilege = -137,

        // The handle corresponds to GDIPrimary
        ExpectedNonPrimaryDisplayHandle = -138,

        // Setting Physx GPU requires that the GPU is compute capable
        ExpectedComputeGPUHandle = -139,

        // Stereo part of NVAPI failed to initialize completely. Check if stereo driver is installed.
        StereoNotInitialized = -140,

        // Access to stereo related registry keys or values failed.
        StereoRegistryAccessFailed = -141,

        // Given registry profile type is not supported.
        StereoRegistryProfileTypeNotSupported = -142,

        // Given registry value is not supported.
        StereoRegistryValueNotSupported = -143,

        // Stereo is not enabled and function needed it to execute completely.
        StereoNotEnabled = -144,

        // Stereo is not turned on and function needed it to execute completely.
        StereoNotTurnedOn = -145,

        // Invalid device interface.
        StereoInvalidDeviceInterface = -146,


        // Separation percentage or JPEG image capture quality out of [0-100] range.
        StereoParameterOutOfRange = -147,

        // Given frustum adjust mode is not supported.
        StereoFrustumAdjustModeNotSupported = -148,

        // The mosaic topology is not possible given the current state of HW
        TopologyNotPossible = -149,

        // An attempt to do a display resolution mode change has failed
        ModeChangeFailed = -150,

        // d3d11.dll/d3d11_beta.dll cannot be loaded.
        D3D11LibraryNotFound = -151,

        // Address outside of valid range.
        InvalidAddress = -152,

        // The input does not match any of the available devices
        MatchingDeviceNotFound = -153
    }
}