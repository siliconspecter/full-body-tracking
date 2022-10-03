using System;
using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kernel32
{
  /// <inheritdoc />
  public sealed class Kernel32 : IKernel32
  {
    [DllImport("kernel32.dll", EntryPoint = "CopyMemory")]
    private static extern void CopyMemoryImport(IntPtr dest, IntPtr src, uint count);

    /// <inheritdoc />
    public void CopyMemory(IntPtr dest, IntPtr src, uint count)
    {
      CopyMemoryImport(dest, src, count);
    }
  }
}
