## <img src="NvAPIWrapper/Icon.png" width="24" alt="NvAPIWrapper"> NvAPIWrapper (for NVAPI R410)
[![](https://img.shields.io/github/license/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/commits/master)
[![](https://img.shields.io/github/issues/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/issues)

NvAPIWrapper is a .Net wrapper for NVIDIA public API, capable of managing all aspects of a display setup using NVIDIA GPUs.

This project is licensed under LGPL and therefore can be used in closed source or commercial projects. However, any commit or change to the main code must be public and there should be a read me file along with the DLL clarifying the license and its terms as part of your project as well as a hyperlink to this repository. [Read more about LGPL](https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE).

## How to get
[![](https://img.shields.io/nuget/dt/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)
[![](https://img.shields.io/nuget/v/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)

This library is available for download and use through <a href="https://www.nuget.org/packages/NvAPIWrapper.Net">NuGet Gallery</a>.

## Help me fund my own Death Star

[![](https://img.shields.io/badge/crypto-CoinPayments-8a00a3.svg?style=flat-square)](https://www.coinpayments.net/index.php?cmd=_donate&reset=1&merchant=820707aded07845511b841f9c4c335cd&item_name=Donate&currency=USD&amountf=20.00000000&allow_amount=1&want_shipping=0&allow_extra=1)
[![](https://img.shields.io/badge/shetab-ZarinPal-8a00a3.svg?style=flat-square)](https://zarinp.al/@falahati)
[![](https://img.shields.io/badge/usd-Paypal-8a00a3.svg?style=flat-square)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=ramin.graphix@gmail.com&lc=US&item_name=Donate&no_note=0&cn=&curency_code=USD&bn=PP-DonationsBF:btn_donateCC_LG.gif:NonHosted)

**--OR--**

You can always donate your time by contributing to the project or by introducing it to others.

## What's Supported
NvAPIWrapper is not a complete wrapper of NVAPI; at least, not yet. Following is the list of NVAPI features and their status:

- [X] General: Full Support
  - [X] Chipset Information
  - [X] Driver Information and Driver Restart
  - [X] Lid and Dock Information
        
- [X] Surround: Full Support
  - [X] Topology Configuration (Mosaic Phase 2)
  - [X] Grid Configuration (Mosaic Phase 1)
  - [X] Warping Control
  - [X] Color Intensity Control
        
- [X] Display: Full Support
  - [X] Display Information and Capabilities
  - [X] Path Configuration
  - [X] Custom Resolutions
  - [X] Refresh Rate Override
  - [X] HDMI Support Information and Capabilities
  - [X] DisplayPort Color Capabilities
  - [X] HDR Capabalities
  
- [X] Display Control: Full Support
  - [X] Color Control
  - [X] Saturation Control (Vibrance)
  - [X] HUE Control
  - [X] HDMI InfoFrame Control
  - [X] HDR and HDR Color Control
  - [X] ScanOut Information and Configuration
  - [X] View Control
  - [X] EDID Retrival and Modification
        
- [X] GPU: Full Support
  - [X] GPU Informaion
  - [X] GPU Atchitecture Information
  - [X] GPU Output Information
  - [X] ECC Memory Reporting and Managment
  - [X] PCI-E Information
  - [X] Performance Capabilities and Monitoring
  - [X] Cooler Information (Fan/Liquid/Passive) and Managment (Including RTX+)
  - [X] GPU Illumination Managment
  - [X] Usage Monitoring
  - [X] Power Limit Status and Managment (Modification only via the low level API)
  - [X] Thermal Limit Status and Managment (Modification only via the low level API)
  - [X] Performance State Managment (Modification only via the low level API)
  - [X] Clock Boost and Clock Boost Curve Confiurations (via low level API)
  - [X] Voltage Boost and Voltage Boost Table Configurations (via low level API)
  - [X] Clock Lock Configurations (via low level API)
        
- [X] DRS: Full Support
  - [X] Session, Profile and Application Managment
  - [X] Documented Setting and Managed Setting Values
        
- [X] Stereo (3D): Full Support
  - [X] Stereo Control
  - [X] Stereo Configurations
        
- [ ] D3D: No Support
- [ ] GSync: No Support
- [ ] OpenGL: No Support
- [ ] Vidio: No Support


If a feature you need is missing, feel free to open an [issue](https://github.com/falahati/NvAPIWrapper/issues).

## How to use
NvAPIWrappr allows you to use the NVAPI functions directly (a.k.a. the low-level API) using the `NvAPIWrapper.Native` namespace. However, there is also a .Net friendly implementation of the NVAPI features (a.k.a. the high-level API) that can be used to minimize the complexity of your code and makes it more compatible with later releases of the library, therefore, I strongly recommend using these classes instead of using the native functions directly.

Currently, you can access different parts of the high level API as follow:

* Namespace `NvAPIWrapper.Display`: Display and Display Control API
* Namespace `NvAPIWrapper.GPU`: GPU specific API
* Namespace `NvAPIWrapper.Mosaic`: Mosaic API - Surround
* Namespace `NvAPIWrapper.DRS`: NVIDIA Driver settings and application profiles
* Namespace `NvAPIWrapper.Stereo`: Stereo specific settings and configurations
* Class `NvAPIWrapper.NVIDIA`: General Information And Methods

Please also take a look at the `NvAPISample` project for a number of simple examples.

This library is fully documented and this makes your journey through it as easy as it is possible with NVAPI.

## Related Projects

- [**WindowsDisplayAPI**](https://github.com/falahati/WindowsDisplayAPI/): WindowsDisplayAPI is a .Net wrapper for Windows Display and Windows CCD APIs

- [**EDIDParser**](https://github.com/falahati/EDIDParser/): EDIDParser is a library allowing all .Net developers to parse and to extract information from raw EDID binary data. (Extended Display Identification Data)

- [**HeliosDisplayManagement**](https://github.com/falahati/HeliosDisplayManagement/): An open source display profile management program for Windows with support for NVIDIA Surround

## License
Copyright (C) 2017-2020 Soroush Falahati

This project is licensed under the GNU Lesser General Public License ("LGPL") and therefore can be used in closed source or commercial projects. 
However, any commit or change to the main code must be public and there should be a read me file along with the DLL clarifying the license and its terms as part of your project as well as a hyperlink to this repository. [Read more about LGPL](LICENSE).
