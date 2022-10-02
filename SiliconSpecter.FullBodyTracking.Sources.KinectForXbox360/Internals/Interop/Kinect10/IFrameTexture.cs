using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;

/// <summary>
/// A handle to an image from the Kinect.
/// </summary>
[Guid("13EA17F5-FF2E-4670-9EE5-1297A6E880D1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComImport]
public interface IFrameTexture
{
    /// <summary>
    /// Unused.
    /// </summary>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [Obsolete]
    int BufferLen();

    /// <summary>
    /// Retrieves the number of <see cref="byte"/>s between each row of the buffer.
    /// </summary>
    /// <returns>The number of <see cref="byte"/>s between each row of the buffer.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int Pitch();

    /// <summary>
    /// Locks an area of <see langword="this"/> <see cref="IFrameTexture"/> for direct access.
    /// </summary>
    /// <param name="level">Unused.  Always zero.</param>
    /// <param name="lockedArea">Details regarding the area locked.</param>
    /// <param name="rect">Unused.  Always <see cref="IntPtr.Zero"/>.</param>
    /// <param name="flags">Unused.  Always zero.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int LockRect(uint level, ref LockedArea lockedArea, IntPtr rect, uint flags);

    /// <summary>
    /// Unused.
    /// </summary>
    /// <param name="level">Unused.</param>
    /// <param name="description">Unused.</param>
    /// <returns>Unused.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    [Obsolete]
    int GetLevelDesc(uint level, ref IntPtr description);

    /// <summary>
    /// Unlocks the previously locked area of <see langword="this"/> <see cref="IFrameTexture"/>.
    /// </summary>
    /// <param name="level">Unused.  Always zero.</param>
    /// <returns>Zero on success, non-zero on failure.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int UnlockRect(uint level);
}
