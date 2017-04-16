## <img src="NvAPIWrapper/Icon.png" width="42" alt="NvAPIWrapper"> NvAPIWrapper (for NVAPI R375)
NvAPIWrapper is a .Net wrapper for NVIDIA public API, capable of managing all aspects of a display setup using NVIDIA GPUs

## WHERE TO DOWNLOAD
This library is available for download and use through <a href="https://www.nuget.org/packages/NvAPIWrapper">NuGet Gallery</a>.

## WHAT PARTS ARE INCLUDED
NvAPIWrapper is not a compleate wrapper of NVAPI; at least, not yet. Following is the list of NVAPI fetures and their status:

* General: <span style="color: green">Full Support</span>
* Display: <span style="color: green">Full Support</span>
* Display Control: <span style="color: orange">Partial Support</span> (no color control, HUE control, etc)
* GPU: <span style="color: orange">Partial Support</span> (no sensor data, etc)
* D3D: <span style="color: red">No Support</span>
* DRS: <span style="color: red">No Support</span>
* GSync: <span style="color: red">No Support</span>
* Mosaic (Sorround): <span style="color: green">Full Support</span>
* OpenGL: <span style="color: red">No Support</span>
* Stereo (3D): <span style="color: red">No Support</span>
* Vidio: <span style="color: red">No Support</span>

## HOW TO USE
NvAPIWrappr allows you to use the NVAPI functions directly using the `NvAPIWrapper.Native` namespace. However, there is also a .Net friendly implementation of the NVAPI features that can be used to minimize the complexity of the code and make it more compatible with later releases of the library, therefore, we strongly recommend using these .Net friendly classes instead of using the native functions directly.

Currently, you can access different parts of the library as follow:

* Namespace `NvAPIWrapper.Display`: Display and Display Control API
* Namespace `NvAPIWrapper.GPU`: GPU specific API
* Namespace `NvAPIWrapper.Mosaic`: Mosaic API Phase 1 and Phase 2 - Surround
* Class `NvAPIWrapper.NVIDIA`: General Information And Methods

Please also take a look at the `NvAPISample` project for a number of simple examples.

Aside from all this, the library is fully documented and this makes your journey through it as easy as it is possible.

## GNU LESSER GENERAL PUBLIC LICENSE

Version 3, 29 June 2007

Copyright (C) 2007 Free Software Foundation, Inc.
<http://fsf.org/>

Everyone is permitted to copy and distribute verbatim copies of this
license document, but changing it is not allowed.