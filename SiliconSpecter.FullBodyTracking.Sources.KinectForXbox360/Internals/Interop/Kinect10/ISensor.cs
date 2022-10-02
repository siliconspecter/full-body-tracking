using SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10.CoordinateMapping;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// An instance of a Kinect sensor.
/// </summary>
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("D3D9AB7B-31BA-44CA-8CC0-D42525BBEA43"), ComImport]
public interface ISensor
{
    /// <summary>
    /// Initializes <see langword="this"/> <see cref="ISensor"/>.
    /// </summary>
    /// <remarks>This method should only be called once per <see cref="ISensor"/>.</remarks>
    /// <param name="flags">The <see cref="InitializeFlags"/> to use when initializing <see langword="this"/> <see cref="ISensor"/>.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiInitialize(InitializeFlags flags);

    /// <summary>
    /// Shuts down <see langword="this" /> <see cref="ISensor" />.
    /// </summary>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void NuiShutdown();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="event">Unused.</param>
    /// <param name="flags">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiSetFrameEndEvent(IntPtr @event, uint flags);

    /// <summary>
    /// Opens an image stream.
    /// </summary>
    /// <param name="imageType">The type of the image stream to open.</param>
    /// <param name="resolution">The resolution of the image stream to open.</param>
    /// <param name="flags">Unused; always zero.</param>
    /// <param name="frameLimit">The number of frames to buffer; always two.</param>
    /// <param name="nextFrameEvent">Unused; always <see cref="IntPtr.Zero"/>.</param>
    /// <param name="stream">An <see cref="IntPtr"/> to the opened image screen, if any, else, <see cref="IntPtr.Zero"/>.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamOpen(ImageType imageType, ImageResolution resolution, uint flags, uint frameLimit, IntPtr nextFrameEvent, out IntPtr stream);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="stream">Unused.</param>
    /// <param name="flags">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageStreamSetImageFrameFlags(IntPtr stream, uint flags);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="stream">Unused.</param>
    /// <param name="flags">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageStreamGetImageFrameFlags(IntPtr stream, out uint flags);

    /// <summary>
    /// Retrieves the next <see cref="ImageFrame"/> from an image stream.
    /// </summary>
    /// <param name="stream">An <see cref="IntPtr"/> to the image stream to retrieve the next <see cref="ImageFrame"/> from.</param>
    /// <param name="millisecondsToWait">The maximum number of milliseconds to wait for an updated image frame.  Normally zero.</param>
    /// <param name="imageFrame">The retrieved <see cref="ImageFrame"/>, if any.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamGetNextFrame(IntPtr stream, uint millisecondsToWait, out ImageFrame imageFrame);

    /// <summary>
    /// Releases a previously retrieved <see cref="ImageFrame"/>.
    /// </summary>
    /// <param name="stream">An <see cref="IntPtr"/> to the image stream for which to release a previously retrieved <see cref="ImageFrame"/>.</param>
    /// <param name="imageFrame">The <see cref="ImageFrame"/> to release.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamReleaseFrame(IntPtr stream, ref ImageFrame imageFrame);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="colorResolution"></param>
    /// <param name="viewArea">Unused.</param>
    /// <param name="depthX">Unused.</param>
    /// <param name="depthY">Unused.</param>
    /// <param name="depthValue">Unused.</param>
    /// <param name="colorX">Unused.</param>
    /// <param name="colorY">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageGetColorPixelCoordinatesFromDepthPixel(ImageResolution colorResolution, ref ImageViewArea viewArea, int depthX, int depthY, ushort depthValue, out int colorX, out int colorY);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="viewArea">Unused.</param>
    /// <param name="depthX">Unused.</param>
    /// <param name="depthY">Unused.</param>
    /// <param name="depthValue">Unused.</param>
    /// <param name="colorX">Unused.</param>
    /// <param name="colorY">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution(ImageResolution colorResolution, ImageResolution depthResolution, ref ImageViewArea viewArea, int depthX, int depthY, ushort depthValue, out int colorX, out int colorY);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthValueCount">Unused.</param>
    /// <param name="depthValues">Unused.</param>
    /// <param name="colorCoordinateCount">Unused.</param>
    /// <param name="colorCoordinates">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolution(ImageResolution colorResolution, ImageResolution depthResolution, uint depthValueCount, IntPtr depthValues, uint colorCoordinateCount, IntPtr colorCoordinates);

    /// <summary>
    /// Sets the pitch of the Kinect (using its motorized stand).
    /// </summary>
    /// <param name="degrees">The number of degrees relative to the horizon, where positive values are above the horizon and negative values are below.  The limit is approximately +/- 27.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiCameraElevationSetAngle(int degrees);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="degrees">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiCameraElevationGetAngle(out int degrees);

    /// <summary>
    /// Enables <see cref="Skeleton"/> tracking.
    /// </summary>
    /// <param name="nextFrameEvent">Unused.  Always <see cref="IntPtr.Zero"/>.</param>
    /// <param name="flags">Settings regarding the <see cref="Skeleton"/> tracking to perform.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonTrackingEnable(IntPtr nextFrameEvent, SkeletonTrackingFlags flags);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiSkeletonTrackingDisable();

    /// <summary>
    /// Specifies which <see cref="Skeleton"/>s are to be tracked fully.
    /// </summary>
    /// <param name="playerIds">The <see cref="Skeleton.PlayerIdentifier"/>s of two <see cref="Skeleton"/>s which are to be tracked fully.  Use zero where no <see cref="Skeleton"/> is to be tracked fully.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonSetTrackedSkeletons([MarshalAs(UnmanagedType.LPArray, SizeConst = SkeletonFrame.NumberOfSkeletonsTrackedInDetail)] uint[] playerIds);

    /// <summary>
    /// Retrieves the next frame of <see cref="Skeleton"/> tracking data.
    /// </summary>
    /// <param name="millisecondsToWait">The maximum number of milliseconds to wait for an updated image frame.  Normally zero.</param>
    /// <param name="skeletonFrame">The retrieved <see cref="SkeletonFrame"/>.</param>
    /// <returns>Zero on success, non-zero on failure or no frame being ready yet.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonGetNextFrame(uint millisecondsToWait, ref SkeletonFrame skeletonFrame);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="skeletonFrame">Unused.</param>
    /// <param name="smoothingParameters">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiTransformSmooth(ref IntPtr skeletonFrame, ref IntPtr smoothingParameters);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="unknown">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiGetAudioSource(out IntPtr unknown);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [Obsolete]
    int NuiInstanceIndex();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    [Obsolete]
    string NuiDeviceConnectionId();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    [Obsolete]
    string NuiUniqueId();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    [Obsolete]
    string NuiAudioArrayId();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiStatus();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [Obsolete]
    uint NuiInitializationFlags();

    /// <summary>
    /// Retrieves the <see cref="ICoordinateMapper"/> associated with <see langword="this"/> <see cref="ISensor"/>.
    /// </summary>
    /// <param name="coordinateMapper">The <see cref="ICoordinateMapper"/> associated with <see langword="this"/> <see cref="ISensor"/>.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetCoordinateMapper(out ICoordinateMapper? coordinateMapper);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="stream">Unused.</param>
    /// <param name="imageFrame">Unused.</param>
    /// <param name="nearMode">Unused.</param>
    /// <param name="frameTexture">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiImageFrameGetDepthImagePixelFrameTexture(IntPtr stream, ref ImageFrame imageFrame, out int nearMode, out IFrameTexture frameTexture);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="cameraSettings">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiGetColorCameraSettings(out IntPtr cameraSettings);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [Obsolete]
    int NuiGetForceInfraredEmitterOff();

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="forceInfraredEmitterOff">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiSetForceInfraredEmitterOff(int forceInfraredEmitterOff);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="reading">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiAccelerometerGetCurrentReading(out KinectVector4 reading);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="depthFilter">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiSetDepthFilter(IntPtr depthFilter);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="depthFilter">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiGetDepthFilter(out IntPtr depthFilter);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="timeStamp">Unused.</param>
    /// <param name="depthFilter">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int NuiGetDepthFilterForTimeStamp(long timeStamp, out IntPtr depthFilter);
}
