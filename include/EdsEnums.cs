using System;

namespace Canon.EDSDK;

public enum EdsDataType : uint
{
    Unknown = 0,
    Bool = 1,
    String = 2,
    Int8 = 3,
    Int16 = 4,
    UInt8 = 6,
    UInt16 = 7,
    Int32 = 8,
    UInt32 = 9,
    Int64 = 10,
    UInt64 = 11,
    Float = 12,
    Double = 13,
    ByteBlock = 14,
    Rational = 20,
    Point = 21,
    Rect = 22,
    Time = 23,
    BoolArray = 30,
    Int8Array = 31,
    Int16Array = 32,
    Int32Array = 33,
    UInt8Array = 34,
    UInt16Array = 35,
    UInt32Array = 36,
    RationalArray = 37,
    FocusInfo = 101,
    PictureStyleDesc = 102,
}

public enum EdsSeekOrigin : uint
{
    Cur = 0,
    Begin,
    End,
}

public enum EdsAccess : uint
{
    Read = 0,
    Write,
    ReadWrite,
    Error = 0xFFFFFFFF,
}

public enum EdsFileCreateDisposition : uint
{
    CreateNew = 0,
    CreateAlways,
    OpenExisting,
    OpenAlways,
    TruncateExisting,
}

public enum EdsTargetImageType : uint
{
    Unknown = 0x00000000,
    Jpeg = 0x00000001,
    TIFF = 0x00000007,
    TIFF16 = 0x00000008,
    RGB = 0x00000009,
    RGB16 = 0x0000000A,
}

public enum EdsImageSource : uint
{
    FullView = 0,
    Thumbnail,
    Preview,
}

public enum EdsProgressOption : uint
{
    NoReport = 0,
    Done,
    Periodically,
}

[Flags]
public enum EdsFileAttribute : uint
{
    Normal = 0x00000000,
    ReadOnly = 0x00000001,
    Hidden = 0x00000002,
    System = 0x00000004,
    Archive = 0x00000020,
}

public enum EdsSaveTo : uint
{
    Camera = 1,
    Host = 2,
    Both = 3,
}

public enum EdsStorageType : uint
{
    Non = 0,
    CF = 1,
    SD = 2,
}

public enum EdsTransferOption : uint
{
    ByDirectTransfer = 1,
    ByRelease = 2,
    ToDesktop = 0x00000100,
}

public enum EdsMirrorLockupState : uint
{
    Disable = 0,
    Enable = 1,
    DuringShooting = 2,
}

public enum EdsMirrorUpSetting : uint
{
    Off = 0,
    On = 1,
}

public enum EdsEvfAf : uint
{
    OFF = 0,
    ON = 1,
}

public enum EdsShutterButton : uint
{
    OFF = 0x00000000,
    Halfway = 0x00000001,
    Completely = 0x00000003,
    Halfway_NonAF = 0x00010001,
    Completely_NonAF = 0x00010003,
}

public enum EdsEvfAFMode : uint
{
    Quick = 0,
    Live = 1,
    LiveFace = 2,
    LiveMulti = 3,
    LiveZone = 4,
    LiveCatchAF = 9,
    LiveSpotAF = 10,
}

public enum EdsStroboMode
{
    Internal = 0,
    ExternalETTL = 1,
    ExternalATTL = 2,
    ExternalTTL = 3,
    ExternalAuto = 4,
    ExternalManual = 5,
    Manual = 6,
}

public enum EdsETTL2Mode
{
    Evaluative = 0,
    Average = 1,
}

public enum EdsDcStrobe : uint
{
    Auto = 0,
    On = 1,
    SlowSynchro = 2,
    Off = 3,
}

public enum EdsDcLensBarrelState : uint
{
    Inner = 0,
    Outer = 1,
}

public enum EdsDcRemoteShootingMode : uint
{
    Stop = 0,
    Start = 1,
}

public enum EdsAEMode : uint
{
    Program = 0x00,
    Tv = 0x01,
    Av = 0x02,
    Manual = 0x03,
    Bulb = 0x04,
    A_DEP = 0x05,
    DEP = 0x06,
    Custom = 0x07,
    Lock = 0x08,
    Green = 0x09,
    NightPortrait = 0x0A,
    Sports = 0x0B,
    Portrait = 0x0C,
    Landscape = 0x0D,
    Closeup = 0x0E,
    FlashOff = 0x0F,
    CreativeAuto = 0x13,
    Movie = 0x14,
    PhotoInMovie = 0x15,
    SceneIntelligentAuto = 0x16,
    HandheldNightScenes = 0x17,
    Hdr_BacklightControl = 0x18,
    SCN = 0x19,
    Children = 0x1A,
    Food = 0x1B,
    CandlelightPortraits = 0x1C,
    CreativeFilter = 0x1D,
    RoughMonoChrome = 0x1E,
    SoftFocus = 0x1F,
    ToyCamera = 0x20,
    Fisheye = 0x21,
    WaterColor = 0x22,
    Miniature = 0x23,
    Hdr_Standard = 0x24,
    Hdr_Vivid = 0x25,
    Hdr_Bold = 0x26,
    Hdr_Embossed = 0x27,
    Movie_Fantasy = 0x28,
    Movie_Old = 0x29,
    Movie_Memory = 0x2A,
    Movie_DirectMono = 0x2B,
    Movie_Mini = 0x2C,
    Panning = 0x2D,
    GroupPhoto = 0x2E,
    SelfPortrait = 0x32,
    PlusMovieAuto = 0x33,
    SmoothSkin = 0x34,
    Panorama = 0x35,
    Silent = 0x36,
    Flexible = 0x37,
    OilPainting = 0x38,
    Fireworks = 0x39,
    StarPortrait = 0x3A,
    StarNightscape = 0x3B,
    StarTrails = 0x3C,
    StarTimelapseMovie = 0x3D,
    BackgroundBlur = 0x3E,
    VideoBlog = 0x3F,
    Unknown = 0xFFFFFFFF,
}

public enum EdsWhiteBalance : int
{
    Click = -1,
    Auto = 0,
    Daylight = 1,
    Cloudy = 2,
    Tungsten = 3,
    Fluorescent = 4,
    Strobe = 5,
    Manual1 = 6,
    Shade = 8,
    ColorTemp = 9,
    PCSet1 = 10,
    PCSet2 = 11,
    PCSet3 = 12,
    Manual2 = 15,
    Manual3 = 16,
    Manual4 = 18,
    Manual5 = 19,
    PCSet4 = 20,
    PCSet5 = 21,
    AwbWhite = 23,
}

public enum EdsColorSpace : uint
{
    sRGB = 1,
    AdobeRGB = 2,
    Unknown = 0xFFFFFFFF,
}

public enum EdsPictureStyle : uint
{
    User1 = 0x0021,
    User2 = 0x0022,
    User3 = 0x0023,
    PC1 = 0x0041,
    PC2 = 0x0042,
    PC3 = 0x0043,
    Standard = 0x0081,
    Portrait = 0x0082,
    Landscape = 0x0083,
    Neutral = 0x0084,
    Faithful = 0x0085,
    Monochrome = 0x0086,
    Auto = 0x0087,
    FineDetail = 0x0088,
}

[Flags]
public enum EdsBracket : uint
{
    AEB = 0x01,
    ISOB = 0x02,
    WBB = 0x04,
    FEB = 0x08,
    Unknown = 0xFFFFFFFF,
}

public enum EdsBatteryLevel : uint
{
    Empty = 1,
    Low = 30,
    Half = 50,
    Normal = 80,
    AC = 0xFFFFFFFF,
}

public enum EdsImageFormat : int
{
    Unknown = 0x00000000,
    Jpeg = 0x00000001,
    CRW = 0x00000002,
    RAW = 0x00000004,
    CR2 = 0x00000006,
    CR2_Jpeg = 0x00000007,
    HEIF = 0x00000008,
}

public enum EdsImageSize : int
{
    Large = 0,
    Middle = 1,
    Small = 2,
    Middle1 = 5,
    Middle2 = 6,
    Small1 = 14,
    Small2 = 15,
    Small3 = 16,
    Unknown = -1,
}

public enum EdsCompressQuality : int
{
    Normal = 2,
    Fine = 3,
    Lossless = 4,
    SuperFine = 5,
    Unknown = -1,
}

[Flags]
public enum EdsEvfOutputDevice : uint
{
    TFT = 0x01,
    PC = 0x02,
    PC_Small = 0x08,
}

public enum EdsEvfZoom : int
{
    Fit = 1,
    x5 = 5,
    x10 = 10,
}

public enum EdsEvfDriveLens : uint
{
    Near1 = 0x00000001,
    Near2 = 0x00000002,
    Near3 = 0x00000003,
    Far1 = 0x00008001,
    Far2 = 0x00008002,
    Far3 = 0x00008003,
}

public enum EdsImageQualityValue : uint
{
    // Jpeg Only
    LJ = 0x0010FF0F,
    MJ = 0x0110FF0F,
    M1J = 0x0510FF0F,
    M2J = 0x0610FF0F,
    SJ = 0x0210FF0F,
    S1J = 0x0E10FF0F,
    S2J = 0x0F10FF0F,
    LJF = 0x0013FF0F,
    LJN = 0x0012FF0F,
    MJF = 0x0113FF0F,
    MJN = 0x0112FF0F,
    SJF = 0x0213FF0F,
    SJN = 0x0212FF0F,
    S1JF = 0x0E13FF0F,
    S1JN = 0x0E12FF0F,
    S2JF = 0x0F13FF0F,
    S3JF = 0x1013FF0F,

    // RAW + Jpeg
    LR = 0x0064FF0F,
    LRLJF = 0x00640013,
    LRLJN = 0x00640012,
    LRMJF = 0x00640113,
    LRMJN = 0x00640112,
    LRSJF = 0x00640213,
    LRSJN = 0x00640212,
    LRS1JF = 0x00640E13,
    LRS1JN = 0x00640E12,
    LRS2JF = 0x00640F13,
    LRS3JF = 0x00641013,
    LRLJ = 0x00640010,
    LRMJ = 0x00640110,
    LRM1J = 0x00640510,
    LRM2J = 0x00640610,
    LRSJ = 0x00640210,
    LRS1J = 0x00640E10,
    LRS2J = 0x00640F10,

    // MRAW(SRAW1) + Jpeg
    MR = 0x0164FF0F,
    MRLJF = 0x01640013,
    MRLJN = 0x01640012,
    MRMJF = 0x01640113,
    MRMJN = 0x01640112,
    MRSJF = 0x01640213,
    MRSJN = 0x01640212,
    MRS1JF = 0x01640E13,
    MRS1JN = 0x01640E12,
    MRS2JF = 0x01640F13,
    MRS3JF = 0x01641013,
    MRLJ = 0x01640010,
    MRM1J = 0x01640510,
    MRM2J = 0x01640610,
    MRSJ = 0x01640210,

    // SRAW(SRAW2) + Jpeg
    SR = 0x0264FF0F,
    SRLJF = 0x02640013,
    SRLJN = 0x02640012,
    SRMJF = 0x02640113,
    SRMJN = 0x02640112,
    SRSJF = 0x02640213,
    SRSJN = 0x02640212,
    SRS1JF = 0x02640E13,
    SRS1JN = 0x02640E12,
    SRS2JF = 0x02640F13,
    SRS3JF = 0x02641013,
    SRLJ = 0x02640010,
    SRM1J = 0x02640510,
    SRM2J = 0x02640610,
    SRSJ = 0x02640210,

    // CRAW + Jpeg
    CR = 0x0063FF0F,
    CRLJF = 0x00630013,
    CRMJF = 0x00630113,
    CRM1JF = 0x00630513,
    CRM2JF = 0x00630613,
    CRSJF = 0x00630213,
    CRS1JF = 0x00630E13,
    CRS2JF = 0x00630F13,
    CRS3JF = 0x00631013,
    CRLJN = 0x00630012,
    CRMJN = 0x00630112,
    CRM1JN = 0x00630512,
    CRM2JN = 0x00630612,
    CRSJN = 0x00630212,
    CRS1JN = 0x00630E12,
    CRLJ = 0x00630010,
    CRMJ = 0x00630110,
    CRM1J = 0x00630510,
    CRM2J = 0x00630610,
    CRSJ = 0x00630210,
    CRS1J = 0x00630E10,
    CRS2J = 0x00630F10,

    // HEIF
    HEIFL = 0x0080FF0F,
    RHEIFL = 0x00640080,
    CRHEIFL = 0x00630080,
    HEIFLF = 0x0083FF0F,
    HEIFLN = 0x0082FF0F,
    HEIFMF = 0x0183FF0F,
    HEIFMN = 0x0182FF0F,
    HEIFS1F = 0x0E83FF0F,
    HEIFS1N = 0x0E82FF0F,
    HEIFS2F = 0x0F83FF0F,
    RHEIFLF = 0x00640083,
    RHEIFLN = 0x00640082,
    RHEIFMF = 0x00640183,
    RHEIFMN = 0x00640182,
    RHEIFS1F = 0x00640E83,
    RHEIFS1N = 0x00640E82,
    RHEIFS2F = 0x00640F83,
    CRHEIFLF = 0x00630083,
    CRHEIFLN = 0x00630082,
    CRHEIFMF = 0x00630183,
    CRHEIFMN = 0x00630182,
    CRHEIFS1F = 0x00630E83,
    CRHEIFS1N = 0x00630E82,
    CRHEIFS2F = 0x00630F83,

    QualityUnknown = 0xFFFFFFFF,
}
