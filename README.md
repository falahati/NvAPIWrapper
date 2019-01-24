## <img src="NvAPIWrapper/Icon.png" width="24" alt="NvAPIWrapper"> NvAPIWrapper (for NVAPI R375)
[![](https://img.shields.io/github/license/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/commits/master)
[![](https://img.shields.io/github/issues/falahati/NvAPIWrapper.svg?style=flat-square)](https://github.com/falahati/NvAPIWrapper/issues)

NvAPIWrapper is a .Net wrapper for NVIDIA public API, capable of managing all aspects of a display setup using NVIDIA GPUs.

This project is licensed under LGPL and therefore can be used in closed source or commercial projects. However any commit or change to the main code must be public and there should be a read me file along with the DLL clearigying this as part of your project. [Read more about LGPL](https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE).

## WHERE TO DOWNLOAD
[![](https://img.shields.io/nuget/dt/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)
[![](https://img.shields.io/nuget/v/NvAPIWrapper.Net.svg?style=flat-square)](https://www.nuget.org/packages/NvAPIWrapper.Net)

This library is available for download and use through <a href="https://www.nuget.org/packages/NvAPIWrapper.Net">NuGet Gallery</a>.

## Donation
Donations assist development and are greatly appreciated; also always remember that [every coffee counts!](https://media.makeameme.org/created/one-simply-does-i9k8kx.jpg) :)

[![](https://img.shields.io/badge/fiat-PayPal-8a00a3.svg?style=flat-square)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=WR3KK2B6TYYQ4&item_name=Donation&currency_code=USD&source=url)
[![](https://img.shields.io/badge/crypto-CoinPayments-8a00a3.svg?style=flat-square)](https://www.coinpayments.net/index.php?cmd=_donate&reset=1&merchant=820707aded07845511b841f9c4c335cd&item_name=Donate&currency=USD&amountf=20.00000000&allow_amount=1&want_shipping=0&allow_extra=1)
[![](https://img.shields.io/badge/shetab-ZarinPal-8a00a3.svg?style=flat-square)](https://zarinp.al/@falahati)

**--OR--**

You can always donate your time by contributing to the project or by introducing it to others.

## WHAT PARTS ARE INCLUDED
NvAPIWrapper is not a complete wrapper of NVAPI; at least, not yet. Following is the list of NVAPI features and their status:

* General: Full Support
* Mosaic (Surround): Full Support
* Display: Full Support
* Display Control: Partial Support (no color control, HUE control, etc)
* GPU: Partial Support (incomplete Sensor, incomplete PStates, incomplete clock frequency, etc)
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
