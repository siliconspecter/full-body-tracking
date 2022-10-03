using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10
{
  /// <inheritdoc />
  public sealed class Kinect10 : IKinect10
  {
    [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "NuiCreateSensorById")]
    private static extern int NuiCreateSensorByIdImport([MarshalAs(UnmanagedType.LPWStr)] string instanceName, out ISensor sensor);

    /// <inheritdoc />
    public int NuiCreateSensorById(string instanceName, out ISensor sensor)
    {
      return NuiCreateSensorByIdImport(instanceName, out sensor);
    }
  }
}
