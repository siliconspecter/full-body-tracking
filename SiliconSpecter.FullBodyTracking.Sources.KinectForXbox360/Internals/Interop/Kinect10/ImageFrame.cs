using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.Kinect10;

// TODO: is it right that nothing here is used?

/// <summary>
/// Represents a frame read from an image stream.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct ImageFrame
{
  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public long TimeStamp;

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public uint FrameNumber;

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public ImageType ImageType;

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public ImageResolution Resolution;

  /// <summary>
  /// The <see cref="IFrameTexture"/> which backs onto <see langword="this"/> <see cref="ImageFrame"/>.
  /// </summary>
  public IFrameTexture FrameTexture;

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public uint FrameFlags;

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  public ImageViewArea ViewArea;
}
