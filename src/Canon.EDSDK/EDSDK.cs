using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Canon.EDSDK;

public static partial class EDSDK
{
    const string EDSDKLib = "EDSDK";

    #region Lifecycle

    [LibraryImport(EDSDKLib, EntryPoint = "EdsInitializeSDK")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsInitializeSDK();

    [LibraryImport(EDSDKLib, EntryPoint = "EdsTerminateSDK")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsTerminateSDK();

    #endregion

    #region Reference counting

    [LibraryImport(EDSDKLib, EntryPoint = "EdsRetain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsRetain(nint inRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsRelease")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsRelease(nint inRef);

    #endregion

    #region Item tree

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetChildCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetChildCount(nint inRef, out int outCount);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetChildAtIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetChildAtIndex(nint inRef, int inIndex, out nint outRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetParent(nint inRef, out nint outParentRef);

    #endregion

    #region Properties

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetPropertySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetPropertySize(nint inRef, EdsPropertyId inPropertyID, int inParam,
        out EdsDataType outDataType, out int outSize);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetPropertyData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam,
        int inPropertySize, nint outPropertyData);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSetPropertyData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam,
        int inPropertySize, nint inPropertyData);

    // Functions with non-blittable struct parameters use DllImport
    [DllImport(EDSDKLib, EntryPoint = "EdsGetPropertyDesc", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsGetPropertyDesc(nint inRef, EdsPropertyId inPropertyID,
        out EdsPropertyDesc outPropertyDesc);

    #endregion

    #region Property data typed overloads

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out uint outData)
    {
        uint value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(uint), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out int outData)
    {
        int value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(int), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsTime outData)
    {
        EdsTime value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsTime), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsPoint outData)
    {
        EdsPoint value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsPoint), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsRect outData)
    {
        EdsRect value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsRect), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsSize outData)
    {
        EdsSize value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsSize), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsCameraPos outData)
    {
        EdsCameraPos value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsCameraPos), (nint)(&value));
        outData = value;
        return err;
    }

    public static unsafe EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out EdsFocusShiftSetting outData)
    {
        EdsFocusShiftSetting value;
        var err = EdsGetPropertyData(inRef, inPropertyID, inParam, sizeof(EdsFocusShiftSetting), (nint)(&value));
        outData = value;
        return err;
    }

    public static EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out string outData)
    {
        nint ptr = Marshal.AllocHGlobal(256);
        try
        {
            var err = EdsGetPropertyData(inRef, inPropertyID, inParam, 256, ptr);
            outData = Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
            return err;
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out int[] outData)
    {
        var err = EdsGetPropertySize(inRef, inPropertyID, 0, out _, out int size);
        if (err is not EdsError.OK)
        {
            outData = [];
            return err;
        }

        nint ptr = Marshal.AllocHGlobal(size);
        try
        {
            err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);
            int len = size / sizeof(int);
            outData = new int[len];
            Marshal.Copy(ptr, outData, 0, len);
            return err;
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static EdsError EdsGetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, out byte[] outData)
    {
        var err = EdsGetPropertySize(inRef, inPropertyID, 0, out _, out int size);
        if (err is not EdsError.OK)
        {
            outData = [];
            return err;
        }

        nint ptr = Marshal.AllocHGlobal(size);
        try
        {
            err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);
            outData = new byte[size];
            Marshal.Copy(ptr, outData, 0, size);
            return err;
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static unsafe EdsError EdsSetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, uint inData)
    {
        return EdsSetPropertyData(inRef, inPropertyID, inParam, sizeof(uint), (nint)(&inData));
    }

    public static unsafe EdsError EdsSetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, int inData)
    {
        return EdsSetPropertyData(inRef, inPropertyID, inParam, sizeof(int), (nint)(&inData));
    }

    public static EdsError EdsSetPropertyData(nint inRef, EdsPropertyId inPropertyID, int inParam, byte[] inData)
    {
        nint ptr = Marshal.AllocHGlobal(inData.Length);
        try
        {
            Marshal.Copy(inData, 0, ptr, inData.Length);
            return EdsSetPropertyData(inRef, inPropertyID, inParam, inData.Length, ptr);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    #endregion

    #region Device list and camera info

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetCameraList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetCameraList(out nint outCameraListRef);

    // Non-blittable: EdsDeviceInfo has MarshalAs byte[] fields
    [DllImport(EDSDKLib, EntryPoint = "EdsGetDeviceInfo", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsGetDeviceInfo(nint inCameraRef, out EdsDeviceInfo outDeviceInfo);

    #endregion

    #region Session

    [LibraryImport(EDSDKLib, EntryPoint = "EdsOpenSession")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsOpenSession(nint inCameraRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCloseSession")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCloseSession(nint inCameraRef);

    #endregion

    #region Camera commands

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSendCommand")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSendCommand(nint inCameraRef, EdsCameraCommand inCommand, int inParam);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSendStatusCommand")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSendStatusCommand(nint inCameraRef, EdsStateCommand inCameraState, int inParam);

    // Non-blittable: EdsCapacity has Pack=2
    [DllImport(EDSDKLib, EntryPoint = "EdsSetCapacity", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetCapacity(nint inCameraRef, EdsCapacity inCapacity);

    #endregion

    #region Volume

    // Non-blittable: EdsVolumeInfo has MarshalAs byte[] fields
    [DllImport(EDSDKLib, EntryPoint = "EdsGetVolumeInfo", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsGetVolumeInfo(nint inVolumeRef, out EdsVolumeInfo outVolumeInfo);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsFormatVolume")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsFormatVolume(nint inVolumeRef);

    #endregion

    #region Directory items

    // Non-blittable: EdsDirectoryItemInfo has MarshalAs byte[] fields
    [DllImport(EDSDKLib, EntryPoint = "EdsGetDirectoryItemInfo", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsGetDirectoryItemInfo(nint inDirItemRef, out EdsDirectoryItemInfo outDirItemInfo);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDeleteDirectoryItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDeleteDirectoryItem(nint inDirItemRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDownload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDownload(nint inDirItemRef, ulong inReadSize, nint outStream);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDownloadCancel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDownloadCancel(nint inDirItemRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDownloadComplete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDownloadComplete(nint inDirItemRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDownloadThumbnail")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDownloadThumbnail(nint inDirItemRef, nint outStream);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetAttribute(nint inDirItemRef, out EdsFileAttribute outFileAttribute);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSetAttribute(nint inDirItemRef, EdsFileAttribute inFileAttribute);

    #endregion

    #region Streams

    // Non-blittable: string parameter
    [DllImport(EDSDKLib, EntryPoint = "EdsCreateFileStream", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsCreateFileStream(
        [MarshalAs(UnmanagedType.LPStr)] string inFileName,
        EdsFileCreateDisposition inCreateDisposition,
        EdsAccess inDesiredAccess,
        out nint outStream);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCreateMemoryStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCreateMemoryStream(ulong inBufferSize, out nint outStream);

    // Non-blittable: string parameter
    [DllImport(EDSDKLib, EntryPoint = "EdsCreateStreamEx", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    public static extern EdsError EdsCreateStreamEx(
        string inFileName,
        EdsFileCreateDisposition inCreateDisposition,
        EdsAccess inDesiredAccess,
        out nint outStream);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCreateMemoryStreamFromPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCreateMemoryStreamFromPointer(nint inUserBuffer, ulong inBufferSize, out nint outStream);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetPointer(nint inStreamRef, out nint outPointer);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsRead")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsRead(nint inStreamRef, ulong inReadSize, nint outBuffer, out ulong outReadSize);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsWrite")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsWrite(nint inStreamRef, ulong inWriteSize, nint inBuffer, out uint outWrittenSize);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSeek")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSeek(nint inStreamRef, long inSeekOffset, EdsSeekOrigin inSeekOrigin);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetPosition(nint inStreamRef, out ulong outPosition);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetLength(nint inStreamRef, out ulong outLength);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCopyData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCopyData(nint inStreamRef, ulong inWriteSize, nint outStreamRef);

    // Non-blittable: delegate parameter
    [DllImport(EDSDKLib, EntryPoint = "EdsSetProgressCallback", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetProgressCallback(nint inRef, EdsProgressCallback inProgressFunc,
        EdsProgressOption inProgressOption, nint inContext);

    #endregion

    #region Image operations

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCreateImageRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCreateImageRef(nint inStreamRef, out nint outImageRef);

    // Non-blittable: EdsImageInfo has nested struct
    [DllImport(EDSDKLib, EntryPoint = "EdsGetImageInfo", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsGetImageInfo(nint inImageRef, EdsImageSource inImageSource, out EdsImageInfo outImageInfo);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsGetImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsGetImage(nint inImageRef, EdsImageSource inImageSource,
        EdsTargetImageType inImageType, EdsRect inSrcRect, EdsSize inDstSize, nint outStreamRef);

    #endregion

    #region Event handlers

    // Non-blittable: delegate parameters
    [DllImport(EDSDKLib, EntryPoint = "EdsSetCameraAddedHandler", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetCameraAddedHandler(EdsCameraAddedHandler inCameraAddedHandler, nint inContext);

    [DllImport(EDSDKLib, EntryPoint = "EdsSetPropertyEventHandler", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetPropertyEventHandler(nint inCameraRef, EdsPropertyEvent inEvent,
        EdsPropertyEventHandler inPropertyEventHandler, nint inContext);

    [DllImport(EDSDKLib, EntryPoint = "EdsSetObjectEventHandler", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetObjectEventHandler(nint inCameraRef, EdsObjectEvent inEvent,
        EdsObjectEventHandler inObjectEventHandler, nint inContext);

    [DllImport(EDSDKLib, EntryPoint = "EdsSetCameraStateEventHandler", CallingConvention = CallingConvention.StdCall)]
    public static extern EdsError EdsSetCameraStateEventHandler(nint inCameraRef, EdsStateEvent inEvent,
        EdsStateEventHandler inStateEventHandler, nint inContext);

    #endregion

    #region Live View (EVF)

    [LibraryImport(EDSDKLib, EntryPoint = "EdsCreateEvfImageRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsCreateEvfImageRef(nint inStreamRef, out nint outEvfImageRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsDownloadEvfImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsDownloadEvfImage(nint inCameraRef, nint outEvfImageRef);

    [LibraryImport(EDSDKLib, EntryPoint = "EdsSetFramePoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    public static partial EdsError EdsSetFramePoint(nint inCameraRef, EdsSize inFramePoint,
        [MarshalAs(UnmanagedType.I1)] bool inLockAfFrame);

    #endregion
}
