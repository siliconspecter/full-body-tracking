namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kernel32;

/// <summary>
/// Exposes native methods in kernel32.dll.
/// </summary>
public interface IKernel32
{
    /// <summary>
    /// Copies a specified number of <see cref="byte"/>s from one <see cref="IntPtr"/> to another.
    /// </summary>
    /// <param name="dest">The destination to copy to.</param>
    /// <param name="src">The source to copy from.</param>
    /// <param name="count">The number of <see cref="byte"/>s to copy.</param>
    void CopyMemory(IntPtr dest, IntPtr src, uint count);
}
