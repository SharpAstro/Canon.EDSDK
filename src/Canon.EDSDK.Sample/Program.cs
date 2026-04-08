using System.Runtime.InteropServices;
using Canon.EDSDK;

Console.WriteLine("Canon.EDSDK — Property Read Test");

var err = EDSDK.EdsInitializeSDK();
Console.WriteLine($"Init: {err}");
if (err != EdsError.OK) return;

try
{
    err = EDSDK.EdsGetCameraList(out var cameraList);
    EDSDK.EdsGetChildCount(cameraList, out int count);
    Console.WriteLine($"Cameras: {count}");
    if (count == 0) return;

    EDSDK.EdsGetChildAtIndex(cameraList, 0, out var cam);

    // Get device info before opening session
    EDSDK.EdsGetDeviceInfo(cam, out var deviceInfo);
    Console.WriteLine($"Device: {deviceInfo.DeviceDescription}");
    Console.WriteLine($"Port:   {deviceInfo.PortName}");

    err = EDSDK.EdsOpenSession(cam);
    Console.WriteLine($"OpenSession: {err}");
    if (err != EdsError.OK) return;

    try
    {
        // Pump Win32 messages — EDSDK uses a hidden window for internal event dispatch.
        // Without this, EdsGetPropertyData may return stale/zero values.
        Console.Write("Pumping messages");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < 2000)
        {
            while (PeekMessage(out var msg, 0, 0, 0, 1))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
            Thread.Sleep(50);
            Console.Write(".");
        }
        Console.WriteLine(" done\n");

        // --- Read properties ---
        Console.WriteLine("--- Camera Properties ---");

        ReadString(cam, EdsPropertyId.ProductName, "Product");
        ReadString(cam, EdsPropertyId.FirmwareVersion, "Firmware");
        ReadString(cam, EdsPropertyId.BodyIDEx, "BodyID");
        ReadString(cam, EdsPropertyId.OwnerName, "Owner");

        ReadUInt32(cam, EdsPropertyId.BatteryLevel, "Battery");
        ReadUInt32(cam, EdsPropertyId.ISOSpeed, "ISO");
        ReadUInt32(cam, EdsPropertyId.Tv, "Tv (shutter)");
        ReadUInt32(cam, EdsPropertyId.Av, "Av (aperture)");
        ReadUInt32(cam, EdsPropertyId.AEMode, "AE Mode");
        ReadUInt32(cam, EdsPropertyId.DriveMode, "Drive Mode");
        ReadUInt32(cam, EdsPropertyId.WhiteBalance, "White Balance");
        ReadUInt32(cam, EdsPropertyId.ImageQuality, "Image Quality");
        ReadUInt32(cam, EdsPropertyId.MeteringMode, "Metering Mode");
        ReadUInt32(cam, EdsPropertyId.ExposureCompensation, "Exp Comp");
        ReadUInt32(cam, EdsPropertyId.AvailableShots, "Avail Shots");
        ReadUInt32(cam, EdsPropertyId.ColorSpace, "Color Space");
        ReadUInt32(cam, EdsPropertyId.SaveTo, "Save To");
        ReadUInt32(cam, EdsPropertyId.MirrorUpSetting, "Mirror Lock");

        // ISO options
        Console.WriteLine("\n--- ISO options ---");
        err = EDSDK.EdsGetPropertyDesc(cam, EdsPropertyId.ISOSpeed, out var isoDesc);
        if (err == EdsError.OK)
        {
            Console.Write("  Available: ");
            for (int i = 0; i < isoDesc.NumElements; i++)
                Console.Write($"{isoDesc.PropDesc[i]} ");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"  EdsGetPropertyDesc: {err}");
        }
    }
    finally
    {
        EDSDK.EdsCloseSession(cam);
        EDSDK.EdsRelease(cam);
    }
    EDSDK.EdsRelease(cameraList);
}
finally
{
    EDSDK.EdsTerminateSDK();
}

Console.WriteLine("\nDone.");

static void ReadUInt32(nint cam, EdsPropertyId prop, string label)
{
    var err = EDSDK.EdsGetPropertyData(cam, prop, 0, out uint value);
    Console.WriteLine(err == EdsError.OK
        ? $"  {label,-18}: {value} (0x{value:X})"
        : $"  {label,-18}: {err}");
}

static void ReadString(nint cam, EdsPropertyId prop, string label)
{
    var err = EDSDK.EdsGetPropertyData(cam, prop, 0, out string value);
    Console.WriteLine(err == EdsError.OK
        ? $"  {label,-18}: \"{value}\""
        : $"  {label,-18}: {err}");
}

[DllImport("user32.dll")]
static extern bool PeekMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);
[DllImport("user32.dll")]
static extern bool TranslateMessage(ref MSG lpMsg);
[DllImport("user32.dll")]
static extern nint DispatchMessage(ref MSG lpMsg);

[StructLayout(LayoutKind.Sequential)]
struct MSG { public nint hwnd; public uint message; public nuint wParam; public nint lParam; public uint time; public int ptX, ptY; }
