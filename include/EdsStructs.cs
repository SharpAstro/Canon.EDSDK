using System.Runtime.InteropServices;
using System.Text;

namespace Canon.EDSDK;

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsPoint
{
    public readonly int X;
    public readonly int Y;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsRect
{
    public readonly int X;
    public readonly int Y;
    public readonly int Width;
    public readonly int Height;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsSize
{
    public readonly int Width;
    public readonly int Height;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsRational
{
    public readonly int Numerator;
    public readonly uint Denominator;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsTime
{
    public readonly int Year;
    public readonly int Month;
    public readonly int Day;
    public readonly int Hour;
    public readonly int Minute;
    public readonly int Second;
    public readonly int Milliseconds;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsDeviceInfo
{
    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 256)]
    private readonly byte[] _portName;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 256)]
    private readonly byte[] _deviceDescription;

    public readonly uint DeviceSubType;
    private readonly uint _reserved;

    public string PortName => Encoding.ASCII.GetString(_portName).TrimEnd((char)0);
    public string DeviceDescription => Encoding.ASCII.GetString(_deviceDescription).TrimEnd((char)0);
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsVolumeInfo
{
    public readonly EdsStorageType StorageType;
    public readonly EdsAccess Access;
    public readonly ulong MaxCapacity;
    public readonly ulong FreeSpaceInBytes;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 256)]
    private readonly byte[] _volumeLabel;

    public string VolumeLabel => Encoding.ASCII.GetString(_volumeLabel).TrimEnd((char)0);
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsDirectoryItemInfo
{
    public readonly ulong Size;
    public readonly int IsFolder;
    public readonly uint GroupID;
    public readonly uint Option;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 256)]
    private readonly byte[] _fileName;

    public readonly uint Format;
    public readonly uint DateTime;

    public string FileName => Encoding.ASCII.GetString(_fileName).TrimEnd((char)0);
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsImageInfo
{
    public readonly uint Width;
    public readonly uint Height;
    public readonly uint NumOfComponents;
    public readonly uint ComponentDepth;
    public readonly EdsRect EffectiveRect;
    private readonly uint _reserved1;
    private readonly uint _reserved2;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsSaveImageSetting
{
    public readonly uint JPEGQuality;
    private readonly nint _iccProfileStream;
    private readonly uint _reserved;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsPropertyDesc
{
    public readonly int Form;
    public readonly uint Access;
    public readonly int NumElements;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public readonly int[] PropDesc;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsPictureStyleDesc
{
    public readonly int Contrast;
    public readonly uint Sharpness;
    public readonly int Saturation;
    public readonly int ColorTone;
    public readonly uint FilterEffect;
    public readonly uint ToningEffect;
    public readonly uint SharpFineness;
    public readonly uint SharpThreshold;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsFocusPoint
{
    public readonly uint Valid;
    public readonly uint Selected;
    public readonly uint JustFocus;
    public readonly EdsRect Rect;
    private readonly uint _reserved;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsFocusInfo
{
    public readonly EdsRect ImageRect;
    public readonly uint PointNumber;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1053)]
    public readonly EdsFocusPoint[] FocusPoints;

    public readonly uint ExecuteMode;
}

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public readonly struct EdsCapacity
{
    public readonly int NumberOfFreeClusters;
    public readonly int BytesPerSector;
    public readonly int Reset;

    public EdsCapacity(int numberOfFreeClusters, int bytesPerSector, int reset)
    {
        NumberOfFreeClusters = numberOfFreeClusters;
        BytesPerSector = bytesPerSector;
        Reset = reset;
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsCameraPos
{
    public readonly int Status;
    public readonly int Position;
    public readonly int Rolling;
    public readonly int Pitching;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EdsFocusShiftSetting
{
    public readonly uint Version;
    public readonly uint FocusShiftFunction;
    public readonly uint ShootingNumber;
    public readonly uint StepWidth;
    public readonly uint ExposureSmoothing;
}

[StructLayout(LayoutKind.Sequential)]
public struct EdsManualWBData
{
    public uint Valid;
    public uint DataSize;

    [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 32)]
    private readonly byte[] _caption;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public byte[] Data;

    public string Caption => Encoding.ASCII.GetString(_caption).TrimEnd((char)0);
}
