
<p align="center">
    <img src="https://raw.githubusercontent.com/GasInfinity/Rena.Native/main/assets/logo-native.svg" height="130">
</p>

<h1 align="center"> Rena.Native</h1>

<p align="center">
    <a href="https://dotnet.microsoft.com/"><img alt="Dynamic XML Badge" src="https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fraw.githubusercontent.com%2FGasInfinity%2FRena.Native%2Fmain%2FRena.Native%2FRena.Native.csproj&query=%2F%2FTargetFramework%5B1%5D&logo=dotnet&logoColor=green&label=%20&color=%23222222"></a>
    <a href="https://www.codefactor.io/repository/github/gasinfinity/rena.native"><img src="https://www.codefactor.io/repository/github/gasinfinity/rena.native/badge" alt="CodeFactor" /></a>
    <a href="https://www.nuget.org/packages/Rena.Native/"><img alt="Nuget" src="https://img.shields.io/nuget/v/Rena.Native"></a>
    <a href="https://mit-license.org/"><img alt="GitHub" src="https://img.shields.io/github/license/GasInfinity/Rena.Native"></a>
</p>

> Warning! This library is in development and currently does not adhere to SemVer until we hit v2.0, so, expect breaking changes!

## Features
* Useful unmanaged types for interop like [Pointer{T}](https://github.com/GasInfinity/Rena.Native/blob/main/Rena.Native/Pointer.cs) and [UnmanagedSpan{T}](https://github.com/GasInfinity/Rena.Native/blob/main/Rena.Native/UnmanagedSpan.cs)
* High-performance unsafe extensions
* Safety checks in Debug mode

## TODO's
- [ ] Add a compile-time option instead of relying on Debug mode for the safety checks
- [ ] Finish the API
- [ ] Add documentation to every type and method