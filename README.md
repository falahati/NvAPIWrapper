## <img src="NvAPIWrapper/Icon.png" width="24" alt="NvAPIWrapper"> NvAPIWrapper (for NVAPI R375)
NvAPIWrapper is a .Net wrapper for NVIDIA public API, capable of managing all aspects of a display setup using NVIDIA GPUs

## WHERE TO DOWNLOAD
This library is available for download and use through <a href="https://www.nuget.org/packages/NvAPIWrapper.Net">NuGet Gallery</a>.

## WHAT PARTS ARE INCLUDED
NvAPIWrapper is not a complete wrapper of NVAPI; at least, not yet. Following is the list of NVAPI features and their status:

* General: Full Support
* Mosaic (Surround): Full Support
* Display: Full Support
* Display Control: Partial Support (no color control, HUE control, etc)
* GPU: Partial Support (no sensor data, etc)
* D3D: No Support
* DRS: No Support
* GSync: No Support
* OpenGL: No Support
* Stereo (3D): No Support
* Vidio: No Support

## HOW TO USE
NvAPIWrappr allows you to use the NVAPI functions directly using the `NvAPIWrapper.Native` namespace. However, there is also a .Net friendly implementation of the NVAPI features that can be used to minimize the complexity of the code and make it more compatible with later releases of the library, therefore, we strongly recommend using these .Net friendly classes instead of using the native functions directly.

Currently, you can access different parts of the library as follow:

* Namespace `NvAPIWrapper.Display`: Display and Display Control API
* Namespace `NvAPIWrapper.GPU`: GPU specific API
* Namespace `NvAPIWrapper.Mosaic`: Mosaic API Phase 1 and Phase 2 - Surround
* Class `NvAPIWrapper.NVIDIA`: General Information And Methods

Please also take a look at the `NvAPISample` project for a number of simple examples.

Aside from all this, the library is fully documented and this makes your journey through it as easy as it is possible.

## LICENSE
Copyright (C) 2017 Soroush Falahati

Released under the GNU Lesser General Public License ("LGPL")
