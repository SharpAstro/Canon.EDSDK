# Canon.EDSDK

Modern .NET 10 P/Invoke bindings for Canon's EDSDK (13.14.40).

## Features

- **LibraryImport** for blittable methods (source-generated, zero-alloc), **DllImport** for non-blittable
- `nint` handles throughout (not `IntPtr`)
- `EdsError` return type on all methods instead of raw `uint`
- Typed enum params — `EdsPropertyId`, `EdsCameraCommand`, `EdsObjectEvent` etc.
- `readonly struct` with private fields and lazy string decode
- Typed `EdsGetPropertyData` / `EdsSetPropertyData` overloads using `unsafe` stackalloc
- `[Flags]` on `EdsFileAttribute`, `EdsEvfOutputDevice`, `EdsBracket`

## Usage

```csharp
using Canon.EDSDK;

// Initialize SDK
EDSDK.EdsInitializeSDK();

// Get camera list
EDSDK.EdsGetCameraList(out nint cameraList);
EDSDK.EdsGetChildAtIndex(cameraList, 0, out nint camera);

// Open session
EDSDK.EdsOpenSession(camera);

// Set property
EDSDK.EdsSetPropertyData(camera, EdsPropertyId.ISOSpeed, 0, 0x00000068); // ISO 800

// Take picture
EDSDK.EdsSendCommand(camera, EdsCameraCommand.TakePicture, 0);
```

## Requirements

Canon's EDSDK binary (`EDSDK.dll`) is **not included** — it must be obtained from [Canon's Developer Community](https://developercommunity.usa.canon.com/). Place it in `lib/win-x64/`.

## License

MIT
