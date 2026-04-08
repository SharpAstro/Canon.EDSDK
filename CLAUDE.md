# Canon.EDSDK — Developer Guide

## Build

```
dotnet build
```

Requires .NET 10 SDK. The Canon EDSDK DLL (`EDSDK.dll`) is NOT required to compile — only to run. The `lib/win-x64/` slot is conditionally packed via `Condition="Exists(...)"`.

## Design

Single `static partial class EDSDK` in `include/EDSDK.cs` with all P/Invoke methods. Supporting types split across files by concern.

### Interop strategy

- **`LibraryImport`** (source-generated) for methods with only blittable params: lifecycle, ref counting, session, commands, streams, live view. Uses `CallConvStdcall` (EDSDK default on Windows).
- **`DllImport`** (runtime marshalling) for methods with non-blittable params: `EdsDeviceInfo`, `EdsVolumeInfo`, `EdsDirectoryItemInfo` (structs with `[MarshalAs]` byte array fields), `EdsPropertyDesc` (inline int[128]), string params, delegate params.

### Property get/set

The raw P/Invoke `EdsGetPropertyData`/`EdsSetPropertyData` take `nint` data pointers. Typed overloads use `unsafe` pointer arithmetic — no `Marshal.AllocHGlobal`/`Marshal.PtrToStructure`:

```csharp
public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId id, int param, out uint outData)
{
    uint value;
    var err = EdsGetPropertyData(inRef, id, param, sizeof(uint), (nint)(&value));
    outData = value;
    return err;
}
```

For variable-size data (`string`, `int[]`, `byte[]`), `Marshal.AllocHGlobal` is used with `try/finally` to prevent leaks on exceptions (NINA's original code didn't have this).

### Structs

Follow the sharpastro pattern: `readonly struct` with `[StructLayout(LayoutKind.Sequential)]`, private `byte[]` fields with `[MarshalAs(UnmanagedType.ByValArray)]`, public `string` properties via `Encoding.ASCII.GetString(...).TrimEnd((char)0)`.

### Files

| File | Contents |
|------|----------|
| `include/EDSDK.cs` | All P/Invoke methods + typed property overloads |
| `include/EdsError.cs` | `EdsError : uint` (~120 error codes) |
| `include/EdsEnums.cs` | Value enums: AEMode, WhiteBalance, ImageQuality, ShutterButton, etc. |
| `include/EdsPropertyId.cs` | `EdsPropertyId : uint` (~80 property IDs) |
| `include/EdsCommands.cs` | `EdsCameraCommand`, `EdsStateCommand`, event ID enums |
| `include/EdsStructs.cs` | All structs (EdsDeviceInfo, EdsCapacity, EdsFocusInfo, etc.) |
| `include/EdsCallbacks.cs` | 5 delegate types returning `EdsError` with typed enum params |

### Origin

The original EDSDK C# binding was ported from Johannes Bildstein's CodeProject article (2013, LGPL 3.0). NINA's version is the most complete public fork, tracking EDSDK 13.14.40. This package modernizes it for .NET 10 with LibraryImport, typed enums, and readonly structs.

## Testing

Requires Canon's `EDSDK.dll` in the DLL search path. No automated tests — the library wraps a vendor binary that needs a physical camera.
