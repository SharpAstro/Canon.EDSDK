namespace Canon.EDSDK;

public delegate EdsError EdsProgressCallback(uint inPercent, nint inContext, ref bool outCancel);
public delegate EdsError EdsCameraAddedHandler(nint inContext);
public delegate EdsError EdsPropertyEventHandler(EdsPropertyEvent inEvent, EdsPropertyId inPropertyID, uint inParam, nint inContext);
public delegate EdsError EdsObjectEventHandler(EdsObjectEvent inEvent, nint inRef, nint inContext);
public delegate EdsError EdsStateEventHandler(EdsStateEvent inEvent, uint inParameter, nint inContext);
