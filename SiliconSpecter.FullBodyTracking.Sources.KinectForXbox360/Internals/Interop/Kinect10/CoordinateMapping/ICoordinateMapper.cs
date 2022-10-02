using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10.CoordinateMapping;

/// <summary>
/// Maps between various forms of data created by the Kinect.
/// </summary>
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("618E3670-BA84-4405-898A-3FF64446157C"), ComImport]
public interface ICoordinateMapper
{
    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="dataByteCount">Unused.</param>
    /// <param name="data">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetColorToDepthRelationalParameters(out uint dataByteCount, out IntPtr data);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="callback">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NotifyParametersChanged(IntPtr callback);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="colorType">Unused.</param>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPixelCount">Unused.</param>
    /// <param name="depthPixels">Unused.</param>
    /// <param name="depthPointCount">Unused.</param>
    /// <param name="depthPoints">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapColorFrameToDepthFrame(ImageType colorType, ImageResolution colorResolution, ImageResolution depthResolution, uint depthPixelCount, IntPtr depthPixels, uint depthPointCount, IntPtr depthPoints);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="colorType">Unused.</param>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPixelCount">Unused.</param>
    /// <param name="depthPixels">Unused.</param>
    /// <param name="skeletonPointCount">Unused.</param>
    /// <param name="skeletonPoints">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapColorFrameToSkeletonFrame(ImageType colorType, ImageResolution colorResolution, ImageResolution depthResolution, uint depthPixelCount, IntPtr depthPixels, uint skeletonPointCount, IntPtr skeletonPoints);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPixelCount">Unused.</param>
    /// <param name="depthPixels">Unused.</param>
    /// <param name="colorType">Unused.</param>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="colorPointCount">Unused.</param>
    /// <param name="colorPoints">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapDepthFrameToColorFrame(ImageResolution depthResolution, uint depthPixelCount, IntPtr depthPixels, ImageType colorType, ImageResolution colorResolution, uint colorPointCount, IntPtr colorPoints);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPixelCount">Unused.</param>
    /// <param name="depthPixels">Unused.</param>
    /// <param name="skeletonPointCount">Unused.</param>
    /// <param name="skeletonPoints">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapDepthFrameToSkeletonFrame(ImageResolution depthResolution, uint depthPixelCount, IntPtr depthPixels, uint skeletonPointCount, IntPtr skeletonPoints);

    /// <summary>
    /// Maps from a coordinate in depth space to the equivalent coordinate in color space.
    /// </summary>
    /// <param name="depthResolution">The resolution of the depth buffer.</param>
    /// <param name="depthPoint">The point in the depth buffer.</param>
    /// <param name="colorType">The format of the color buffer.</param>
    /// <param name="colorResolution">The resolution of the color buffer.</param>
    /// <param name="colorPoint">The point in the color buffer.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapDepthPointToColorPoint(ImageResolution depthResolution, ref DepthImagePoint depthPoint, ImageType colorType, ImageResolution colorResolution, out Point colorPoint);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPoint">Unused.</param>
    /// <param name="skeletonPoint">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapDepthPointToSkeletonPoint(ImageResolution depthResolution, ref DepthImagePoint depthPoint, out KinectVector4 skeletonPoint);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="skeletonPoint">Unused.</param>
    /// <param name="colorType">Unused.</param>
    /// <param name="colorResolution">Unused.</param>
    /// <param name="colorPoint">Unused.</param>
    /// <returns>Unused.</returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapSkeletonPointToColorPoint(ref KinectVector4 skeletonPoint, ImageType colorType, ImageResolution colorResolution, out Point colorPoint);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="skeletonPoint">Unused.</param>
    /// <param name="depthResolution">Unused.</param>
    /// <param name="depthPoint">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int MapSkeletonPointToDepthPoint(ref KinectVector4 skeletonPoint, ImageResolution depthResolution, out DepthImagePoint depthPoint);
}
