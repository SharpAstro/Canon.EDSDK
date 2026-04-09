using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Canon.EDSDK;

[InlineArray(256)]
public struct InlineBytes256 { private byte _element; }

[InlineArray(32)]
public struct InlineBytes32 { private byte _element; }

[InlineArray(128)]
public struct InlineInt128 { private int _element; }

[InlineArray(1053)]
public struct InlineFocusPoints1053 { private EdsFocusPoint _element; }

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
public struct EdsDeviceInfo
{
    private InlineBytes256 _portName;
    private InlineBytes256 _deviceDescription;
    public uint DeviceSubType;
    private uint _reserved;

    public readonly string PortName => ReadInlineString(_portName);
    public readonly string DeviceDescription => ReadInlineString(_deviceDescription);

    private static string ReadInlineString(InlineBytes256 bytes) =>
        Encoding.ASCII.GetString(((ReadOnlySpan<byte>)bytes).TrimEnd((byte)0));
}

[StructLayout(LayoutKind.Sequential)]
public struct EdsVolumeInfo
{
    public EdsStorageType StorageType;
    public EdsAccess Access;
    public ulong MaxCapacity;
    public ulong FreeSpaceInBytes;
    private InlineBytes256 _volumeLabel;

    public readonly string VolumeLabel =>
        Encoding.ASCII.GetString(((ReadOnlySpan<byte>)_volumeLabel).TrimEnd((byte)0));
}

[StructLayout(LayoutKind.Sequential)]
public struct EdsDirectoryItemInfo
{
    public ulong Size;
    public int IsFolder;
    public uint GroupID;
    public uint Option;
    private InlineBytes256 _fileName;
    public uint Format;
    public uint DateTime;

    public readonly string FileName =>
        Encoding.ASCII.GetString(((ReadOnlySpan<byte>)_fileName).TrimEnd((byte)0));
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
public struct EdsPropertyDesc
{
    public int Form;
    public uint Access;
    public int NumElements;
    public InlineInt128 PropDesc;
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
public struct EdsFocusInfo
{
    public EdsRect ImageRect;
    public uint PointNumber;
    public InlineFocusPoints1053 FocusPoints;
    public uint ExecuteMode;
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
    private InlineBytes32 _caption;
    public byte Data; // variable-length; read via pointer offset

    public readonly string Caption =>
        Encoding.ASCII.GetString(((ReadOnlySpan<byte>)_caption).TrimEnd((byte)0));
}
