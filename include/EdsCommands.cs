namespace Canon.EDSDK;

public enum EdsCameraCommand : uint
{
    TakePicture = 0x00000000,
    ExtendShutDownTimer = 0x00000001,
    BulbStart = 0x00000002,
    BulbEnd = 0x00000003,
    PressShutterButton = 0x00000004,
    DoEvfAf = 0x00000102,
    DriveLensEvf = 0x00000103,
    DoClickWBEvf = 0x00000104,
    MovieSelectSwON = 0x00000107,
    MovieSelectSwOFF = 0x00000108,
    RequestRollPitchLevel = 0x00000109,
    SetRemoteShootingMode = 0x0000010F,
    RequestSensorCleaning = 0x00000112,
}

public enum EdsStateCommand : uint
{
    UILock = 0x00000000,
    UIUnLock = 0x00000001,
    EnterDirectTransfer = 0x00000002,
    ExitDirectTransfer = 0x00000003,
}

public enum EdsPropertyEvent : uint
{
    All = 0x00000100,
    PropertyChanged = 0x00000101,
    PropertyDescChanged = 0x00000102,
}

public enum EdsObjectEvent : uint
{
    All = 0x00000200,
    VolumeInfoChanged = 0x00000201,
    VolumeUpdateItems = 0x00000202,
    FolderUpdateItems = 0x00000203,
    DirItemCreated = 0x00000204,
    DirItemRemoved = 0x00000205,
    DirItemInfoChanged = 0x00000206,
    DirItemContentChanged = 0x00000207,
    DirItemRequestTransfer = 0x00000208,
    DirItemRequestTransferDT = 0x00000209,
    DirItemCancelTransferDT = 0x0000020A,
    VolumeAdded = 0x0000020C,
    VolumeRemoved = 0x0000020D,
}

public enum EdsStateEvent : uint
{
    All = 0x00000300,
    Shutdown = 0x00000301,
    JobStatusChanged = 0x00000302,
    WillSoonShutDown = 0x00000303,
    ShutDownTimerUpdate = 0x00000304,
    CaptureError = 0x00000305,
    InternalError = 0x00000306,
    AfResult = 0x00000309,
}
